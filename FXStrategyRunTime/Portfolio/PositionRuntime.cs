using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.EvaluationContext;
using FXStrategy.DataAccess;

namespace FXStrategy.Interpreter
{
    /// <summary>
    /// Represents a position at run time
    /// </summary>
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

        /// <summary>
        /// Name of the currency In position
        /// </summary>
        public string Currency
        {
            get
            {
                if (_currencyInPosition != null)
                    return _currencyInPosition.Name;
                else
                    return null;
            }
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

        private Currency _baseCurrency;

        public Currency BaseCurrency
        {
            get { return _baseCurrency; }
            set { _baseCurrency = value; }
        }

        private PositionRecord _currentPositionRecord;

        private List<PositionRecord> _positionRecords;

        /// <summary>
        /// Position records for this position
        /// </summary>
        public List<PositionRecord> PositionRecords
        {
            get {
                if (_positionRecords == null)
                {
                    _positionRecords = new List<PositionRecord>();
                }
                return _positionRecords; }
        }
        
        /// <summary>
        /// Stop loss operation, 
        /// the executeDate is assigned to end date of the current position record
        /// </summary>
        /// <param name="executeDate">the date performing stop loss</param>
        public void StopLoss(DateTime executeDate)
        {
            if (Status != PositionRuntimeStatus.StopLoss)
            {
                Status = PositionRuntimeStatus.StopLoss;
                _currentPositionRecord.EndDate = executeDate;
            }
        }

        /// <summary>
        /// Stop loss re-entry operation
        /// A new position record is created with start date as executeDate
        /// </summary>
        /// <param name="executeDate">the date performing stop loss re-entry</param>
        public void StopLossReEntry(DateTime executeDate)
        {
            if (Status != PositionRuntimeStatus.StopLoss)
                throw new Exception("The status of a position must be at stop loss when calling StopLossReEntry function.");

            Status = PositionRuntimeStatus.Active;
            // create a new trade record using the last traded record
            _currentPositionRecord = new PositionRecord(executeDate, this.CurrencyInPosition, BaseCurrency, PositionType, this);
            PositionRecords.Add(_currentPositionRecord);
        }

        /// <summary>
        /// Take profit operation
        /// the executeDate is assigned to end date of the current position record
        /// </summary>
        /// <param name="executeDate">the date performing take profit</param>
        public void TakeProfit(DateTime executeDate)
        {
            if (Status != PositionRuntimeStatus.TakeProfit)
            {
                Status = PositionRuntimeStatus.TakeProfit;
                _currentPositionRecord.EndDate = executeDate;
            }
        }

        /// <summary>
        /// Take profit re-entry operation
        /// A new position record is created with start date as executeDate
        /// </summary>
        /// <param name="executeDate">the date performing take profit re-entry</param>
        public void TakeProfitReEntry(DateTime executeDate)
        {
            if (Status != PositionRuntimeStatus.TakeProfit)
                throw new Exception("The status of a position must be at Take Profit when calling TakeProfitReEntry function.");

            Status = PositionRuntimeStatus.Active;
            // create a new trade record using the last traded record
            _currentPositionRecord = new PositionRecord(executeDate, this.CurrencyInPosition, BaseCurrency, PositionType, this);
            PositionRecords.Add(_currentPositionRecord);
        }

        /// <summary>
        /// Operation to close the current position
        /// </summary>
        /// <param name="executeDate">the date closing the positiong</param>
        public void ClosePosition(DateTime executeDate)
        {
            if (_status != PositionRuntimeStatus.Closed)
            {
                Status = PositionRuntimeStatus.Closed;
                _endTime = executeDate;
                _currentPositionRecord.EndDate = executeDate;
                _currencyInPosition = null;
            }
        }

        /// <summary>
        /// Operation to open a position with the specified currency
        /// </summary>
        /// <param name="currencyInPosition"></param>
        /// <param name="baseCurrency"></param>
        /// <param name="executeDate">the date opening the position</param>
        public void OpenPosition(Currency currencyInPosition, Currency baseCurrency, DateTime executeDate)
        {
            if (Status == PositionRuntimeStatus.Active)
                throw new Exception("The position is already opened. Currency in the position: " + this._currencyInPosition.Name
                    );
            Status = PositionRuntimeStatus.Active;
            _startTime = executeDate;
            this._currencyInPosition = currencyInPosition;
            this._baseCurrency = baseCurrency;
            _currentPositionRecord = new PositionRecord(executeDate, currencyInPosition, baseCurrency, PositionType, this);
            PositionRecords.Add(_currentPositionRecord);
        }

       

        /// <summary>
        /// The current position record
        /// </summary>
        public PositionRecord CurrentPositionRecord
        {
            get
            {
                return _currentPositionRecord;
            }
        }

    }

    /// <summary>
    /// Status of the position
    /// </summary>
    public enum PositionRuntimeStatus
    {
        Active,
        StopLoss,
        Closed,
        TakeProfit,
    }
}
