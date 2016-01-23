using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.EvaluationContext
{
    public class ExchangeRate : TimeSeriesData
    {
        private string _baseCurrencyName;
        private string _variableCurrencyName;

        public string VariableCurrencyName
        {
            get { return _variableCurrencyName; }
            set { _variableCurrencyName = value; }
        }

        public string BaseCurrencyName
        {
            get { return _baseCurrencyName; }
            set { _baseCurrencyName = value; }
        }
    }

    public class ExchangeRateBid : ExchangeRate { }

    public class ExchangeRateAsk : ExchangeRate { }
}
