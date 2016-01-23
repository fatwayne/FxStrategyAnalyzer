using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.EvaluationContext
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

        /// <summary>
        /// Return currency names
        /// </summary>
        /// <param name="time">Query date</param>
        /// <returns></returns>
        public abstract IEnumerable<string> GetCurrencies(DateTime time);


    }

    /// <summary>
    /// top 3 currencies in term of interest rate
    /// </summary>
    public class Top3Currencies : CurrencyNameSource
    {
        public Top3Currencies(CurrencyDataSource currencyDataSource, Currency baseCurrency)
            : base("Top3Currencies", currencyDataSource, baseCurrency) { }

        /// <summary>
        /// Names of the top 3 currencies
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public override IEnumerable<string> GetCurrencies(DateTime time)
        {
            var top3Currencies = _currencyDataSource
                                .GetCurrencyNameOrderByInterestRate(time)
                                .Reverse()
                                .Take(3);

            // remove base currency
            if (top3Currencies.Contains(BaseCurrency.Name))
            {
                var currencyList = top3Currencies.ToList();
                int position = currencyList.IndexOf(BaseCurrency.Name);
                currencyList.RemoveRange(position, currencyList.Count - position);
                return currencyList;
            }
            else
                return top3Currencies;
        }
    }

    /// <summary>
    /// Bottom 3 Currencies in term of interest rate
    /// </summary>
    public class Bottom3Currencies : CurrencyNameSource
    {
        public Bottom3Currencies(CurrencyDataSource currencyDataSource, Currency baseCurrency)
            : base("Bottom3Currencies", currencyDataSource, baseCurrency) { }

        public override IEnumerable<string> GetCurrencies(DateTime time)
        {
            var bottom3Currencies = _currencyDataSource
                                    .GetCurrencyNameOrderByInterestRate(time)
                                    .Take(3);

            // remove base currency
            if (bottom3Currencies.Contains(BaseCurrency.Name))
            {
                var currencyList = bottom3Currencies.ToList();
                int position = currencyList.IndexOf(BaseCurrency.Name);
                currencyList.RemoveRange(position, currencyList.Count - position);
                return currencyList;
            }
            else
                return bottom3Currencies;
           /*  return _currencyDataSource.GetAllCurrencyData()
                .OrderBy(cp => cp.GetInterestRateDiff(time))
                .Where(cp => cp.GetInterestRateDiff(time) < 0)
                .Select(cp => cp.VariableCurrency.Name).Take(3);*/
        }
    }




}
