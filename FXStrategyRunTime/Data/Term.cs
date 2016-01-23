using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.Interpreter.Data
{
    public class Term
    {
        public Term(DateTime startDate, DateTime endDate)
        {
            _startDate = startDate;
            _endDate = endDate;
        }

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public bool IsWithinTerm(DateTime date)
        {
            return (date >= StartDate && date <= EndDate);
        }


    }
}
