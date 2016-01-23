using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrategyLanguageParser.MetaModel
{
    public abstract class OperationExpression : Expression
    {
        public Expression LeftExpression
        {
            get;
            set;
        }

        public Expression RightExpression
        {
            get;
            set;
        }


    }

  
}
