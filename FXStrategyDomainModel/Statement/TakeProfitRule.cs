using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.MetaModel
{
    public class TakeProfitRule : ConditionalRule
    {
        public TakeProfitRule(string name, TimeIntervalDefinition executeFrequency, Statement statement, BooleanExpression condition, Variable positionSet)
        : base(name, executeFrequency, statement, condition, positionSet){ }
    }
}
