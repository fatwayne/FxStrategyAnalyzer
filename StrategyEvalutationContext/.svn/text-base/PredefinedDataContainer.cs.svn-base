using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.DataAccess;

namespace FXStrategy.EvaluationContext
{
    /// <summary>
    /// The class contains predefined data,
    /// such as Top3Currencies - top 3 currencies in term of interest rate
    /// and Bottom3Currencies - bottom 3 currencies in term of interest rate
    /// </summary>
    public class PredefinedDataContainer
    {
        private Dictionary<string, object> _containerTable;

        public PredefinedDataContainer()
        {
            _containerTable = new Dictionary<string, object>();
        }

        public IEnumerable<object> GetData(string sourceName, DateTime time)
        {
            object source;
            if (_containerTable.TryGetValue(sourceName, out source))
            {
                if (source is CurrencyNameSource)
                {
                    List<string> result = ((CurrencyNameSource)source).GetCurrencies(time).ToList();
                    return result;
                }
                else
                    throw new Exception("The source type is not recognized: " + source.GetType());
            }
            else
                throw new Exception("Source name is not defined: " + sourceName);
        }

        public void Add(CurrencyNameSource newCurrencyNameSource)
        {
            _containerTable.Add(newCurrencyNameSource.SourceName, newCurrencyNameSource);
        }
    }
}
