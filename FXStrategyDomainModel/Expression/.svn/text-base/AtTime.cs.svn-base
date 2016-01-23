using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.MetaModel
{
    public class AtTime : TimeDataSetOperation
    {
        public AtTime(TimeDataSetAccessor timeDataSetAccessor, Expression requestDate) 
            :base(timeDataSetAccessor)
        {
            _requestDate = requestDate;
        }

        private Expression _requestDate;

        public Expression RequestDate
        {
            get { return _requestDate; }
        }

        public override Type Type
        {
            get { return typeof(decimal); }
        }

        public override object Eval(EvaluationContext.Context evaluationContext)
        {
            DateTime requestDate = (DateTime)_requestDate.Eval(evaluationContext);

            if (TimeDataSetAccessor is ExchangeRateAccessor)
            {
                FXStrategy.DataAccess.TimeSeriesCollection<FXStrategy.DataAccess.ExchangeRateBid> timeSeries = (FXStrategy.DataAccess.TimeSeriesCollection<FXStrategy.DataAccess.ExchangeRateBid>)_timeDataSetAccessor.Eval(evaluationContext);
                return timeSeries.At(requestDate);
            }
            else if (TimeDataSetAccessor is InterestRateAccessor)
            {
                FXStrategy.DataAccess.TimeSeriesCollection<FXStrategy.DataAccess.InterestRate> timeSeries = (FXStrategy.DataAccess.TimeSeriesCollection<FXStrategy.DataAccess.InterestRate>)_timeDataSetAccessor.Eval(evaluationContext);
                return timeSeries.At(requestDate);
            }
            else
                throw new Exception("Unhandled TimeDataSetAccessor: " + TimeDataSetAccessor.GetType().Name);
        }
    }
}
