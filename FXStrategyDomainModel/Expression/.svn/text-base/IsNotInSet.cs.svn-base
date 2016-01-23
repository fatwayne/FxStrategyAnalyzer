using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.EvaluationContext;

namespace FXStrategy.MetaModel
{
    public class IsNotInSet : BooleanExpression
    {
        public override object Eval(Context evaluationContext)
        {
            var set = (IEnumerable<object>)RightExpression.Eval(evaluationContext);
            var element = LeftExpression.Eval(evaluationContext);

            if (set.All(s => s == null))
                return true;

            return !set.Contains(element); 
        }
    }
}
