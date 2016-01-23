using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.Interpreter;
using FXStrategy.DataAccess;
using FXStrategy.EvaluationContext;
using FXStrategy.MetaModel;
using FXStrategy.Interpreter.Data;
using FXStrategy.DataAccess;

namespace FXStrategy.Interpreter
{
    /// <summary>
    /// Responsible for profit calculation
    /// </summary>
    public class ProfitCalculationEngine
    {
        List<DateTime> _effectiveDates;
        CurrencyDataSource _currencyDataSource;

        public ProfitCalculationEngine(CurrencyDataSource currencyDataSource)
        {
            _currencyDataSource = currencyDataSource;
           
        }

        /// <summary>
        /// Calculate returns from the position records in the position run time
        /// </summary>
        /// <param name="positionRunTimeList">list of positions (from the runtime)</param>
        /// <param name="startDate">start date of the back-testing analysis</param>
        /// <param name="endDate">end date of the back-testing analysis</param>
        /// <param name="termLength">Length of forward contract</param>
        public void Evaluate(List<PositionRuntime> positionRunTimeList, 
            DateTime startDate, DateTime endDate, PeriodicTimeDefinition termLength)
        {
             if (termLength.AvailableDates == null)
                termLength.Initialize(startDate, endDate);

             _effectiveDates = Util.DateTimeHelper.GetWeekdaysDate(startDate, endDate);

            _individualPositionIndex = new Dictionary<PositionRuntime, List<TimeSeriesData>>();

            // initialize data set
            _currencyDataSource.PreLoad();

            // Calculate profit for each position parallely
            positionRunTimeList.AsParallel().ForAll(position =>
                CalculateIndividualPositionProfitIndexes(startDate,endDate,termLength,position));

            CalculateReturnIndex(startDate,endDate,termLength);
        }

        /// <summary>
        /// Calculate the total profit from all positions
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="termLength"></param>
        private void CalculateReturnIndex(DateTime startDate, DateTime endDate, PeriodicTimeDefinition termLength)
        {
            _returnOverTime = new List<TimeSeriesData>();
            _indexOverTime = new List<TimeSeriesData>();

            int numberOfPositions = _individualPositionIndex.Keys.Count;
            var returns = _individualPositionIndex.Values.SelectMany(d => d);
            // except first date
            var effectiveTermDates = termLength.AvailableDates.Where(d => d != startDate);

            decimal averageReturn;
            decimal currentIndex = 100;
            foreach (var date in _effectiveDates)
            {
                var targetRecords = returns.Where(r => r.Time == date);

                if (targetRecords == null || targetRecords.Count() == 0)
                    continue;

                averageReturn = returns.Where(r => r.Time == date).Average(r => r.Value);

                // if it is the maturity date, accumulate the profit
                if (effectiveTermDates.Contains(date))
                {
                    currentIndex = currentIndex * (1 + averageReturn);
                    _indexOverTime.Add(new TimeSeriesData()
                    {
                        Time = date,
                        Value = currentIndex
                    });
                }
                else
                {
                    _indexOverTime.Add(new TimeSeriesData()
                    {
                        Time = date,
                        Value = currentIndex * (1 + averageReturn)
                    });
                }

                _returnOverTime.Add(new TimeSeriesData()
                {
                    Time = date,
                    Value = averageReturn
                });
            }
        }

        /// <summary>
        /// Calculate profit for an individual position over time
        /// </summary>
        /// <param name="startDate">start date of the back-testing analysis</param>
        /// <param name="endDate">end date of the back-testing analysis</param>
        /// <param name="termLength">Length of forward contract</param>
        /// <param name="position">the position for calculation</param>
        private void CalculateIndividualPositionProfitIndexes(DateTime startDate, 
            DateTime endDate, PeriodicTimeDefinition termLength, PositionRuntime position)
        {
            AssignEndDateToLastRecord(endDate, position);
            List<PositionRecord> transformedRecord = TransformPositionRecords(position, termLength);
            List<TimeSeriesData> positionIndexes = new List<TimeSeriesData>();
            foreach (var record in transformedRecord)
            {

                DateTime effectiveStartDate = GetEffectiveStartDate(record);
                DateTime effectiveEndDate = GetEffectiveEndDate(record);

                decimal startExchangeRate = GetExchangeRateBaseOnPosType(record, effectiveStartDate);

                decimal variableInRate = 
                    GetIntRate(record.CurrencyInPosition.Name, effectiveStartDate) / (decimal)100;
                decimal baseInRate = 
                    GetIntRate(record.BaseCurrency.Name, effectiveStartDate) / (decimal)100;

                decimal forwardRate =
                    CalculateForwardRate((effectiveEndDate - effectiveStartDate).Days, 
                    startExchangeRate, baseInRate, variableInRate);

                foreach (var currentDate in DateTimeHelper.GetWeekdaysDate(record.StartDate, record.EndDate))
                {
                    decimal curExchangeRate = GetExchangeRate(record, currentDate);

                    decimal curProfit = CalculateProfitBaseOnPosType(record, forwardRate, curExchangeRate);

                    var existingIndex = positionIndexes.Where(d => d.Time == currentDate).FirstOrDefault();

                    if (existingIndex == null)
                    {
                        positionIndexes.Add(new TimeSeriesData()
                        {
                            Time = currentDate,
                            Value = curProfit
                        });
                    }
                    else
                    {
                        existingIndex.Value += curProfit;
                    }
                }
            }
            _individualPositionIndex.Add(position, positionIndexes);
        }

