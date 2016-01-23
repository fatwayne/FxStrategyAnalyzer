using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrategyLanguageParser.MetaModel
{
    public class Variable : Expression
    {
        public Variable(string name, Type type, object value)
        {
            Name = name;
            _type = type;
            Value = value;
        }


        public string Name
        {
            get;
            set;
        }

        private Type _type;
        public override Type Type
        {
            get{
                return _type;   
            }

        }

        public object Value { get; set; }

        public override object Eval()
        {
            return Value;
        }
    }
}
