using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.EvaluationContext
{
    public class CurrencyPairData
    {
        public CurrencyPairData(string baseCurrencyName, string variableCurrrencyName)
        {
            _baseCurrencyName = baseCurrencyName;
            _variableCurrencyName = variableCurrrencyName;
        }

        private string _baseCurrencyName;

        public string BaseCurrencyName
        {
            get { return _baseCurrencyName; }
        }
        private string _variableCurrencyName;
        public string VariableCurrencyName
        {
            get { return _variableCurrencyName; }
        }

        private TimeSeriesCollection<ExchangeRateAsk> _askExchangeRates;
        public TimeSeriesCollection<ExchangeRateAsk> AskExchangeRates
        {
            get { return _askExchangeRates; }
            set { _askExchangeRates = value; }
        }

        public TimeSeriesCollection<ExchangeRateBid> _bidExchangeRates;
        public TimeSeriesCollection<ExchangeRateBid> BidExchangeRates
        {
            get { return _bidExchangeRates; }
            set { _bidExchangeRates = value; }
        }


        public TimeSeriesCollection<ExchangeRateBid> ExchangeRates
        {
            get { return _bidExchangeRates; }
            set { _bidExchangeRates = value; }
        }

        /// <summary>
        /// Interest rate difference of Base currency and variable currency
        /// Variable currency - Base currency
        /// </summary>
        public decimal GetInterestRateDiff(DateTime targetTime)
        {
            return ( VariableCurrencyInterestRates.AtTime(targetTime).Value -
                BaseCurrencyInterestRates.AtTime(targetTime).Value);
        }


        private TimeSeriesCollection<InterestRate> _baseCurrencyInterestRates;
        public TimeSeriesCollection<InterestRate> BaseCurrencyInterestRates
        {
            get { 
                return _baseCurrencyInterestRates; }
            set { _baseCurrencyInterestRates = value; }
        }

        private TimeSeriesCollection<InterestRate> _variableCurrencyInterestRates;
        public TimeSeriesCollection<InterestRate> VariableCurrencyInterestRates
        {
            get { return _variableCurrencyInterestRates; }
            set { _variableCurrencyInterestRates = value; }
        }
    }
}
