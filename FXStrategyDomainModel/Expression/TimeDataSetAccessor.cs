using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.MetaModel
{
    public abstract class TimeDataSetAccessor : Expression
    {
        public override Type Type
        {
            get { return typeof(TimeDataSet); }
        }

       
    }

   
}
