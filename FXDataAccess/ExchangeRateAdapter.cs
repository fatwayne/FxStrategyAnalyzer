using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXStrategy.DataAccess
{
    public class ExchangeRateAdapter
    {
        FXEntities.FXEntities _fxEntities;

        private const string GLOBAL_BASE_CURRENCY = "EUR";

        private Dictionary<Tuple<string, string>, IEnumerable<FXEntities.ExchangeRate>> _nonEURBasePairCache;

        private Dictionary<Tuple<string, string>, IEnumerable<FXEntities.ExchangeRate>> NonEURBasePairCache
        {
            get {
                if (_nonEURBasePairCache == null)
                    _nonEURBasePairCache = new Dictionary<Tuple<string, string>, IEnumerable<FXEntities.ExchangeRate>>();
                return _nonEURBasePairCache; }
        }


        public ExchangeRateAdapter(FXEntities.FXEntities fxEntities)
        {
            _fxEntities = fxEntities;
        }

        public IEnumerable<FXEntities.ExchangeRate> GetExchangeRates(string baseCurrencyCode, string variableCurrencyCode)
        {
            // handle when baseCurrencyCode is not EUR
            if (baseCurrencyCode != GLOBAL_BASE_CURRENCY)
            {
                Tuple<string, string> targetCurrencyPairCode = new Tuple<string, string>(baseCurrencyCode, variableCurrencyCode);
                if (!NonEURBasePairCache.Keys.Contains(targetCurrencyPairCode))
                {
                    var eurBaseExchangeRates = GetExchangeRates(GLOBAL_BASE_CURRENCY, baseCurrencyCode);
                    var eurVariableExchangeRates = GetExchangeRates(GLOBAL_BASE_CURRENCY, variableCurrencyCode);
                    List<FXEntities.ExchangeRate> resultList = new List<FXEntities.ExchangeRate>();

                    eurBaseExchangeRates.Select(e => e.Date).ToList().ForEach(d =>
                        {
                            var tarBaseRecord = eurBaseExchangeRates.Where(e => e.Date == d).First();
                            decimal globalbase_base_bid = tarBaseRecord.BidPrice;
                            decimal globalbase_base_ask = tarBaseRecord.AskPrice;

                            var tarVarRecord = eurVariableExchangeRates.Where(e => e.Date == d).FirstOrDefault();

                            if (tarVarRecord != null)
                            {

                                decimal globalbase_variable_bid = tarVarRecord.BidPrice;
                                decimal globalbase_variable_ask = tarVarRecord.AskPrice;

                                resultList.Add(new FXEntities.ExchangeRate()
                                {
                                    BaseCurrencyCode = baseCurrencyCode,
                                    VariableCurrencyCode = variableCurrencyCode,
                                    BidPrice = globalbase_variable_bid / globalbase_base_ask,
                                    AskPrice = globalbase_variable_ask / globalbase_base_bid

                                });
                            }
                        }
                        );

                    NonEURBasePairCache[targetCurrencyPairCode] = resultList;
                }
                return NonEURBasePairCache[targetCurrencyPairCode];
            }
            else
            {


                var result = from record in _fxEntities.ExchangeRates
                             where record.BaseCurrency.CurrencyCode == baseCurrencyCode &&
                             record.VariableCurrency.CurrencyCode == variableCurrencyCode
                             select record;

                return result;
            }
        }

        public IEnumerable<FXEntities.ExchangeRate> GetExchangeRates()
        {
            var result = from record in _fxEntities.ExchangeRates
                            select record;

            return result;
        }

        public decimal GetAskPrice(string baseCurrencyCode, string variableCurrencyCode, DateTime date)
        {
            var record = GetExchangeRate(baseCurrencyCode, variableCurrencyCode, date);

            if (record != null)
                return record.AskPrice;
            else
                throw new Exception(string.Format("The exchange rate (ask) for the currency pair {0}/{1} at date {2:d} is not found."
                    ,baseCurrencyCode,variableCurrencyCode,date));
        }

        
        public decimal GetBidPrice(string baseCurrencyCode, string variableCurrencyCode, DateTime date)
        {
            var record = GetExchangeRate(baseCurrencyCode, variableCurrencyCode, date);

            if (record != null)
                return record.BidPrice;
            else
                throw new Exception(string.Format("The exchange rate (ask) for the currency pair {0}/{1} at date {2:d} is not found."));
        }

        private FXEntities.ExchangeRate GetExchangeRate(string baseCurrencyCode, string variableCurrencyCode, DateTime date)
        {
            var record = GetExchangeRates(baseCurrencyCode, variableCurrencyCode).Where(e => e.Date == date).FirstOrDefault();
            return record;
        }

    }
}
