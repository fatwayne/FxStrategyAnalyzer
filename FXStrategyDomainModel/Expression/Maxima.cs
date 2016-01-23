using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.MetaModel
{
    public class Maxima : TimeDataSetOperation
    {
        public Maxima(TimeDataSetAccessor timeDataSetAccessor) : base(timeDataSetAccessor) { }


        public override Type Type
        {
            get { throw new NotImplementedException(); }
        }

        public DateTime RequestDate
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int NumberOfDays
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public override object Eval(EvaluationContext.Context evaluationContext)
        {
            throw new NotImplementedException();
        }
    }
}
