using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.EvaluationContext;
using FXStrategy.DataAccess;
namespace FXStrategy.Interpreter
{
    /// <summary>
    /// Represents a position record
    /// </summary>
    public class PositionRecord
    {

        private DateTime _startDate;
        /// <summary>
        /// The date  opening the position
        /// </summary>
        public DateTime StartDate
        {
            get { return _startDate; }
        }
        /// <summary>
        /// The date closing the position
        /// </summary>
        public DateTime EndDate
        {
            get;
            set;
        }

        private Currency _currencyInPosition;
        /// <summary>
        /// The currency that was held in this period
        /// </summary>
        public Currency CurrencyInPosition
        {
            get { return _currencyInPosition; }
        }

        private Currency _baseCurrency;
        public Currency BaseCurrency
        {
            get { return _baseCurrency; }
        }

        private PositionRuntime _positionRuntime;

        public PositionRuntime PositionRuntime
        {
            get { return _positionRuntime; }
        }

        public PositionRecord(DateTime startTradeTime, 
            DateTime endTradeTime, Currency currencyToTrade, 
            Currency baseCurrency, PositionType type, PositionRuntime positionRuntime):
            this(startTradeTime, currencyToTrade, baseCurrency,  type, positionRuntime){
                this.EndDate = endTradeTime;
        }

        public PositionRecord(DateTime startTradeTime, Currency currencyToTrade,
            Currency baseCurrency, PositionType type, PositionRuntime positionRuntime)
        {
            _currencyInPosition = currencyToTrade;
            _startDate = startTradeTime;
            _baseCurrency = baseCurrency;
            _type = type;
            _positionRuntime = positionRuntime;
        }

        private PositionType _type;
        public PositionType Type
        {
            get { return _type; }
            set { _type = value; }
        }

    }
}
