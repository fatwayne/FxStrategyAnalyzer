using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.EvaluationContext
{
    public class TimeSeriesData
    {
        private DateTime _time;

        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }

        private decimal _value;

        public decimal Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
