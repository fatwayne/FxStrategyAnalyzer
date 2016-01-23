using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel;
using System.IO;
using System.Data;
using FXEntities;


namespace ExcelFileReader
{
    public class RateUploader
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public static void UploadInterestRateExcel(string filePath)
        {
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

            //1. Reading from a binary Excel file ('97-2003 format; *.xls)
            //IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            //...
            //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
            using (IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream))
            {
                Dictionary<int, string> currencyPosition = new Dictionary<int, string>(){
                    {1, "EUR"},{2, "USD"},{3, "JPY"},{4, "GBP"},{5,"CHF"},
                    {6, "AUD"}, {7, "CAD"},{8, "SEK"},{9, "NOK"},{10,"NZD"}
                };
                //...
                //3. DataSet - The result of each spreadsheet will be created in the result.Tables
                // DataSet result = excelReader.AsDataSet();
                //...
                //4. DataSet - Create column names from first row
                excelReader.IsFirstRowAsColumnNames = true;
                DataSet result = excelReader.AsDataSet();

                DataTable dt = result.Tables[0];

                DateTime datetime;
                using (FXEntities.FXEntities fxEntities = new FXEntities.FXEntities())
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        datetime = FromExcelSerialDate(Convert.ToInt32(dr[0]));
                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (dt.Columns.IndexOf(dc) != 0)
                            {
                                fxEntities.AddToInterestRates(new FXEntities.InterestRate()
                                {
                                    CurrencyCode = currencyPosition[dt.Columns.IndexOf(dc)],
                                    Date = datetime,
                                    Value = Convert.ToDecimal( Convert.ToDouble(dr[dc.ColumnName]))
                                });
                            }
                        }
                    }

                    fxEntities.SaveChanges();
                }

            }

            stream.Close();
        }

        //public static void UploadExchangeRateExcel(string filePath)
        //{
        //    FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

        //    //1. Reading from a binary Excel file ('97-2003 format; *.xls)
        //    //IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
        //    //...
        //    //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
        //    using (IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream))
        //    {
        //        //...
        //        //3. DataSet - The result of each spreadsheet will be created in the result.Tables
        //        // DataSet result = excelReader.AsDataSet();
        //        //...
        //        //4. DataSet - Create column names from first row
        //        excelReader.IsFirstRowAsColumnNames = true;
        //        DataSet result = excelReader.AsDataSet();

        //        DataTable dt = result.Tables[0];

        //        DateTime datetime;
        //        string baseCurrency;
        //        string variableCurrency;
        //        using (FXEntities.FXEntities fxEntities = new FXEntities.FXEntities())
        //        {
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                if (dr["Date"].ToString().Equals(""))
        //                    continue;
        //                datetime = Convert.ToDateTime(dr["Date"]);
        //                foreach (DataColumn dc in dt.Columns)
        //                {
        //                    if (dc.ColumnName != "Date")
        //                    {
        //                        baseCurrency = dc.ColumnName.Split('/')[0];
        //                        variableCurrency = dc.ColumnName.Split('/')[1];
        //                        fxEntities.AddToExchangeRates(new FXEntities.ExchangeRate()
        //                        {
        //                            BaseCurrency = fxEntities.Currencies.Where(c => c.CurrencyCode == baseCurrency).First(),
        //                            VarialeCurrency = fxEntities.Currencies.Where(c => c.CurrencyCode == variableCurrency).First(),
        //                            Date = datetime,
        //                            Value = Convert.ToDecimal(Convert.ToDouble(dr[dc.ColumnName]))
        //                        });
        //                    }
        //                }
        //            }

        //            fxEntities.SaveChanges();
        //        }

        //    }

        //    stream.Close();
        //}
        public static DateTime FromExcelSerialDate(int SerialDate)
        {
            if (SerialDate > 59) SerialDate -= 1; //Excel/Lotus 2/29/1900 bug   
            return new DateTime(1899, 12, 31).AddDays(SerialDate);
        }

        public static void UploadExchangeBidAskRateExcel(string filePath)
        {
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

            //1. Reading from a binary Excel file ('97-2003 format; *.xls)
            //IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            //...
            //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
            using (IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream))
            {
                //...
                //3. DataSet - The result of each spreadsheet will be created in the result.Tables
                // DataSet result = excelReader.AsDataSet();
                //...
                //4. DataSet - Create column names from first row
                
                const int TOTAL_NUM_CURRENCY = 9;

                excelReader.IsFirstRowAsColumnNames = false;
                DataSet result = excelReader.AsDataSet();

                DataTable dt = result.Tables[0];

                DateTime datetime;
                string baseCurrency;
                string variableCurrency;
                int targetFirstColumn;

              
              
                    List<ExchangeRate> exchangeRateList = new List<ExchangeRate>();
                    for(int i = 0; i < TOTAL_NUM_CURRENCY; i++){
                        targetFirstColumn = i * 4;

                        string currencyPair = dt.Rows[0][targetFirstColumn].ToString().Split(' ')[0];
                        baseCurrency = currencyPair.Substring(0, 3);
                        variableCurrency = currencyPair.Substring(3, 3);

                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dt.Rows.IndexOf(dr) == 0 || dt.Rows.IndexOf(dr) == 1)
                                continue;

                            datetime = FromExcelSerialDate(Convert.ToInt32(dr[targetFirstColumn]));


                            exchangeRateList.Add(new FXEntities.ExchangeRate()
                            {
                                BaseCurrencyCode = baseCurrency,
                                VariableCurrencyCode = variableCurrency,
                                Date = datetime,
                                BidPrice = Convert.ToDecimal(Convert.ToDouble(dr[targetFirstColumn + 1])),
                                AskPrice = Convert.ToDecimal(Convert.ToDouble(dr[targetFirstColumn + 2]))
                            });

                        }
                    }

                    DateTime minDate = exchangeRateList.Select(e => e.Date).Min();
                    DateTime maxDate = exchangeRateList.Select(e => e.Date).Max();
                    var availableDates = Util.DateTimeHelper.GetWeekdaysDate(
                            minDate, maxDate   );

                    foreach (var currency in exchangeRateList.Select(e => e.VariableCurrencyCode).Distinct().Where(c => c != "EUR").ToList())
                    {
                        var insertedDates = exchangeRateList
                            .Where(e => e.VariableCurrencyCode == currency).Select(e => e.Date);

                        var leftDays = availableDates.Except(insertedDates);

                        foreach (var leftDay in leftDays)
                        {
                            if (leftDay == minDate)
                                continue;

                            var lastRecordBeforeLeftDay = exchangeRateList
                                                            .Where(e => e.VariableCurrencyCode == currency &&
                                                                e.Date == insertedDates.Where(d => d < leftDay).Max()).FirstOrDefault();

                            if (lastRecordBeforeLeftDay == null)
                                throw new Exception("Cannot find record to replace");
                            else
                            {
                                ExchangeRate newExchangeRate = new ExchangeRate()
                                {
                                    BaseCurrencyCode = lastRecordBeforeLeftDay.BaseCurrencyCode,
                                    VariableCurrencyCode = lastRecordBeforeLeftDay.VariableCurrencyCode,
                                    BidPrice = lastRecordBeforeLeftDay.BidPrice,
                                    AskPrice = lastRecordBeforeLeftDay.AskPrice,
                                    Date = leftDay
                                };

                                exchangeRateList.Add(newExchangeRate);
                                
                            }

                        }
                    }
                    using (FXEntities.FXEntities fxEntities = new FXEntities.FXEntities())
                    {
                        exchangeRateList.ForEach(e => fxEntities.AddToExchangeRates(e));
                      fxEntities.SaveChanges();
                      }

            }

            stream.Close();
        }

      

    }
}
