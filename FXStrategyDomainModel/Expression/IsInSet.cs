using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.EvaluationContext;

namespace FXStrategy.MetaModel
{
    /// <summary>
    /// Left expression as element
    /// Right expression as set
    /// </summary>
    public class IsInSet : BooleanExpression
    {

        public override object Eval(Context evaluationContext)
        {
            var set = (IEnumerable<object>) RightExpression.Eval(evaluationContext);
            var element = LeftExpression.Eval(evaluationContext);

            return set.Contains(element);
        }
    }
}
