using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FXStrategy.MetaModel
{
    /// <summary>
    /// Expression for getting moving average from time series data
    /// </summary>
    public class MovingAverage : TimeDataSetOperation
    {
        public MovingAverage(TimeDataSetAccessor timeDataSetAccessor, PeriodicTimeDefinition length, Expression requestDate)
            :base(timeDataSetAccessor)
        {
            _length = length;
            _requestDate = requestDate;
        }

        private PeriodicTimeDefinition _length;

        public PeriodicTimeDefinition NumberOfDays
        {
            get { return _length; }
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
            int numberOfDays = (int)_length.LengthExpr.Eval(evaluationContext);
            if (_length.PeriodicType == PeriodicType.Month)
            {
                numberOfDays *= 30;
            }
            
            DateTime requestDate = (DateTime) _requestDate.Eval(evaluationContext);


            if (TimeDataSetAccessor is ExchangeRateAccessor)
            {
                FXStrategy.DataAccess.TimeSeriesCollection<FXStrategy.DataAccess.ExchangeRateBid> timeSeries
                    = (FXStrategy.DataAccess.TimeSeriesCollection<FXStrategy.DataAccess.ExchangeRateBid>)
                    _timeDataSetAccessor.Eval(evaluationContext);
                return timeSeries.MovingAvg(requestDate, numberOfDays);          
            }
            else if (TimeDataSetAccessor is InterestRateAccessor)
            {
                FXStrategy.DataAccess.TimeSeriesCollection<FXStrategy.DataAccess.InterestRate> timeSeries 
                    = (FXStrategy.DataAccess.TimeSeriesCollection<FXStrategy.DataAccess.InterestRate>)
                    _timeDataSetAccessor.Eval(evaluationContext);
                return timeSeries.MovingAvg(requestDate, numberOfDays);          
            }
            else
                throw new Exception("Unhandled TimeDataSetAccessor: " + TimeDataSetAccessor.GetType().Name);
        }
    }
}
