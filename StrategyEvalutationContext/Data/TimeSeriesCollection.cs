using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.EvaluationContext
{
    /// <summary>
    /// Collection for time series data
    /// </summary>
    /// <typeparam name="T">ITimeSeriesData</typeparam>
    public class TimeSeriesCollection<T> : IEnumerable<T> where T : TimeSeriesData
    {
        private Dictionary<DateTime, T> _progressDataDict;

        public Dictionary<DateTime, T> ProgressDataDict
        {
            get {
                if (_progressDataDict == null)
                    _progressDataDict = new Dictionary<DateTime, T>();
                return _progressDataDict; }
            set { _progressDataDict = value; }
        }


        public T GetLocalMax(DateTime startDate, DateTime endDate){
            return ProgressDataDict.Values.
                Where(e => e.Time >= startDate && e.Time <= endDate).
                OrderByDescending(e => e.Value).First() ;
        }

        /// <summary>
        /// Get maximum value from the beginning to the specified date (endDate)
        /// </summary>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public T GetLocalMax(DateTime endDate)
        {
            return ProgressDataDict.Values.
                Where(e => e.Time <= endDate).
                OrderByDescending(e => e.Value).First();
        }

        /// <summary>
        /// Simple moving average
        /// </summary>
        /// <param name="date"></param>
        /// <param name="numberOfDay"></param>
        /// <returns></returns>
        public decimal MovingAvg(DateTime date, int numberOfDay)
        {
            var data =  ProgressDataDict.Values.
                Where(e => e.Time <= date && e.Time >= date.AddDays(numberOfDay));

            if (data == null || data.Count() == 0)
                return 0;
            else
                return data.Average(v => v.Value);
        }

        public void AddTimeSeriesData(T data)
        {
            ProgressDataDict.Add(data.Time, data);
        }

        public void AddTimeSeriesData(IEnumerable<T> data)
        {
            foreach (var d in data)
            {
                AddTimeSeriesData(d);
            }
        }

        public decimal At(DateTime time)
        {
            return AtTime(time).Value;
        }

        public T AtTime(DateTime time)
        {
            if (!ProgressDataDict.Keys.Contains(time))
                throw new ArgumentException("There is no element at time: " + time);

            return ProgressDataDict[time];
        }
        

        #region IEnumerable<ITimeSeriesData> Members

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T element in ProgressDataDict.Values)
                yield return element;
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            foreach (T element in ProgressDataDict.Values)
                yield return element;
        }

        #endregion
    }
}
