using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.MetaModel
{
    public class ClosePosition : PositionOperation
    {
        public ClosePosition(Expression position)
        {
            _position = position;
        }

        private Expression _position;

        public Expression Position
        {
            get { return _position; }
        }
       

    }
}
