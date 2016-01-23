using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Irony.StrategyData
{
    public class InterestRate : TimeSeriesData
    {
        private Currency _currency;

        public Currency Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }
      
    }
}
