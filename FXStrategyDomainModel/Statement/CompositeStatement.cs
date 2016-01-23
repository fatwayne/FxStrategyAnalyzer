using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.MetaModel
{
    public class CompositeStatement : Statement
    {
        public CompositeStatement(List<Statement> statements)
        {
            _statements = statements;
        }

        private List<Statement> _statements;
        public List<Statement> Statements
        {
            get
            {
                return _statements;
            }
        }
    }
}
