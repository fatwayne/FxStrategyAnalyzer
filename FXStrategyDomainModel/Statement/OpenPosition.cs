using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.MetaModel
{
    public class OpenPosition : PositionOperation
    {
        public OpenPosition(Variable positionSet, Expression currency)
        {
            if (positionSet.Type != typeof(PositionSet))
                throw new Exception("Only with type PositionSet is accepted");

            _positionSet = positionSet;
            _currency = currency;
        }

        private Variable _positionSet;

        public Variable PositionSet
        {
            get { return _positionSet; }
        }


        private Expression _currency;

        public Expression Currency
        {
            get { return _currency; }
        }

    }
}
