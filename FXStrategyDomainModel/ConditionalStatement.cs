using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.MetaModel
{
    public class ConditionalStatement : Statement
    {
        public ConditionalStatement(Statement innerStatement)
        {
            _innerStatement = innerStatement;
        }

        private Statement _innerStatement;

        public Statement InnerStatement
        {
            get { return _innerStatement; }
        }
      
    }
}
