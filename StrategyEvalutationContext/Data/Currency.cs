using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.EvaluationContext
{
    public class Currency
    {
        private string _name;

        public Currency(string name)
        {
            this._name = name;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Currency))
                return false;
            else
                return this == obj || (((Currency)obj).Name == this.Name);

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