        /// <summary>
        /// Determine whether the exchange rate is ask or bid for forward rate calculation
        /// </summary>
        /// <param name="record"></param>
        /// <param name="effectiveStartDate"></param>
        /// <returns></returns>
        private decimal GetExchangeRateBaseOnPosType(PositionRecord record, DateTime effectiveStartDate)
        {
            decimal startExchangeRate;
            if (record.Type == PositionType.Long)
                startExchangeRate = GetAskExRate(record.BaseCurrency.Name,
                                                                   record.CurrencyInPosition.Name,
                                                                   effectiveStartDate);
            else
                startExchangeRate = GetBidExRate(record.BaseCurrency.Name,
                                                                    record.CurrencyInPosition.Name,
                                                                    effectiveStartDate);
            return startExchangeRate;
        }

        private static decimal CalculateProfitBaseOnPosType(PositionRecord record, decimal forwardRate, decimal curExchangeRate)
        {
            decimal curProfit;
            if (record.Type == PositionType.Long)
                curProfit = (forwardRate - curExchangeRate) / curExchangeRate;
            else
                curProfit = (curExchangeRate - forwardRate) / curExchangeRate;
            return curProfit;
        }

        /// <summary>
        /// Get the bid or ask exchange rate according to the type of the record.
        /// Long position -> Bid price
        /// Short position -> Ask price
        /// </summary>
        /// <param name="record"></param>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        private decimal GetExchangeRate(PositionRecord record, DateTime currentDate)
        {
            decimal curExchangeRate;
            if (record.Type == PositionType.Long)
                curExchangeRate = GetBidExRate(record.BaseCurrency.Name,
                                                            record.CurrencyInPosition.Name,
                                                            currentDate);
            else
                curExchangeRate = GetAskExRate(record.BaseCurrency.Name,
                                                           record.CurrencyInPosition.Name,
                                                           currentDate);
            return curExchangeRate;
        }

        /// <summary>
        /// Get effective end date for a position record.
        /// It is to avoid a position record ends at weekend.
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        private DateTime GetEffectiveEndDate(PositionRecord record)
        {
            DateTime effectiveEndDate;
            if (_effectiveDates.Contains(record.EndDate))
                effectiveEndDate = record.EndDate;
            else
                effectiveEndDate = _effectiveDates.Where(d => d <= record.StartDate).Max();

            return effectiveEndDate;
        }

        /// <summary>
        /// Get the start date of the record which is within a valid date
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        private DateTime GetEffectiveStartDate(PositionRecord record)
        {
            DateTime effectiveStartDate;

            if (_effectiveDates.Contains(record.StartDate))
                effectiveStartDate = record.StartDate;
            else
                effectiveStartDate = _effectiveDates.Where(d => d >= record.StartDate).Min();
            return effectiveStartDate;
        }

        /// <summary>
        /// calculate forward rate
        /// </summary>
        /// <param name="numberOfDays">number of days towards contract end date</param>
        /// <param name="variableSpotRate">spot exchange rate of varaible currency</param>
        /// <param name="baseInRate">interest rate of base currency</param>
        /// <param name="variableInRate">interest rate of variable currency</param>
        /// <returns></returns>
        private static decimal CalculateForwardRate(int numberOfDays, decimal variableSpotRate, decimal baseInRate, decimal variableInRate)
        {
            decimal fraction = (decimal)numberOfDays / (decimal)360;

            decimal forwardRate = variableSpotRate * (1 + variableInRate * fraction) / (1 + baseInRate * fraction);
                                    
            return forwardRate;
        }


