using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.MetaModel
{
    public abstract class ConditionalRule : TradingRule
    {
        public ConditionalRule(string name, TimeIntervalDefinition executeFrequency, Statement statement, BooleanExpression condition, Variable positionSet) :
            base(name, executeFrequency, statement)
        {
            _condition = condition;
            _positionSet = positionSet;
        }
        private BooleanExpression _condition;

        public BooleanExpression Condition
        {
            get { return _condition; }
        }

        private Variable _positionSet;

        public Variable PositionSet
        {
            get { return _positionSet; }
        }
    }
}
