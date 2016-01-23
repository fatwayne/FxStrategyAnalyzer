using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.MetaModel
{
    /// <summary>
    /// represents a for all statement
    /// </summary>
    public class ForAllStatement : Statement
    {
        public ForAllStatement(Variable iterator, Expression collection, Statement innerStatement)
        {
            _iterator = iterator;
            _collection = collection;
            _innerStatement = innerStatement;
        }

        private Variable _iterator;

        public Variable Iterator
        {
            get { return _iterator; }
        }

        private Expression _collection;

        public Expression Collection
        {
            get { return _collection; }
        }

        private Statement _innerStatement;

        public Statement InnerStatement
        {
            get { return _innerStatement; }
        }

    
    }
}
