using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace FXStrategy.EvaluationContext
{
    public class ValuesTable : Dictionary<string, object>
    {
        public ValuesTable(int capacity) : base(capacity) { }
        public object this[string name]
        {
            get { 
                object result;
                if (!this.TryGetValue(name, out result))
                {
                    throw new Exception("Undeclared variable: " + name);
                }
                return result;
            }
            set { base[name] = value; }
        }

    }//class

}
