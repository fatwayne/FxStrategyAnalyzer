using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.EvaluationContext;

namespace FXStrategy.MetaModel
{
    public class Constant : Expression
    {
        public Constant(Type type, object value)
        {
            _type = type;
            Value = value;
        }

        public object Value { get; set; }

        private Type _type;
        public override Type Type
        {
            get { return Value.GetType(); }
        }

        public override object Eval(Context evaluationContext)
        {
            return Value;
        }
            
    }
}
