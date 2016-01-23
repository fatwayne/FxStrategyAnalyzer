using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Irony.StrategyData
{
    public class PositionRuntime
    {
        public PositionRuntime(PositionType positionType)
        {
            _positionType = positionType;
        }

        private PositionType _positionType;
        public PositionType PositionType
        {
            get { return _positionType; }
        }
        
        private DateTime _startTime;
        public DateTime StartTime
        {
            get { return _startTime; }
        }

        private DateTime _endTime;
        public DateTime EndTime
        {
            get { return _endTime; }
        }


        private PositionRuntimeStatus _status;
        public PositionRuntimeStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private Currency _currencyInPosition;
        public Currency CurrencyInPosition
        {
            get { return _currencyInPosition; }
            set { _currencyInPosition = value; }
        }

        private PositionRecord _currentPositionRecord;

        private List<PositionRecord> _positionRecords;
        public List<PositionRecord> PositionRecords
        {
            get {
                if (_positionRecords == null)
                {
                    _positionRecords = new List<PositionRecord>();
                }
                return _positionRecords; }
        }
        
        public void StopLoss(DateTime dateTime)
        {
            Status = PositionRuntimeStatus.StopLoss;
            _currentPositionRecord.EndTime = dateTime;
        }

        public void ReEntry(Currency currencyForPosition, Currency baseCurrency, DateTime dateTime)
        {
            Status = PositionRuntimeStatus.Active;
            // create a new trade record
            _currentPositionRecord = new PositionRecord(dateTime, currencyForPosition, baseCurrency, PositionType, this);
            PositionRecords.Add(_currentPositionRecord);
        }

        public void CloseTrade(DateTime dateTime)
        {
            Status = PositionRuntimeStatus.Closed;
            _endTime = dateTime;
            _currentPositionRecord.EndTime = dateTime;

        }

        public void OpenPosition(Currency currencyForPosition, Currency baseCurrency,  DateTime dateTime)
        {

            Status = PositionRuntimeStatus.Active;
            _currentPositionRecord = new PositionRecord(dateTime, currencyForPosition, baseCurrency, PositionType, this);
            PositionRecords.Add(_currentPositionRecord);
        }


    }

    public enum PositionRuntimeStatus
    {
        Active,
        StopLoss,
        Closed
    }
}
