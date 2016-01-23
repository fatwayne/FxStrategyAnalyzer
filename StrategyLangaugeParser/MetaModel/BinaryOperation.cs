using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrategyLanguageParser.MetaModel
{
    public abstract class BinaryOperation : OperationExpression
    {

        public override Type Type
        {
            get
            {
                return new Bool();
            }
        }
    }

    public class And : BinaryOperation
    {
        public override object Eval()
        {
            return (bool)LeftExpression.Eval() && (bool)RightExpression.Eval();
        }
    }

    public class Or : BinaryOperation
    {
        public override object Eval()
        {
            return (bool)LeftExpression.Eval() || (bool)RightExpression.Eval();
        }
    }

    public class Equal : BinaryOperation
    {
        public override object Eval()
        {
            return (bool)LeftExpression.Eval() == (bool)RightExpression.Eval();
        }
    }

    public class NotEqual : BinaryOperation
    {
        public override object Eval()
        {
            return (bool)LeftExpression.Eval() != (bool)RightExpression.Eval();
        }
    }



}