        /// <summary>
        /// Split position records into records in the length of termLength
        /// </summary>
        /// <param name="originalPositionRecords"></param>
        /// <param name="termLength"></param>
        /// <returns></returns>
        private List<PositionRecord> TransformPositionRecords(
            PositionRuntime positionRuntime,
            PeriodicTimeDefinition termLength)
        {
            List<DateTime> termEffectiveDates = termLength.AvailableDates;

            List<PositionRecord> transformedPositionRecords = new List<PositionRecord>();

            foreach (var positionRecord in positionRuntime.PositionRecords)
            {
                // list of terms that available for the position
                var effectiveTerms = TermManager.GetTermFromPeriod(positionRecord.StartDate, positionRecord.EndDate, termLength);

                // create terms for each position record
                effectiveTerms.ForEach(t => transformedPositionRecords.Add(new PositionRecord(
                    t.StartDate, 
                    t.EndDate,
                    positionRecord.CurrencyInPosition, positionRecord.BaseCurrency,
                    positionRecord.Type, positionRuntime)));

                if (positionRecord.EndDate != effectiveTerms.Last().EndDate)
                {
                    // for the case ending the record within a forward contract
                    // an opposite position will be opened
                    transformedPositionRecords.Add(new PositionRecord(
                      effectiveTerms.Last().StartDate,
                      effectiveTerms.Last().EndDate,
                      positionRecord.CurrencyInPosition,
                      positionRecord.BaseCurrency,
                      (positionRecord.Type == PositionType.Long) ? PositionType.Short : PositionType.Long,
                      positionRuntime));
                }
            }

            return transformedPositionRecords;
        }

        private static void AssignEndDateToLastRecord(DateTime endDate, PositionRuntime position)
        {
            var emptyEndTimeRecords = position.PositionRecords.SelectMany(r => r.PositionRuntime.PositionRecords).Where(p => p.EndDate == DateTime.MinValue);
            foreach(var emptyEndTimeRecord in emptyEndTimeRecords)
                emptyEndTimeRecord.EndDate = endDate;
        }


        private List<TimeSeriesData>_returnOverTime;
        /// <summary>
        /// Return from all positions over time
        /// </summary>
        public List<TimeSeriesData> ReturnOverTime
        {
            get
            {
                return _returnOverTime;
            }
        }

        private List<TimeSeriesData> _indexOverTime;
        /// <summary>
        /// Accumulated capital contributed by all positions over time
        /// </summary>
        public List<TimeSeriesData> IndexOverTime
        {
            get
            {
                return _indexOverTime;
            }
        }
        /// <summary>
        /// Daily profit of in dividual position
        /// </summary>
        public Dictionary<PositionRuntime, List<TimeSeriesData>> _individualPositionIndex;

        /// <summary>
        /// Get bid exchange rate
        /// </summary>
        /// <param name="baseCurrencyCode">currency code of base currency</param>
        /// <param name="varaibleCurrencyCode">currency code of variable currency</param>
        /// <param name="date">requested date</param>
        /// <returns></returns>
        private decimal GetBidExRate(string baseCurrencyCode, string varaibleCurrencyCode, DateTime date)
        {
            return _currencyDataSource.GetCurrencyPairData(baseCurrencyCode,varaibleCurrencyCode).BidExchangeRates.AtTime(date).Value;
        }


        /// <summary>
        /// Get Ask exchange rate
        /// </summary>
        /// <param name="baseCurrencyCode">currency code of base currency</param>
        /// <param name="varaibleCurrencyCode">currency code of variable currency</param>
        /// <param name="date">requested date</param>
        /// <returns></returns>
        private decimal GetAskExRate(string baseCurrencyCode, string varaibleCurrencyCode, DateTime date)
        {
            return _currencyDataSource.GetCurrencyPairData(baseCurrencyCode, varaibleCurrencyCode).AskExchangeRates.AtTime(date).Value;
        }

        /// <summary>
        /// Get interest rate of a currency
        /// </summary>
        /// <param name="currencyCode">Currency code of the currency</param>
        /// <param name="date">requested date</param>
        /// <returns></returns>
        private decimal GetIntRate(string currencyCode, DateTime date)
        {
            return _currencyDataSource.GetInterestRateData(currencyCode).At(date);
        }
    }

    
}
