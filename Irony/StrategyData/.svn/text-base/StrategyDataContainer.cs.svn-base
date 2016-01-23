using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Irony.StrategyData
{
    public class StrategyDataContainer
    {
        private Dictionary<string, object> _containerTable;

        public StrategyDataContainer()
        {
            _containerTable = new Dictionary<string, object>();
        }

        public IEnumerable<object> GetData(string sourceName, DateTime time)
        {
            object source;
            if (_containerTable.TryGetValue(sourceName, out source))
            {
                if (source.GetType() == typeof(CurrencyNameSource))
                {
                    return ((CurrencyNameSource)source).GetCurrencies(time);
                }
                else
                    throw new Exception("The source type is not recognized: " + source.GetType());
            }
            else
                throw new Exception("Source name is not defined: " + sourceName);
        }

    }
}
