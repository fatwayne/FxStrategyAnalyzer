using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Irony.StrategyData
{
    public class CurrencyData
    {
        public CurrencyData(Currency baseCurrency, Currency variableCurrrency)
        {
            _baseCurrency = baseCurrency;
            _variableCurrency = variableCurrrency;
        }

        private Currency _baseCurrency;

        public Currency BaseCurrency
        {
            get { return _baseCurrency; }
        }
        private Currency _variableCurrency;
        public Currency VariableCurrency
        {
            get { return _variableCurrency; }
        }

        private TimeSeriesCollection<ExchangeRate> _exchangeRates;

        public TimeSeriesCollection<ExchangeRate> ExchangeRates
        {
            get { return _exchangeRates; }
            set { _exchangeRates = value; }
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
