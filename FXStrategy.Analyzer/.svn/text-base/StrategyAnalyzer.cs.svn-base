using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.LanguageParsing;
using FXStrategy.EvaluationContext;
using FXStrategy.Interpreter;
using FXStrategy.MetaModel;
using FXStrategy.DataAccess;

namespace FXStrategy.Analyzer
{
    /// <summary>
    /// Control for the whole strategy analysis process
    /// </summary>
    public class StrategyAnalyzer
    {
        CurrencyDataSource _currencyDataSource;

        public StrategyAnalyzer()
        {
            var fxEntities = new FXEntities.FXEntities();
            _currencyDataSource = new CurrencyDataSource(fxEntities);
            _currencyDataSource.PreLoad();
        }

        /// <summary>
        /// Start the process of analysing the strategy from source code
        /// </summary>
        /// <param name="startDate">start date of back-testing</param>
        /// <param name="endDate">end date of back-testing</param>
        /// <param name="monthsInTerm">length of forward contract in months</param>
        /// <param name="fxCode">source code for a strategy</param>
        /// <returns>Profit Index over time</returns>
        public IEnumerable<DateIndex> Execute(DateTime startDate, DateTime endDate, int monthsInTerm, string fxCode)
        {
            // these objects are created for each execution
            // it is to support multi-threading
            var fxParser = new StrategyLanguageParser();
            var modelTransformer = new ASTToModelTransform.AstTreeToModelTransformer();
            var calculationEngine = new ProfitCalculationEngine(_currencyDataSource);
            var strategyRunTime = new StrategyInterpreter(_currencyDataSource);

            // parse the source code into abstract syntax tree
            var strategyAstNode = fxParser.Parse(fxCode);

            // transform the abstract syntax tree to the model
            var strategyModel = modelTransformer.Transform(strategyAstNode);

            // execute the model
            strategyRunTime.Clear();
            strategyRunTime.Execute(strategyModel, startDate, endDate);
            
            // calculate profit based on the position records
            calculationEngine.Evaluate( 
                strategyRunTime.PositionSetTable.Values.SelectMany(v => v.Positions).ToList(),
                startDate,endDate,
                new PeriodicTimeDefinition(monthsInTerm, PeriodicType.Month));

            return calculationEngine.IndexOverTime.Select(
                i => new DateIndex(){
                    Date = i.Time,
                    Index = i.Value
                });
        }
    }

    /// <summary>
    /// Represents profit index against time
    /// </summary>
    public class DateIndex
    {
        public DateTime Date { get; set; }
        public decimal Index { get; set; }
    }

    
}
