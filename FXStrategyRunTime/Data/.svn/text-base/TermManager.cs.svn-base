using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.MetaModel;

namespace FXStrategy.Interpreter.Data
{
    public class TermManager
    {
        static public List<Term> GetTermFromPeriod(
            DateTime positionStartDate, DateTime positionEndDate,
            PeriodicTimeDefinition termLength
            )
        {

            List<Term> resultTerms = new List<Term>();

            var availableTermDates = termLength.AvailableDates;

            // the date of the first term
            var firstTermEndDate = availableTermDates.Where(d => d > positionStartDate).Min();

            var lastTermEndDate = availableTermDates.Where(d => d >= positionEndDate).Min();

            var effectiveDates =  availableTermDates
                .Where(d => d >= positionStartDate && d <= lastTermEndDate);

            for (int i = 0; i < effectiveDates.Count() ; i++)
            {
                if (i == 0)
                    resultTerms.Add(new Term(positionStartDate, firstTermEndDate));
                else if( effectiveDates.ElementAtOrDefault(i+1) != DateTime.MinValue)
                    resultTerms.Add(
                        new Term(effectiveDates.ElementAt(i), 
                            effectiveDates.ElementAt(i + 1)
                            ));
            }

            return resultTerms;
        }

    
    }
}
