using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StrategyAnalyzerCore.Visualization.Models;

namespace StrategyAnalyzerCore.Visualization.ViewModels
{
    public class StrategyCodeVM : VMBase
    {
        private StrategyCode _strategyCode;

        public StrategyCodeVM()
        {
            this._strategyCode = new StrategyCode();
        }


        public string Header
        {
            get { return _strategyCode.Header; }
            set { _strategyCode.Header = value; 
                base.OnPropertyChanged("Header");
            }
        }
        public string Code
        {
            get { return this._strategyCode.Code; }
            set { _strategyCode.Code = value;
            base.OnPropertyChanged("Code");
            }
        }
    }
}
