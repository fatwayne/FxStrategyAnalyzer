using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrategyLanguageParser.MetaModel
{
    public abstract class ArithmeticExpression : OperationExpression
    {
        public override Type Type
        {
            get
            {
                if (LeftExpression.Type == Type.Integer && RightExpression.Type == Type.Integer)
                    return Type.Integer;
                else if (LeftExpression.Type == Type.Decimal && RightExpression.Type == Type.Decimal)
                    return Type.Decimal;
                else
                    throw new Exception("Operation between different primitive type is not allowed");
            }
        }
    }

    public class Sum : ArithmeticExpression
    {
        public override object Eval()
        {
            if (LeftExpression.Type == Type.Integer && RightExpression.Type == Type.Integer)
                return (int)LeftExpression.Eval() + (int)RightExpression.Eval();
            else if (LeftExpression.Type == Type.Decimal && RightExpression.Type == Type.Decimal)
                return (decimal)LeftExpression.Eval() + (decimal)RightExpression.Eval();
            else
                throw new Exception("Operation between different primitive type is not allowed");
        }


    }

    public class Multiply : ArithmeticExpression
    {
        public override object Eval()
        {
            if (LeftExpression.Type == Type.Integer && RightExpression.Type == Type.Integer)
                return (int)LeftExpression.Eval() * (int)RightExpression.Eval();
            else if (LeftExpression.Type == Type.Decimal && RightExpression.Type == Type.Decimal)
                return (decimal)LeftExpression.Eval() * (decimal)RightExpression.Eval();
            else
                throw new Exception("Operation between different primitive type is not allowed");
        }


    }

    public class Subtract : ArithmeticExpression
    {
        public override object Eval()
        {
            if (LeftExpression.Type == Type.Integer && RightExpression.Type == Type.Integer)
                return (int)LeftExpression.Eval() - (int)RightExpression.Eval();
            else if (LeftExpression.Type == Type.Decimal && RightExpression.Type == Type.Decimal)
                return (decimal)LeftExpression.Eval() - (decimal)RightExpression.Eval();
            else
                throw new Exception("Operation between different primitive type is not allowed");
        }
    }
}
