using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrategyLanguageParser.MetaModel
{
    public class ConditionalStatement : Statement
    {
        public BinaryOperation ConditionExpr { get; set; } 

        public List<Statement> InnerStatements
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
