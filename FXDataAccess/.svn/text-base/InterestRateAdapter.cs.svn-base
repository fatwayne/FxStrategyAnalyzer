using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.DataAccess
{
    public class InterestRateAdapter
    {
        private FXEntities.FXEntities _fxEntities;

        public InterestRateAdapter(FXEntities.FXEntities fxEntities)
        {
            _fxEntities = fxEntities;
        }

        public IEnumerable<FXEntities.InterestRate> GetInterestRates(string currencyCode)
        {
            var result = from record in _fxEntities.InterestRates
                            where record.Currency.CurrencyCode == currencyCode
                            select record;

            return result;
        }

        public IEnumerable<FXEntities.InterestRate> GetInterestRates()
        {
            var result = from record in _fxEntities.InterestRates
                            select record;

            return result;
        }

        public decimal GetInterestRate(string currencyCode, DateTime date)
        {
            var tarRecord = GetInterestRates(currencyCode).Where(d => d.Date == date).FirstOrDefault();
            if (tarRecord != null)
                return tarRecord.Value;
            else
                throw new Exception(string.Format("No interest rate for {0} at date {1:d}",currencyCode,date));
        }

        /// <summary>
        /// Interest rate of variable currency subtracts interest rate of base currency 
        /// </summary>
        /// <param name="baseCurrencyCode"></param>
        /// <param name="variableCurrencyCode"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public decimal GetInterestRateDiff(string baseCurrencyCode, string variableCurrencyCode, DateTime date)
        {
            return GetInterestRate(variableCurrencyCode, date) - GetInterestRate(baseCurrencyCode, date);
        }
    }
}
