using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.DataAccess
{
    public class CurrencyAdapter
    {
        FXEntities.FXEntities _fxEntities;

        public CurrencyAdapter(FXEntities.FXEntities fxEntities)
        {
            _fxEntities = fxEntities;
        }

        public List<FXEntities.Currency> GetCurrencies()
        {
            var result = from record in _fxEntities.Currencies
                            select record;

            return result.ToList();
        }
    }
}
