using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.MetaModel
{
    public class StopLossRule : FXStrategy.MetaModel.ConditionalRule
    {
        public StopLossRule(string name,
            TimeIntervalDefinition executeFrequency,
            Variable iterator,
            Statement statement,
            BooleanExpression stopLossCondition,
            Variable positionSet)

            : base(name, executeFrequency,
            // wrap the statement with for all loop
            // and null checking for position
               new ForAllStatement(
                    iterator, positionSet,
                    new IfStatement(new NotEqual(){
                        LeftExpression = new PropertyAccessor(iterator,"Currency"),
                        RightExpression = new Constant(typeof(DBNull),null)},
                        new CompositeStatement(
                            new List<Statement>{
                                    statement,
                                    new IfStatement(stopLossCondition,new PositionStopLoss(iterator))
                                }
                        )            
                    )
                    
                ), stopLossCondition, positionSet
            
            
            )
        {
        }

       

      
    }
}
