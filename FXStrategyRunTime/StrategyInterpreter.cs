using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.DataAccess;
using FXStrategy.MetaModel;
using FXStrategy.EvaluationContext;

namespace FXStrategy.Interpreter
{
    /// <summary>
    /// Responsible for interpreting the strategy model.
    /// It is the runtime environment for the langauge.
    /// </summary>
    public class StrategyInterpreter
    {
        /// <summary>
        /// Variable name for a variable represents the current date of execution
        /// </summary>
        private const string TODAY_STR = "CURRENT_DATE";

        /// <summary>
        /// Variable name fo home currency
        /// </summary>
        private const string BASE_CURRENCY_STR = "HomeCurrency";

        private Dictionary<string, PositionSetRuntime> _positionSetTable;

        /// <summary>
        /// Contains avaliable position set
        /// </summary>
        public Dictionary<string, PositionSetRuntime> PositionSetTable
        {
            get { return _positionSetTable; }
            set { _positionSetTable = value; }
        }
        private Dictionary<string, Currency> _currencyTable;
        Context _evaluationContext;
        CurrencyDataSource _currencyDataSource;

        public Context EvaluationContext
        {
            get { return _evaluationContext; }
            set { _evaluationContext = value; }
        }

        public StrategyInterpreter() : this(new FXEntities.FXEntities()) { }

        public StrategyInterpreter(FXEntities.FXEntities fxEntities) : this(fxEntities,new CurrencyDataSource(fxEntities))
        {
        }

        public StrategyInterpreter(FXEntities.FXEntities fxEntities, CurrencyDataSource currencyDataSource)
        {
            _currencyDataSource = currencyDataSource;
            _positionSetTable = new Dictionary<string, PositionSetRuntime>();
            _currencyTable = new Dictionary<string, Currency>();
            _currencyDataSource.GetAllAvailableCurrencies().ToList().ForEach(c => _currencyTable.Add(c.CurrencyCode, new Currency(c.CurrencyCode)));
        }

        public StrategyInterpreter(CurrencyDataSource currencyDataSource) : this(new FXEntities.FXEntities(),currencyDataSource)
        { }

        public void Clear()
        {
            if(_positionSetTable!=null)
             _positionSetTable.Clear();
            if(_evaluationContext!=null && _evaluationContext.ValuesTable!=null)
                _evaluationContext.ValuesTable.Clear();
        }
        
        private void CreatePositionSet(string positionSetName, Currency baseCurrency, int capacity, PositionType type)
        {
            var positionSet = new PositionSetRuntime(capacity, type, baseCurrency);
            PositionSetTable.Add(positionSetName, positionSet);
            _evaluationContext.ValuesTable.Add(positionSetName, positionSet.Positions);
        }

        /// <summary>
        /// The method for executing the trading strategy.
        /// The result are the position records that located in the position sets
        /// </summary>
        /// <param name="tradingStrategy">the trading strategy</param>
        /// <param name="startDate">start date of back-testing</param>
        /// <param name="endDate">end date of back-testing</param>
        public void Execute(TradingStrategy tradingStrategy, DateTime startDate, DateTime endDate)
        {
            Currency baseCurrency = _currencyTable[tradingStrategy.Portfolio.HomeCurrency];

            // initialize evaluation context
            var collectionDataContainer = new PredefinedDataContainer();
            collectionDataContainer.Add(new Top3Currencies(_currencyDataSource, baseCurrency)); // predefined data set "Top3Currencies"
            collectionDataContainer.Add(new Bottom3Currencies(_currencyDataSource, baseCurrency)); // predefine data set "Bottom3Currencies"
            _evaluationContext = new Context(_currencyDataSource, collectionDataContainer, new ValuesTable(10), startDate);

            // Add the current date of execution into values table
            _evaluationContext.ValuesTable.Add(TODAY_STR, startDate);

            // Add base currency as a variable
            _evaluationContext.ValuesTable.Add(BASE_CURRENCY_STR, baseCurrency.Name);

            // put all the paramter definitions into the table
            foreach (var param in tradingStrategy.ConstantVariableDefinitions)
            {
                _evaluationContext.ValuesTable.Add(param.Variable.Name, param.Constant.Eval(_evaluationContext));
            }

            // initialize position set
            foreach (var positionDef in tradingStrategy.Portfolio.PositionSets)
            {
                PositionType type = (positionDef.PositionType == FXStrategy.MetaModel.PositionType.Long)?PositionType.Long:PositionType.Short;
                CreatePositionSet(positionDef.Name, baseCurrency, (int)positionDef.Number.Eval(_evaluationContext), type);
            }

            // initialize execute frequency
            tradingStrategy.TradingRules.ForEach(t => t.ExecuteFrequency.Initialize(startDate, endDate,_evaluationContext));

            // add predefined data set into collection data container
            for(DateTime currentDate = startDate; currentDate <= endDate; currentDate = currentDate.AddDays(1))
            {
                // No activities on weekend
                if (DateTimeHelper.IsWeekEnd(currentDate))
                    continue;

                _evaluationContext.ValuesTable[TODAY_STR] = currentDate;
                _evaluationContext.CurrentDate = currentDate;
                foreach (TradingRule rule in tradingStrategy.TradingRules)
                {
                    // evaluate the rule inner statement
                    // only if current date fit to its execution frequency
                    if (rule.ExecuteFrequency.CanExecute(currentDate))
                        EvaluateStatement(rule.InnerStatement);
                }
            }

        }

