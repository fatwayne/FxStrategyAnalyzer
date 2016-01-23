using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Irony.StrategyData
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

        public T AtTime(DateTime time)
        {
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
