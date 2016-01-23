using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.EvaluationContext;

namespace FXStrategy.MetaModel
{
    public abstract class BooleanExpression : OperationExpression
    {

        public override Type Type
        {
            get
            {
                return typeof(bool);
            }
        }
    }

    public class And : BooleanExpression
    {
        public override object Eval(Context evaluationContext)
        {
            if (LeftExpression.Type == typeof(bool) && RightExpression.Type == typeof(bool))
                return (bool)LeftExpression.Eval(evaluationContext) && (bool)RightExpression.Eval(evaluationContext);
            else
                throw new Exception("Unknown Type.");
        }
    }

    public class Or : BooleanExpression
    {
        public override object Eval(Context evaluationContext)
        {
            if (LeftExpression.Type == typeof(bool) && RightExpression.Type == typeof(bool))
                return (bool)LeftExpression.Eval(evaluationContext) || (bool)RightExpression.Eval(evaluationContext);
            else
                throw new Exception("Unknown Type.");
        }
    }

    public class Equal : BooleanExpression
    {
        public override object Eval(Context evaluationContext)
        {

            return LeftExpression.Eval(evaluationContext) == RightExpression.Eval(evaluationContext);
        }
    }

    public class NotEqual : BooleanExpression
    {
        public override object Eval(Context evaluationContext)
        {
            return LeftExpression.Eval(evaluationContext) != RightExpression.Eval(evaluationContext);
        }
    }

    public class GreaterThan : BooleanExpression
    {
        public override object Eval(Context evaluationContext)
        {
            object left = LeftExpression.Eval(evaluationContext);
            object right = RightExpression.Eval(evaluationContext);

            if (left.GetType() == typeof(decimal))
            {
                if (right.GetType() == typeof(int))
                    return (decimal)left > (int)right;
                else
                    return (decimal)left > (decimal)right;
            }
            else
            {
                if (right.GetType() == typeof(int))
                    return (int)left > (int)right;
                else
                    return (int)left > (decimal)right;
            }

        }
    }

    public class GreaterThanOrEq : BooleanExpression
    {
        public override object Eval(Context evaluationContext)
        {
            object left = LeftExpression.Eval(evaluationContext);
            object right = RightExpression.Eval(evaluationContext);

            if (left.GetType() == typeof(decimal))
            {
                if (right.GetType() == typeof(int))
                    return (decimal)left >= (int)right;
                else
                    return (decimal)left >= (decimal)right;
            }
            else
            {
                if (right.GetType() == typeof(int))
                    return (int)left >= (int)right;
                else
                    return (int)left >= (decimal)right;
            }
        }
    }

    public class LessThan : BooleanExpression
    {
        public override object Eval(Context evaluationContext)
        {
            object left = LeftExpression.Eval(evaluationContext);
            object right = RightExpression.Eval(evaluationContext);

            if (left.GetType() == typeof(decimal))
            {
                if (right.GetType() == typeof(int))
                    return (decimal)left < (int)right;
                else
                    return (decimal)left < (decimal)right;
            }
            else
            {
                if (right.GetType() == typeof(int))
                    return (int)left < (int)right;
                else
                    return (int)left < (decimal)right;
            }
        }
    }

    public class LessThanOrEq : BooleanExpression
    {
        public override object Eval(Context evaluationContext)
        {
            object left = LeftExpression.Eval(evaluationContext);
            object right = RightExpression.Eval(evaluationContext);

            if (left.GetType() == typeof(decimal))
            {
                if (right.GetType() == typeof(int))
                    return (decimal)left <= (int)right;
                else
                    return (decimal)left <= (decimal)right;
            }
            else
            {
                if (right.GetType() == typeof(int))
                    return (int)left <= (int)right;
                else
                    return (int)left <= (decimal)right;
            }
        }
    }

}