        /// <summary>
        /// Function for evaluating statments
        /// </summary>
        /// <param name="statement"></param>
        protected void EvaluateStatement(Statement statement)
        {
            if (statement == null)
                return;

            if (statement is ForAllStatement)
            {
                var forAllStatement = statement as ForAllStatement;

                IEnumerable<object> collections = forAllStatement.Collection.Eval(_evaluationContext) as IEnumerable<object>;

                if (collections.Count() == 0)
                    return;

                // Declare the iterator in value table
                _evaluationContext.ValuesTable.Add(forAllStatement.Iterator.Name, null);

                foreach (var item in collections)
                {
                    _evaluationContext.ValuesTable[forAllStatement.Iterator.Name] = item;
                    forAllStatement.Iterator.SetType(item.GetType());
                    // evaluate statments recursively
                    EvaluateStatement(forAllStatement.InnerStatement);
                }
                _evaluationContext.ValuesTable.Remove(forAllStatement.Iterator.Name);
            }
            else if (statement is IfStatement)
            {
                var ifStatement = statement as IfStatement;

                if ((bool)ifStatement.ConditionalExpression.Eval(_evaluationContext) == true)
                {
                    EvaluateStatement(ifStatement.InnerStatement);
                }
                else if (ifStatement.IfFalseStatement != null)
                {
                    EvaluateStatement(ifStatement.IfFalseStatement);
                }
            }
            else if (statement is Assignment)
            {
                var assignment = statement as Assignment;
                // assign to the variable object from the values table
                _evaluationContext.ValuesTable[assignment.Target.Name] = assignment.Expression.Eval(_evaluationContext);
            }
            else if (statement is Declaration)
            {
                var declaration = statement as Declaration;
                _evaluationContext.ValuesTable.Add(declaration.Variable.Name, null);
            }
            else if (statement is OpenPosition)
            {
                var openPositionStatement = statement as OpenPosition;
                PositionSetRuntime positionSetRT = PositionSetTable[openPositionStatement.PositionSet.Name];
                string currency = (string)openPositionStatement.Currency.Eval(_evaluationContext);
                positionSetRT.OpenPosition(_currencyTable[currency], _evaluationContext.CurrentDate);
            }
            else if (statement is ClosePosition)
            {
                var closePositionStatement = statement as ClosePosition;

                PositionRuntime positionRT = closePositionStatement.Position.Eval(_evaluationContext) as PositionRuntime;
                positionRT.ClosePosition(_evaluationContext.CurrentDate);
            }
            else if (statement is PositionStopLoss)
            {
                var positionStopLoss = statement as PositionStopLoss;

                PositionRuntime positionRT = _evaluationContext.ValuesTable[positionStopLoss.Position.Name] as PositionRuntime;
                positionRT.StopLoss(_evaluationContext.CurrentDate);
            }
            else if (statement is CompositeStatement)
            {
                var compositeStatement = statement as CompositeStatement;
                compositeStatement.Statements.ForEach(s => EvaluateStatement(s));
            }
            else
                throw new Exception("Unknown statement type: " + statement.GetType().Name);
        }

    }
}
