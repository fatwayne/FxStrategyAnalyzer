using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using StrategyAnalyzerCore.Visualization.Commands;
namespace StrategyAnalyzerCore.Visualization.ViewModels
{
    public class MainVM:VMBase
    {

        private const string G10_CODE = @"

global identifiers{
	define reEntryExRatePercent as 0.05;
	define noOfLongPosition as 3;
	define noOfShortPosition as 3;

}

define Portfolio{
	set HomeCurrency to 'EUR';
	LongPositions: Position<Long>[3];
	ShortPositions: Position<Short>[3];
}


rule reallocation executes on every Friday{
    for all position in Portfolio.LongPositions{
	    if position.Currency is not in Top3Currencies:
		    Close position;
    }

    for all currency in Top3Currencies{
	    if currency is not in Currency of Portfolio.LongPositions:
		    Open Portfolio.LongPositions with currency;
    }

    for all position in Portfolio.ShortPositions{
	    if position.Currency is not in Bottom3Currencies:
		    Close position;
    }

    for all currency in Bottom3Currencies{
	    if currency is not in Currency of Portfolio.ShortPositions:
            Open Portfolio.ShortPositions with currency; 
    }
}

";

        private const string G10_STOP_LOSS_CODE = @"

global identifiers{
	define reEntryExRatePercent as 0.05;
	define noOfLongPosition as 3;
	define noOfShortPosition as 3;

}

define Portfolio{
	set HomeCurrency to 'EUR';
	LongPositions: Position<Long>[3];
	ShortPositions: Position<Short>[3];
}

stop-loss rule StopLossRule executes on every day{
    for Portfolio.ShortPositions
    when curExRate > mvg
    where 
        decimal mvg = 15 days SMA of [HomeCurrency / position.Currency] at CURRENT_DATE;
        decimal curExRate = [HomeCurrency / position.Currency] at CURRENT_DATE;
}

stop-loss rule StopLossRule executes on every day{
    for Portfolio.LongPositions
    when curExRate < mvg
    where 
        decimal mvg = 15 days SMA of [HomeCurrency / position.Currency] at CURRENT_DATE;
        decimal curExRate = [HomeCurrency / position.Currency] at CURRENT_DATE;
}

rule reallocation executes on every Friday{
    for all position in Portfolio.LongPositions{
	    if position.Currency is not in Top3Currencies:
		    Close position;
    }

    for all currency in Top3Currencies{
	    if currency is not in Currency of Portfolio.LongPositions:
		    Open Portfolio.LongPositions with currency;
    }

    for all position in Portfolio.ShortPositions{
	    if position.Currency is not in Bottom3Currencies:
		    Close position;
    }

    for all currency in Bottom3Currencies{
	    if currency is not in Currency of Portfolio.ShortPositions:
            Open Portfolio.ShortPositions with currency; 
    }
}

";


        public MainVM(){
            StrategyCodeVMs.Add(new StrategyCodeVM()
                    {
                        Code = G10_CODE,
                        Header = "G10 Strategy"
                    }
                   );

            StrategyCodeVMs.Add(new StrategyCodeVM()
            {
                Code = G10_STOP_LOSS_CODE,
                Header = "G10 Strategy with stop loss"
            }
                 );
        }

        private ObservableCollection<StrategyCodeVM> _strategyCodeVMs;
        /// <summary>
        ///     List that the TabControl's ItemsSource property is bound to
        /// </summary>
        public ObservableCollection<StrategyCodeVM> StrategyCodeVMs
        {
            get
            {
                if (_strategyCodeVMs == null)
                {
                    _strategyCodeVMs = new ObservableCollection<StrategyCodeVM>();
                    SelectedStrategyCode = _strategyCodeVMs.FirstOrDefault();
                }

                return _strategyCodeVMs;
            }
        }

        private StrategyCodeVM _selectedStrategyCode;

        public StrategyCodeVM SelectedStrategyCode
        {
            get { return _selectedStrategyCode; }
            set { _selectedStrategyCode = value;
            base.OnPropertyChanged("SelectedStrategyCode");
            }
        }


        private Commands.DelegateCommand _createNewStrategyCodeCmd;
        public Commands.DelegateCommand CreateNewStrategyCodeCmd
        {
            get {
                if (_createNewStrategyCodeCmd == null)
                {
                    _createNewStrategyCodeCmd = new DelegateCommand(() => {

                     StrategyCodeVM newStrategyCodeCmd =   new StrategyCodeVM()
                    {
                        Code = "",
                        Header = "New Strategy " + (StrategyCodeVMs.Count() + 1)
                    };
                    StrategyCodeVMs.Add(newStrategyCodeCmd);
                    SelectedStrategyCode = newStrategyCodeCmd;
                    }
                        );

                    
                }
                return _createNewStrategyCodeCmd; }
            set { _createNewStrategyCodeCmd = value; }
        }

        

    }
}
