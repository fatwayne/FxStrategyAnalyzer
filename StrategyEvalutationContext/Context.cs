using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.DataAccess;

namespace FXStrategy.EvaluationContext
{
    /// <summary>
    /// Contains runtime information
    /// </summary>
    public class Context
    {
        DateTime _currentTime;

        /// <summary>
        ///  current date during execution
        /// </summary>
        public DateTime CurrentDate
        {
            get { return _currentTime; }
            set { _currentTime = value; }
        }

        PredefinedDataContainer _predefinedDataContainer;

        public PredefinedDataContainer PredefinedDataContainer
        {
            get { return _predefinedDataContainer; }
        }

        private CurrencyDataSource _currencyDataSource;

        public CurrencyDataSource CurrencyDataSource
        {
            get { return _currencyDataSource; }
            set { _currencyDataSource = value; }
        }

        ValuesTable _valuesTable;
        /// <summary>
        /// Values of variables at runtime
        /// </summary>
        public ValuesTable ValuesTable
        {
            get { return _valuesTable; }
        }

        public Context(CurrencyDataSource currencyDataSource, PredefinedDataContainer collectionDataContainer, ValuesTable valuesTable, DateTime startTime)
        {
            _predefinedDataContainer = collectionDataContainer;
            _valuesTable = valuesTable;
            _currentTime = startTime;
            _currencyDataSource = currencyDataSource;
        }
    }
}
