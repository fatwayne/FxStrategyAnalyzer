using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.MetaModel
{
    public abstract class TimeDataSetOperation : Expression
    {
        public TimeDataSetOperation(TimeDataSetAccessor timeDataSetAccessor)
        {
            _timeDataSetAccessor = timeDataSetAccessor;
        }

        protected TimeDataSetAccessor _timeDataSetAccessor;

        public TimeDataSetAccessor TimeDataSetAccessor
        {
            get { return _timeDataSetAccessor; }
        }
    }
}
