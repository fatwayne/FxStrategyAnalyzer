using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Irony.StrategyData
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// Get all weekdays between a period
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static List<DateTime> GetWeekdaysDate(DateTime startDate, DateTime endDate)
        {
            DateTime tempDate = startDate;
            List<DateTime> result = new List<DateTime>();
            while (tempDate <= endDate)
            {
                if (tempDate.DayOfWeek != DayOfWeek.Saturday && tempDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    result.Add(tempDate);
                }
                tempDate = tempDate.AddDays(1);
            }
            return result;
        }
    }
}
