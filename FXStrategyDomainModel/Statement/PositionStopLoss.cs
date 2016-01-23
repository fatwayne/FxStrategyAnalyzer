using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.MetaModel
{
    public class PositionStopLoss : PositionOperation
    {
        public PositionStopLoss(Variable position)
        {
            _position = position;
        }

        private Variable _position;

        public Variable Position
        {
            get { return _position; }
        }
       
    }
}
