using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Irony.StrategyData
{
    public abstract class CurrencyNameSource
    {
        protected CurrencyDataSource _currencyDataSource;
        public Currency BaseCurrency { get; set; }
        public string SourceName { get; set; }

        public CurrencyNameSource(string sourceName, CurrencyDataSource currencyDataSource, Currency baseCurrency)
        {
            _currencyDataSource = currencyDataSource;
            BaseCurrency = baseCurrency;
            SourceName = sourceName;
        }

        public abstract IEnumerable<Currency> GetCurrencies(DateTime time);


    }

    public class Top3Currencies : CurrencyNameSource
    {
        public Top3Currencies(CurrencyDataSource currencyDataSource, Currency baseCurrency)
            : base("Top3Currencies", currencyDataSource, baseCurrency) { }

        public override IEnumerable<Currency> GetCurrencies(DateTime time)
        {
            return _currencyDataSource.GetAllCurrencyData()
                .OrderByDescending(cp => cp.GetInterestRateDiff(time))
                .Where(cp => cp.GetInterestRateDiff(time) > 0)
                .Select(cp => cp.VariableCurrency).Take(3);
        }
    }


    public class Bottom3Currencies : CurrencyNameSource
    {
        public Bottom3Currencies(CurrencyDataSource currencyDataSource, Currency baseCurrency)
            : base("Bottom3Currencies", currencyDataSource, baseCurrency) { }

        public override IEnumerable<Currency> GetCurrencies(DateTime time)
        {
            return _currencyDataSource.GetAllCurrencyData()
                .OrderBy(cp => cp.GetInterestRateDiff(time))
                .Where(cp => cp.GetInterestRateDiff(time) < 0)
                .Select(cp => cp.VariableCurrency).Take(3);
        }
    }




}
