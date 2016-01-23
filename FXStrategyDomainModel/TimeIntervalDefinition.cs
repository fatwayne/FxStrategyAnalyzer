using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util;
using FXStrategy.EvaluationContext;

namespace FXStrategy.MetaModel
{
    public abstract class TimeIntervalDefinition
    {
        public virtual void Initialize(DateTime startDate, DateTime endDate, Context evaluationContext){
            Initialize(startDate,endDate);
        }

        public abstract void Initialize(DateTime startDate, DateTime endDate);

        public virtual bool CanExecute(DateTime currentDate)
        {
            return true;
            //return AvailableDates.Contains(currentDate);
        }

        protected List<DateTime> _availableDates;

        public virtual List<DateTime> AvailableDates
        {
            get
            {
                //if (_availableDates == null)
                //    throw new Exception("Initialize function has to be called before getting AvailableDates");
                return _availableDates;
            }
        }
    }

    public class ConcreteTimeDefinition : TimeIntervalDefinition
    {
        public DateTime ExecuteTime { get; set; }
        public override bool CanExecute(DateTime currentDate)
        {
            return (ExecuteTime == currentDate);
        }

        public override void Initialize(DateTime startDate, DateTime endDate)
        {
            _availableDates = new List<DateTime>();
            _availableDates.Add(ExecuteTime);
        }

      
    }

    public class PeriodicTimeDefinition : TimeIntervalDefinition
    {
        public PeriodicTimeDefinition(int length, PeriodicType periodicType)
        {
            _length = length;
            _periodicType = periodicType;
        }

        public override bool CanExecute(DateTime currentDate)
        {
            if (currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday)
                return false;

            return base.CanExecute(currentDate);
        }

        public PeriodicTimeDefinition(Expression length, PeriodicType periodicType)
        {
            _lengthExpr = length;
            _periodicType = periodicType;
        }

        public override void Initialize(DateTime startDate, DateTime endDate, Context evaluationContext)
        {
            _length = (int) _lengthExpr.Eval(evaluationContext);
            Initialize(startDate, endDate);
        } 

        public override void Initialize(DateTime startDate, DateTime endDate)
        {
            _availableDates = new List<DateTime>();
            if (PeriodicType == PeriodicType.Month)
            {
                DateTime tempDate;
                for (tempDate = startDate; tempDate <= endDate; tempDate = tempDate.AddMonths(Length))
                {
                    _availableDates.Add(tempDate);
                }

                if (!_availableDates.Contains(tempDate))
                    _availableDates.Add(tempDate);
            }
            else if (PeriodicType == PeriodicType.Day)
            {
                for (DateTime tempDate = startDate; tempDate <= endDate; tempDate = tempDate.AddDays(Length))
                {
                    _availableDates.Add(tempDate);
                }
            }
        }

        private Expression _lengthExpr;

        public Expression LengthExpr
        {
            get { return _lengthExpr; }
        }

        private int _length;
        public int Length { get {
            return _length;
        } }

        private PeriodicType _periodicType;
        public PeriodicType PeriodicType { get {
            return _periodicType;
        } }

    }

    public class WeekDayTimeDefinition : TimeIntervalDefinition
    {
        public DayOfWeek DayOfWeek { get; set; }

        public override void Initialize(DateTime startDate, DateTime endDate)
        {
            _availableDates = new List<DateTime>();
            _startDate = startDate;
        }

        private DateTime _startDate;

        public override bool CanExecute(DateTime currentDate)
        {
            var daysInBetween = DateTimeHelper.GetWeekdaysDate(_startDate, currentDate);
            if (!daysInBetween.Contains(currentDate))
                return false;

            return (currentDate.DayOfWeek == DayOfWeek);

        }
    }
    

    public enum PeriodicType
    {
        Month,
        Week,
        Day
    }

}
