using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.MetaModel
{
    /// <summary>
    /// Represents a trading rule
    /// </summary>
    public class TradingRule
    {
        public TradingRule(string name, TimeIntervalDefinition executeFrequency, Statement statement)
        {
            _name = name;
            _executeFrequency = executeFrequency;
            _innerStatement = statement;
        }

        private string _name;
        /// <summary>
        /// Name of the rule
        /// </summary>
        public string Name
        {
            get { return _name; }
        }


        private TimeIntervalDefinition _executeFrequency;
        /// <summary>
        /// The frequency that  the rule executes
        /// </summary>
        public TimeIntervalDefinition ExecuteFrequency
        {
            get { return _executeFrequency; }
        }

        private Statement _innerStatement;
        public Statement InnerStatement
        {
            get { return _innerStatement; }
        }
    }
}
