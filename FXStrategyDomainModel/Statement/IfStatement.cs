using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.MetaModel
{
    public class IfStatement : Statement
    {
        public IfStatement(Expression conditionalExpression,
          Statement ifTrueStatement)
            : this(conditionalExpression, ifTrueStatement, null)
        {
        }

        public IfStatement(Expression conditionalExpression, 
            Statement ifTrueStatement,
            Statement ifFalseStatement)
        {
            _conditionalExpression = conditionalExpression;
            _ifFalseStatement = ifFalseStatement;
            _innerStatement = ifTrueStatement;
        }


        private Expression _conditionalExpression;

        public Expression ConditionalExpression
        {
            get { return _conditionalExpression; }
        }

        private Statement _ifFalseStatement;

        public Statement IfFalseStatement
        {
            get { return _ifFalseStatement; }
        }

        private Statement _innerStatement;

        public Statement InnerStatement
        {
            get { return _innerStatement; }
        }

        //public override void Eval(evaluationContext.ValuesTable evaluationContext.ValuesTable, DateTime time)
        //{
        //    if ( (bool) ConditionalExpression.Eval(evaluationContext.ValuesTable, time) == true)
        //    {
        //        InnerStatements.ForEach(s => s.Eval(evaluationContext.ValuesTable, time));
        //    }
        //    else if (IfFalseStatements != null)
        //    {
        //        IfFalseStatements.ForEach(s => s.Eval(evaluationContext.ValuesTable, time));
        //    }
        //}
    }
}
