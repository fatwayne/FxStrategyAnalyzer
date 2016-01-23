using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.EvaluationContext;

namespace FXStrategy.MetaModel
{
    public abstract class ArithmeticExpression : OperationExpression
    {

    }

    public class Sum : ArithmeticExpression
    {
        public override Type Type
        {
            get
            {
                if (LeftExpression.Type == typeof(int) && RightExpression.Type == typeof(int))
                    return typeof(int);
                else if (LeftExpression.Type == typeof(decimal) || RightExpression.Type == typeof(decimal))
                    return typeof(decimal);
                else
                    throw new Exception("Unknown Type.");
            }
        }

        public override object Eval(Context evaluationContext)
        {
            if (LeftExpression.Type == typeof(int) && RightExpression.Type == typeof(int))
                return (int)LeftExpression.Eval(evaluationContext) + (int)RightExpression.Eval(evaluationContext);
            else if (LeftExpression.Type == typeof(decimal) || RightExpression.Type == typeof(decimal))
                return (decimal)LeftExpression.Eval(evaluationContext) + (decimal)RightExpression.Eval(evaluationContext);
            else
                throw new Exception("Unknown Type.");
        }


    }

    public class Multiply : ArithmeticExpression
    {
        public override Type Type
        {
            get
            {
                if (LeftExpression.Type == typeof(int) && RightExpression.Type == typeof(int))
                    return typeof(int);
                else if (LeftExpression.Type == typeof(decimal) || RightExpression.Type == typeof(decimal))
                    return typeof(decimal);
                else
                    throw new Exception("Unknown Type.");
            }
        }

        public override object Eval(Context evaluationContext)
        {
            if (LeftExpression.Type == typeof(int) && RightExpression.Type == typeof(int))
                return (int)LeftExpression.Eval(evaluationContext) * (int)RightExpression.Eval(evaluationContext);
            else if (LeftExpression.Type == typeof(decimal) || RightExpression.Type == typeof(decimal))
                return (decimal)LeftExpression.Eval(evaluationContext) * (decimal)RightExpression.Eval(evaluationContext);
            else
                throw new Exception("Unknown Type.");
        }

    }

    public class Subtract : ArithmeticExpression
    {
        public override Type Type
        {
            get
            {
                if (LeftExpression.Type == typeof(int) && RightExpression.Type == typeof(int))
                    return typeof(int);
                else if (LeftExpression.Type == typeof(decimal) || RightExpression.Type == typeof(decimal))
                    return typeof(decimal);
                else
                    throw new Exception("Unknown Type.");
            }
        }

        public override object Eval(Context evaluationContext)
        {
            if (LeftExpression.Type == typeof(int) && RightExpression.Type == typeof(int))
                return (int)LeftExpression.Eval(evaluationContext) - (int)RightExpression.Eval(evaluationContext);
            else if (LeftExpression.Type == typeof(decimal) || RightExpression.Type == typeof(decimal))
                return (decimal)LeftExpression.Eval(evaluationContext) - (decimal)RightExpression.Eval(evaluationContext);
            else
                throw new Exception("Unknown Type.");
        }
    }

    public class Divide : ArithmeticExpression
    {
        public override Type Type
        {
            get
            {
                if (LeftExpression.Type == typeof(int) && RightExpression.Type == typeof(int))
                    return typeof(int);
                else if (LeftExpression.Type == typeof(decimal) || RightExpression.Type == typeof(decimal))
                    return typeof(decimal);
                else
                    throw new Exception("Unknown Type.");
            }
        }

        public override object Eval(Context evaluationContext)
        {
            if (LeftExpression.Type == typeof(int) && RightExpression.Type == typeof(int))
                return (int)LeftExpression.Eval(evaluationContext) / (int)RightExpression.Eval(evaluationContext);
            else if (LeftExpression.Type == typeof(decimal) || RightExpression.Type == typeof(decimal))
                return (decimal)LeftExpression.Eval(evaluationContext) / (decimal)RightExpression.Eval(evaluationContext);
            else
                throw new Exception("Unknown Type.");
        }
    }
}
