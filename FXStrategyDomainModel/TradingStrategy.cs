using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.MetaModel
{
    /// <summary>
    /// Represents a trading strategy
    /// </summary>
    public class TradingStrategy
    {
        private List<TradingRule> _tradingRules;
        public List<TradingRule> TradingRules
        {
            get
            {
                if (_tradingRules == null)
                {
                    _tradingRules = new List<TradingRule>();
                }
                return _tradingRules;
            }
            set
            {
                _tradingRules = value;
            }
        }

        private List<GlobalIdentifier> _paramerterDefinitions;
        public List<FXStrategy.MetaModel.GlobalIdentifier> ConstantVariableDefinitions
        {
            get
            {
                if (_paramerterDefinitions == null)
                {
                    _paramerterDefinitions = new List<GlobalIdentifier>();
                }
                return _paramerterDefinitions;
            }

            set
            {
                _paramerterDefinitions = value;
            }
        }

        private Portfolio _portfolio;
        public Portfolio Portfolio
        {
            get
            {
                if (_portfolio == null)
                    _portfolio = new Portfolio();
                return _portfolio;
            }
            set
            {
                _portfolio = value;
            }
        }
    }
}
