using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelFileReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Are you sure to upload data? (Y/N)");
            string input = Console.ReadLine();
            if(input=="Y")
                RateUploader.UploadExchangeBidAskRateExcel(@"D:\Documents\Study Materials\Master Thesis\Implementation\Data\BidAskPriceDataXLS.xls");
                //RateUploader.UploadInterestRateExcel(@"D:\Documents\Study Materials\Master Thesis\Implementation\Data\Renter_daglige.xls");
        }
    }
}
