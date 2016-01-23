using FXStrategy.LanguageParsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Irony.Parsing;
using System.Linq;
using Irony.Ast;
using FXStrategy.LanguageParsing.AstNodes;
using FXStrategy.ASTToModelTransform;
using FXStrategy.MetaModel;
using FXStrategy.Interpreter;

namespace StrategyAnalyzerTest
{
    
    
    /// <summary>
    ///This is a test class for StrategyLanguageGrammarTest and is intended
    ///to contain all StrategyLanguageGrammarTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StrategyLanguageGrammarTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        public string ExampleCode
        {
            get
            {
                string exampleCode = @"
global variables{
	define reEntryExRatePercent as 0.05;
	define noOfLongPosition as 3;
	define noOfShortPosition as 3;

}



define Portfolio{
	set BaseCurrency to 'EUR';
	LongPositions: Position<Long>[3];
	ShortPositions: Position<Short>[3];
}

stop-loss rule StopLossRule executes on every day{
    for Portfolio.LongPositions
    when curExRate < mvg
    where 
        decimal mvg = 15 days SMA of [BaseCurrency / position.Currency] at CURRENT_DATE;
        decimal curExRate = [BaseCurrency / position.Currency] at CURRENT_DATE;
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
                return exampleCode;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for StrategyLanguageGrammar Constructor
        ///</summary>
        [TestMethod()]
        public void StrategyLanguageGrammarConstructorTest()
        {

            Parser parser = new Parser(new StrategyLanguageGrammar());
            ParseTree parseTree = parser.Parse(ExampleCode);

            Assert.IsTrue(parseTree.HasErrors() == false);
            Assert.IsTrue(parseTree.Root != null);
            //ParseTreeNode root = parseTree.Root;

            //TradingStrategy strategy = root.AstNode as TradingStrategy;
            //Assert.AreEqual(1, strategy.TradingRules.Count());
            //Assert.Inconclusive(String.Format("Parser Created time: {0}, Parsing time: {1}", parserCreated - start, finishParse - parserCreated));
            //Assert.AreEqual(true, root != null);
        }

                /// <summary>
        ///A full test for transforming 
        ///</summary>
        [TestMethod()]
        public void TransformerTest()
        {
            StrategyLanguageParser fxParser = new StrategyLanguageParser();
            TradingStrategyAstNode strategyAst = fxParser.Parse(ExampleCode);
            AstTreeToModelTransformer transformer = new AstTreeToModelTransformer();
            TradingStrategy strategy = transformer.Transform(strategyAst);
            Assert.IsTrue(strategy.TradingRules.Count > 0);

            StrategyInterpreter strategyRunTime = new StrategyInterpreter();
            DateTime startDate = new DateTime(2000, 1, 7);
            DateTime endDate = new DateTime(2000, 2, 25);

            Assert.IsTrue(strategy.ConstantVariableDefinitions.Count == 3);

            strategyRunTime.Execute(strategy, startDate, endDate);

            PositionSetRuntime longPositions = strategyRunTime.PositionSetTable["LongPositions"];
            Assert.IsTrue(longPositions.Positions.Any(p => p.CurrencyInPosition.Name == "USD"));
            Assert.IsTrue(longPositions.Positions.Any(p => p.CurrencyInPosition.Name == "GBP"));
            Assert.IsTrue(longPositions.Positions.Any(p => p.CurrencyInPosition.Name == "NOK"));

            strategyRunTime.Clear();

            strategyRunTime.Execute(strategy, startDate, new DateTime(2000, 3, 10));
            longPositions = strategyRunTime.PositionSetTable["LongPositions"];
            Assert.IsTrue(longPositions.Positions.Any(p => p.CurrencyInPosition.Name == "USD"));
            Assert.IsTrue(longPositions.Positions.Any(p => p.CurrencyInPosition.Name == "GBP"));
            Assert.IsTrue(longPositions.Positions.Any(p => p.CurrencyInPosition.Name == "NZD"));

            strategyRunTime.Clear();

            strategyRunTime.Execute(strategy, startDate, new DateTime(2000, 4, 14));
            longPositions = strategyRunTime.PositionSetTable["LongPositions"];
            Assert.IsTrue(longPositions.Positions.Any(p => p.CurrencyInPosition.Name == "USD"));
            Assert.IsTrue(longPositions.Positions.Any(p => p.CurrencyInPosition.Name == "NOK"));
            Assert.IsTrue(longPositions.Positions.Any(p => p.CurrencyInPosition.Name == "NZD"));
        }

                  /// <summary>
        ///A full test for transforming 
        ///</summary>
        [TestMethod()]
        public void TransformerTest2()
        {
            string exampleCode = @"

global variables{
	define reEntryExRatePercent as 0.05;
	define noOfLongPosition as 3;
	define noOfShortPosition as 3;
}

define Portfolio{
	set BaseCurrency to 'EUR';
	LongPositions: Position<Long>[3];
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
}";


            StrategyLanguageParser fxParser = new StrategyLanguageParser();
            TradingStrategyAstNode strategyAst = fxParser.Parse(exampleCode);
            AstTreeToModelTransformer transformer = new AstTreeToModelTransformer();
            TradingStrategy strategy = transformer.Transform(strategyAst);
            //Assert.IsTrue(strategy.TradingRules.Count == 1);

            StrategyInterpreter strategyRunTime = new StrategyInterpreter();
            DateTime startDate = new DateTime(2000, 1, 7);
            DateTime endDate = new DateTime(2000, 2, 25);

            strategyRunTime.Execute(strategy, startDate, endDate);

            //PositionSetRunTime longPositions = strategyRunTime.PositionSetTable["LongPositions"];
            //Assert.IsTrue(longPositions.Positions.Any(p => p.CurrencyInPosition.Name == "USD"));
            //Assert.IsTrue(longPositions.Positions.Any(p => p.CurrencyInPosition.Name == "GBP"));
            //Assert.IsTrue(longPositions.Positions.Any(p => p.CurrencyInPosition.Name == "NOK"));
        }
    }
}
