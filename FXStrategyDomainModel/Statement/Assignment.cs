using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.MetaModel
{
    public class Assignment : Statement
    {
        public Assignment(Variable target, Expression expression)
        {
            _target = target;
            _expression = expression;
        }

        private Variable _target;

        public Variable Target
        {
            get { return _target; }
            set { _target = value; }
        }

        private Expression _expression;

        public Expression Expression
        {
            get { return _expression; }
            set { _expression = value; }
        }

    }
}
