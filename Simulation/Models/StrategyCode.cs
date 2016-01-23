using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrategyAnalyzerCore.Visualization.Models
{
    public class StrategyCode
    {
        private string _header;
        private string _code;

        /// <summary>
        ///     Header Property 
        /// </summary>
        public string Header
        {
            get { return _header; }
            set
            {
                if (_header != value)
                {
                    _header = value;
                }
            }
        }

        /// <summary>
        ///     Value Property 
        /// </summary>
        public string Code
        {
            get { return _code; }
            set
            {
                if (_code != value)
                {
                    _code = value;
                }
            }
        }
    }
}
