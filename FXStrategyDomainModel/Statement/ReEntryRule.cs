using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.MetaModel
{
    public class StopLossReEntryRule : ConditionalRule
    {
        public StopLossReEntryRule(string name, TimeIntervalDefinition executeFrequency, Statement statement
            ,Variable positionSet, BooleanExpression stopLossCondition)
            : base(name, executeFrequency, statement, stopLossCondition, positionSet) {
        }

    }
}
