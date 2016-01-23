using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Irony.StrategyData
{
    public class PositionRecord
    {

        private DateTime _startTime;
        public DateTime StartTime
        {
            get { return _startTime; }
        }

        public DateTime EndTime
        {
            get;
            set;
        }

        private Currency _currencyInPosition;
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

        public PositionRecord(DateTime startTradeTime, Currency currencyToTrade, Currency baseCurrency, PositionType type, PositionRuntime positionRuntime)
        {
            _currencyInPosition = currencyToTrade;
            _startTime = startTradeTime;
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
