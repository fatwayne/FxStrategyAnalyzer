using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.EvaluationContext;
using FXStrategy.DataAccess;

namespace FXStrategy.Interpreter
{
    /// <summary>
    /// Represents runtime of a position set
    /// </summary>
    public class PositionSetRuntime
    {
        private List<PositionRuntime> _positions;
        public List<PositionRuntime> Positions
        {
            get { return _positions; }
        }

        private Currency _baseCurrency;

        public Currency BaseCurrency
        {
            get { return _baseCurrency; }
            set { _baseCurrency = value; }
        }

        public int Capacity;

        public PositionSetRuntime(int capacity, PositionType positionType, Currency baseCurrency)
        {
            Capacity = capacity;
            PositionType = positionType;
            _positions = new List<PositionRuntime>(capacity);
            for(int i = 0; i < Capacity; i++){
                _positions.Add(new PositionRuntime(positionType) { Status = PositionRuntimeStatus.Closed });
            }

            _baseCurrency = baseCurrency;
        }

        /// <summary>
        /// Open a position for a currency
        /// A position is not created, find a closed position to open
        /// </summary>
        /// <param name="currency">Currency for open</param>
        /// <param name="time">The date for open the position</param>
        public void OpenPosition(Currency currency, DateTime date)
        {
            var position = _positions.Where(p => p.Status == PositionRuntimeStatus.Closed).FirstOrDefault();

            if (position == null)
            {
                throw new Exception("All positions all actively in used. Please make sure there are some available position(s) before opening a new one.");
            }

            position.OpenPosition(currency, _baseCurrency, date);

        }

        public string Name { get; set; }

        public PositionType PositionType { get; set; }
    }
}
