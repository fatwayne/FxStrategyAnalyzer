using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.DataAccess;


namespace FXStrategy.DataAccess
{
    /// <summary>
    /// Contains all currency data. It handles connection to the data source.
    /// </summary>
    public class CurrencyDataSource
    {

        /// <summary>
        /// Entity class
        /// </summary>
        private FXEntities.FXEntities _fxEntities;
        private Dictionary<Tuple<string, string>, CurrencyPairData> _currencyPairDataTable;
        private Dictionary<string, TimeSeriesCollection<InterestRate>> _currencyNameInterestRateTable;

        private ExchangeRateAdapter _exRateAdapter;
        private InterestRateAdapter _inRateAdapter;

        private List<FXEntities.Currency> _allAvailableCurrencies;

        public CurrencyDataSource(FXEntities.FXEntities fxEntities)
        {
            _fxEntities = fxEntities;

            _exRateAdapter = new ExchangeRateAdapter(_fxEntities);
            _inRateAdapter = new InterestRateAdapter(_fxEntities);
            _currencyPairDataTable = new Dictionary<Tuple<string, string>, CurrencyPairData>();
            _currencyNameInterestRateTable = new Dictionary<string, TimeSeriesCollection<InterestRate>>();
        }

        private bool _isPreloaded = false;

        public void PreLoad()
        {
            if (_isPreloaded == true)
                return;

            var codes = GetAllAvailableCurrencies().Select(c => c.CurrencyCode);
            var baseCodes = _fxEntities.ExchangeRates.Select(e => e.BaseCurrencyCode).Distinct();
            foreach (var code in codes)
            {
                foreach (var baseCode in baseCodes)
                {
                    GetCurrencyPairData(baseCode, code);
                }
            }
            GetAllAvailableCurrencies();

            _isPreloaded = true;
        }
      
        public TimeSeriesCollection<InterestRate> GetInterestRateData(string currencyName)
        {
            if (!_currencyNameInterestRateTable.Keys.Contains(currencyName))
            {
                TimeSeriesCollection<InterestRate> inRateCollection = new TimeSeriesCollection<InterestRate>();
                inRateCollection.AddTimeSeriesData(
                    _inRateAdapter.GetInterestRates(currencyName)
                    .Select(c => new InterestRate()
                    {
                        CurrencyName = currencyName,
                        Time = c.Date,
                        Value = c.Value
                    })
                    );
                _currencyNameInterestRateTable[currencyName] = inRateCollection;
            }
            return _currencyNameInterestRateTable[currencyName];
        }

        public CurrencyPairData GetCurrencyPairData(string baseCurrencyName, string variableCurrencyName)
        {
            if (!_currencyPairDataTable.Keys.Contains(new Tuple<string, string>(baseCurrencyName, variableCurrencyName)))
            {
               
                CurrencyPairData currencyData = new CurrencyPairData(baseCurrencyName, variableCurrencyName);

                TimeSeriesCollection<ExchangeRateBid> exRateCollection = new TimeSeriesCollection<ExchangeRateBid>();
                exRateCollection.AddTimeSeriesData(
                    _exRateAdapter.GetExchangeRates(baseCurrencyName, variableCurrencyName).
                        Select(a => new ExchangeRateBid()
                        {
                            BaseCurrencyName = baseCurrencyName,
                            VariableCurrencyName = variableCurrencyName,
                            Time = a.Date,
                            Value = a.BidPrice
                        }));
                currencyData.ExchangeRates = exRateCollection;

                TimeSeriesCollection<ExchangeRateAsk> exAskRateCollection = new TimeSeriesCollection<ExchangeRateAsk>();
                exAskRateCollection.AddTimeSeriesData(
                    _exRateAdapter.GetExchangeRates(baseCurrencyName, variableCurrencyName).
                        Select(a => new ExchangeRateAsk()
                        {
                            BaseCurrencyName = baseCurrencyName,
                            VariableCurrencyName = variableCurrencyName,
                            Time = a.Date,
                            Value = a.AskPrice
                        }));
                currencyData.AskExchangeRates = exAskRateCollection;

                currencyData.VariableCurrencyInterestRates = GetInterestRateData(variableCurrencyName);
                currencyData.BaseCurrencyInterestRates = GetInterestRateData(baseCurrencyName);

                //_currencyDataTable[variableCurrencyName] = currencyData;
                _currencyPairDataTable[new Tuple<string, string>(baseCurrencyName, variableCurrencyName)] = currencyData;
               
            }
            return _currencyPairDataTable[new Tuple<string, string>(baseCurrencyName, variableCurrencyName)];
        }



        public List<FXEntities.Currency> GetAllAvailableCurrencies()
        {
            if (_allAvailableCurrencies == null)
                _allAvailableCurrencies = _fxEntities.Currencies.ToList();
            return _allAvailableCurrencies;
        }

        public IEnumerable<string> GetCurrencyNameOrderByInterestRate(DateTime date)
        {
            return GetAllAvailableCurrencies()
                .Select(c => GetInterestRateData(c.CurrencyCode).AtTime(date)).OrderBy(i => i.Value).Select(i => i.CurrencyName);

           // return _fxEntities.InterestRates.Where(i => i.Date == date).OrderBy(i => i.Value).Select(i => i.CurrencyCode);
        }

     
    }
}
