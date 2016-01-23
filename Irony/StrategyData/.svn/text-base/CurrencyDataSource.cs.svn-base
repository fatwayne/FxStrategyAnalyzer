using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.DataAccess;


namespace Irony.StrategyData
{
    public class CurrencyDataSource
    {
        private Currency _baseCurrency;

        public Currency BaseCurrency
        {
            get { return _baseCurrency; }
        }

        public CurrencyDataSource(Currency baseCurrency)
        {
            _baseCurrency = baseCurrency;

            //_exRateAdapter = new ExchangeRateAdapter();
            //_inRateAdapter = new InterestRateAdapter();
            //_currencyAdapter = new CurrencyAdapter();
            _currencyDataTable = new Dictionary<Currency, CurrencyData>();
        }

        private Dictionary<Currency, CurrencyData> _currencyDataTable;
        private ExchangeRateAdapter _exRateAdapter;
        private InterestRateAdapter _inRateAdapter;
        private CurrencyAdapter _currencyAdapter;

        private TimeSeriesCollection<InterestRate> _baseInRateCollection;
        private void InititalizeBaseInRate()
        {
            _baseInRateCollection.AddTimeSeriesData(
                    _inRateAdapter.GetInterestRates(BaseCurrency.Name)
                    .Select(c => new InterestRate()
                    {
                        Currency = BaseCurrency,
                        Time = c.Date,
                        Value = c.Value
                    })
                    );
        }

        public IEnumerable<CurrencyData> GetAllCurrencyData()
        {
            foreach (var currency in _currencyAdapter.GetCurrencies())
            {
                yield return GetCurrencyData(new Currency(currency.CurrencyCode));
            }
        }


        public CurrencyData GetCurrencyData(Currency variableCurrency)
        {
            if (!_currencyDataTable.Keys.Contains(variableCurrency))
            {

                CurrencyAdapter currrencyAdapter = null;// = new CurrencyAdapter();
                var currencyList = currrencyAdapter.GetCurrencies();

              
                CurrencyData currencyData = new CurrencyData(_baseCurrency,variableCurrency);

                //TimeSeriesCollection<InterestRate> baseInRateCollection = new TimeSeriesCollection<InterestRate>();
                //baseInRateCollection.AddTimeSeriesData(
                //        inRateAdapter.GetInterestRates(BaseCurrency.Name)
                //        .Select(c => new InterestRate()
                //        {
                //            Currency = BaseCurrency,
                //            Time = c.Date,
                //            Value = c.Value
                //        })
                //        );

                currencyData.BaseCurrencyInterestRates = _baseInRateCollection;

                TimeSeriesCollection<ExchangeRate> exRateCollection = new TimeSeriesCollection<ExchangeRate>();
                exRateCollection.AddTimeSeriesData(
                    _exRateAdapter.GetExchangeRates(BaseCurrency.Name, variableCurrency.Name).
                        Select(a => new ExchangeRate()
                        {
                            BaseCurrency = BaseCurrency,
                            VariableCurrency = variableCurrency,
                            Time = a.Date,
                            Value = a.AskPrice // TODO
                        }));
                currencyData.ExchangeRates = exRateCollection;
                
                TimeSeriesCollection<InterestRate> varInRateCollection = new TimeSeriesCollection<InterestRate>();
                varInRateCollection.AddTimeSeriesData(
                        _inRateAdapter.GetInterestRates(variableCurrency.Name)
                        .Select(c => new InterestRate()
                        {
                            Currency = variableCurrency,
                            Time = c.Date,
                            Value = c.Value
                        })
                        );
                currencyData.VariableCurrencyInterestRates = varInRateCollection;

                _currencyDataTable[variableCurrency] = currencyData;
                return currencyData;


            }
            else
            {
                return _currencyDataTable[variableCurrency];
            }
        }
    }
}
