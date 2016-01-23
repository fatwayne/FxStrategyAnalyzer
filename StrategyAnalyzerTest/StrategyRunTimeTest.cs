using FXStrategy.Interpreter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FXStrategy.EvaluationContext;
using FXStrategy.MetaModel;
using System.Linq;
using FXStrategy.MetaModel.DataType;
using System.Collections.Generic;
using System.Text;
using FXStrategy.DataAccess;
using System.IO;

namespace StrategyAnalyzerTest
{
    
    
    /// <summary>
    ///This is a test class for StrategyRunTimeTest and is intended
    ///to contain all StrategyRunTimeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StrategyRunTimeTest
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
        ///A test for Execute
        ///</summary>
        [TestMethod()]
        public void ExecuteTest()
        {
            StrategyInterpreter target = new StrategyInterpreter();
            TradingStrategy tradingStrategy = ConstructTradingStrategy();
                 
            DateTime startDate = new DateTime(2000,1,7);
            DateTime endDate = new DateTime(2000,2,25);
            target.Execute(tradingStrategy, startDate, endDate);

            PositionSetRuntime longPositions = target.PositionSetTable["LongPositions"];
            Assert.IsTrue(longPositions.Positions.Any( p => p.CurrencyInPosition.Name == "USD"));
            Assert.IsTrue(longPositions.Positions.Any( p => p.CurrencyInPosition.Name == "GBP"));
            Assert.IsTrue(longPositions.Positions.Any( p => p.CurrencyInPosition.Name == "NOK"));

            target.Clear();

            target.Execute(tradingStrategy, startDate, new DateTime(2000,3,10));
            longPositions = target.PositionSetTable["LongPositions"];
            Assert.IsTrue(longPositions.Positions.Any(p => p.CurrencyInPosition.Name == "USD"));
            Assert.IsTrue(longPositions.Positions.Any(p => p.CurrencyInPosition.Name == "GBP"));
            Assert.IsTrue(longPositions.Positions.Any(p => p.CurrencyInPosition.Name == "NZD"));

            target.Clear();

            target.Execute(tradingStrategy, startDate, new DateTime(2000, 4, 14));
            longPositions = target.PositionSetTable["LongPositions"];
            Assert.IsTrue(longPositions.Positions.Any(p => p.CurrencyInPosition.Name == "USD"));
            Assert.IsTrue(longPositions.Positions.Any(p => p.CurrencyInPosition.Name == "NOK"));
            Assert.IsTrue(longPositions.Positions.Any(p => p.CurrencyInPosition.Name == "NZD"));

        }

        /// <summary>
        ///A test for Execute
        ///</summary>
        [TestMethod()]
        public void CalculationTest()
        {
            FXEntities.FXEntities fxEntities = new FXEntities.FXEntities();
            CurrencyDataSource currencyDataSource = new CurrencyDataSource(fxEntities);
            StrategyInterpreter target = new StrategyInterpreter(fxEntities, currencyDataSource);
            TradingStrategy tradingStrategy = ConstructTradingStrategy();

            DateTime startDate = new DateTime(2000, 1, 4);
            DateTime endDate = new DateTime(2002, 1, 1);
            target.Execute(tradingStrategy, startDate, endDate);

            ProfitCalculationEngine calEngine = new ProfitCalculationEngine(
                currencyDataSource
                );

            calEngine.Evaluate(target.PositionSetTable["ShortPositions"].Positions,
                startDate,
                endDate, new PeriodicTimeDefinition(3, PeriodicType.Month)
                );
            //calEngine.Analyze(target.PositionSetTable["LongPositions"].Positions
            //    .Union(target.PositionSetTable["ShortPositions"].Positions).ToList(),
            //    startDate, 
            //    endDate, new PeriodicTimeDefinition(3, PeriodicType.Month)
            //    );

            List<TimeSeriesData> returnOverTime = calEngine.ReturnOverTime;
            string dest = @"C:\temp\ReturnOverTime.txt";

            WriteToFile(returnOverTime, dest);

            WriteToFile(calEngine.IndexOverTime, @"C:\temp\IndexOverTime.txt");
        }

         [TestMethod()]
        public void ExecutionTimeTest()
        {
            TradingStrategy tradingStratgy = ConstructTradingStrategy();

            FXEntities.FXEntities fxEntities = new FXEntities.FXEntities();
            CurrencyDataSource currencyDataSource = new CurrencyDataSource(fxEntities);
            currencyDataSource.PreLoad();
            StrategyInterpreter target = new StrategyInterpreter(fxEntities, currencyDataSource);
            DateTime startExecutionTime;
            DateTime endExecutionTime;
            DateTime startDate = new DateTime(2000, 1, 1);
            List<List<double>> results = new List<List<double>>();
            StringBuilder output = new StringBuilder();
            for (int j = 0; j < 10; j++)
            {
                results.Add(new List<double>());
                output.AppendLine("Run no.: " + j);
                for (int i = 0; i < 11; i++)
                {
                    startExecutionTime = DateTime.Now;
                    target.Execute(tradingStratgy, startDate, startDate.AddYears(i + 1));
                    endExecutionTime = DateTime.Now;
                    results[j].Add((endExecutionTime - startExecutionTime).TotalMilliseconds);
                    output.AppendLine(String.Format("Execution time: {0}ms, Number of positionRecord: {1} ",
                        (endExecutionTime - startExecutionTime).TotalMilliseconds,
                        target.PositionSetTable.Sum(p => p.Value.Positions.SelectMany(pos => pos.PositionRecords).Count()
                        )
                        ));
                    target.Clear();

                    //results.Add(endExecutionTime - startExecutionTime);
                }
                output.AppendLine();
            }

            output.AppendLine("Average: ");
            for (int i = 0; i < 10; i++)
            {
                output.AppendLine( (i+1) + " Year: \t" + results.Select(r => r.ElementAt(i)).Average());
            }


            using (StreamWriter writer = new StreamWriter(@"D:\Documents\Study Materials\Master Thesis\Main Thesis\executionTime.txt"))
            {
                
                //for (int i = 1; i <= 10; i++)
                //{
                //    output += String.Format(results[i - 1].Milliseconds + " ms \n");
                //}

                writer.WriteLine(output);
            }

            
        }

        [TestMethod()]
        public void ExchangeRateAccessorTest()
        {

            TradingStrategy tradingStrategy = new TradingStrategy()
            {
                ConstantVariableDefinitions = new System.Collections.Generic.List<GlobalIdentifier>(){
                     new GlobalIdentifier(){ 
                         Variable = new Variable("reEntryExRatePercent", typeof(int)),
                         Constant = new Constant(typeof(decimal),0.05)},
                    new GlobalIdentifier(){ 
                         Variable = new Variable("noOfLongPosition",typeof(int)),
                         Constant = new Constant(typeof(int),3)},
                    new GlobalIdentifier(){ 
                         Variable = new Variable("noOfShortPosition",typeof(int)),
                         Constant = new Constant(typeof(int),3)}
                 },
                Portfolio = new Portfolio()
                {
                    HomeCurrency = "EUR",
                    PositionSets = new List<PositionSet>()
                    {
                        new PositionSet(){
                             Name = "LongPositions", Number = new Variable("noOfLongPosition",typeof(int)), PositionType = FXStrategy.MetaModel.PositionType.Long
                        },
                        new PositionSet(){
                            Name = "ShortPositions", Number = new Variable("noOfShortPosition", typeof(int)), PositionType = FXStrategy.MetaModel.PositionType.Short
                        }
                    }
                },

                TradingRules = new System.Collections.Generic.List<TradingRule>()
                {
                    new TradingRule("test", new ConcreteTimeDefinition(){
                         ExecuteTime = new DateTime(2006,1,30)
                    },
                     new Assignment(
                        new Variable("exRateMVG", typeof(decimal)),
                        new AtTime(
                        new ExchangeRateAccessor(
                            new Constant(typeof(string), "EUR"),
                            new Constant(typeof(string), "USD"))
                            ,
                            new Constant(typeof(DateTime), new DateTime(2006,1,30)))))
                }
            };

            FXEntities.FXEntities fxEntities = new FXEntities.FXEntities();
             CurrencyDataSource currencyDataSource = new CurrencyDataSource(fxEntities);
            StrategyInterpreter target = new StrategyInterpreter(fxEntities, currencyDataSource);
             DateTime startDate = new DateTime(2006, 1, 29);
            DateTime endDate = new DateTime(2006, 1, 31);
            target.Execute(tradingStrategy, startDate, endDate);


        }

        private static void WriteToFile(List<TimeSeriesData> returnOverTime, string dest)
        {
            StringBuilder sb = new StringBuilder();
            //coreEngine.StartAnalysisProcess(startDate, endDate);

            foreach (var record in returnOverTime)
            {
                sb.AppendLine(String.Format("Date: {0} Profit: {1}", record.Time, record.Value));
            }

            using (System.IO.StreamWriter strWri = new System.IO.StreamWriter(dest, false))
            {
                strWri.Write(sb);
            }
        }



        

        private static TradingStrategy ConstructTradingStrategy()
        {
            TradingStrategy tradingStrategy = new TradingStrategy()
            {
                ConstantVariableDefinitions = new System.Collections.Generic.List<GlobalIdentifier>(){
                     new GlobalIdentifier(){ 
                         Variable = new Variable("reEntryExRatePercent", typeof(int)),
                         Constant = new Constant(typeof(decimal),0.05)},
                    new GlobalIdentifier(){ 
                         Variable = new Variable("noOfLongPosition",typeof(int)),
                         Constant = new Constant(typeof(int),3)},
                    new GlobalIdentifier(){ 
                         Variable = new Variable("noOfShortPosition",typeof(int)),
                         Constant = new Constant(typeof(int),3)}
                 },
                Portfolio = new Portfolio()
                {
                    HomeCurrency = "EUR",
                    PositionSets = new List<PositionSet>()
                    {
                        new PositionSet(){
                             Name = "LongPositions", Number = new Variable("noOfLongPosition",typeof(int)), PositionType = FXStrategy.MetaModel.PositionType.Long
                        },
                        new PositionSet(){
                            Name = "ShortPositions", Number = new Variable("noOfShortPosition", typeof(int)), PositionType = FXStrategy.MetaModel.PositionType.Short
                        }
                    }
                },

                TradingRules = new System.Collections.Generic.List<TradingRule>()
                {
                    //new StopLossRule(
                    //    "Stop loss rule",
                    //    new PeriodicTimeDefinition(1, PeriodicType.Day),
                    //    new Variable("position",typeof(Position)),
                    //    new CompositeStatement(
                    //        new List<Statement>{
                    //           new Assignment(new Variable("exRateOverTime",typeof(TimeSeriesData)),
                    //               new PropertyAccessor(
                    //                   new ArgumentedVariable("CurrencyPairSet", typeof(CurrencyPair),
                    //                       new Expression[]{ new Constant(typeof(string), "EUR"), 
                    //                        new PropertyAccessor( new Variable("position"),"Currency")
                    //                       }),
                    //                   "ExchangeRates")
                    //                   ),
                    //            new Assignment(new Variable("currentExRate"),
                    //                new MethodAccessor(new Variable("exRateOverTime",typeof(TimeSeriesData)),
                    //                    "At", new Expression[]{new Variable("TODAY")})),
                    //            new Assignment(new Variable("movingAVG"),
                    //                new MethodAccessor(new Variable("exRateOverTime",typeof(TimeSeriesData)),
                    //                    "MovingAvg", new Expression[]{new Variable("TODAY"),new Constant(typeof(int),14)}))
                    //        }
                    //        ),
                    //    new LessThan{
                    //         LeftExpression = new Variable("currentExRate"),
                    //         RightExpression = new Variable("movingAVG")
                    //    },
                    //    new Variable("LongPositions",typeof(PositionSet))
                    //    ),


                    //new TradingRule(
                    //    "Close Position after a term",
                    //    new PeriodicTimeDefinition( 3,
                    //         PeriodicType.Month
                    //    ),
                    //        new ForAllStatement(
                    //            new Variable( "position",typeof(string)),
                    //             new Variable("LongPositions",typeof(PositionSet)),
                                 
                    //                new ClosePosition(
                    //                  new Variable("position",typeof(Position))
                    //                )
                                
                    //        )
                        
                    //    ),
                    new TradingRule(
                        "reallocation",
                         new WeekDayTimeDefinition(){
                              DayOfWeek = DayOfWeek.Friday
                         },
                         new CompositeStatement(new System.Collections.Generic.List<Statement>(){

                             new ForAllStatement(
                                  new Variable("position",typeof(Position)),
                                 new Variable("LongPositions",typeof(PositionSet)),
                                 
                                new IfStatement(
                                    new IsNotInSet(){
                                        LeftExpression = new PropertyAccessor(
                                            new Variable("position",typeof(Position)),
                                            "Currency")
                                        , RightExpression = new Variable("Top3Currencies",typeof(PredefinedDataSet))
                                          
                                    },
                                    new ClosePosition(new Variable("position",typeof(Position)))
                                )
                             ),
                             new ForAllStatement(
                                  new Variable("currency", typeof(string)),
                                 new Variable("Top3Currencies",typeof(PredefinedDataSet)),
                                  new IfStatement(
                                          new IsNotInSet(){
                                               LeftExpression = new Variable("currency",typeof(string)), 
                                               RightExpression = new CollectionPropertiesAccessor(
                                                  new Variable("LongPositions",typeof(PositionSet)),
                                                   "Currency"
                                               )
                                          },
                                        new OpenPosition(
                                            new Variable("LongPositions", typeof(PositionSet)),
                                            new Variable("currency",typeof(string))
                                        )
                                     )
                                 
                             ),

                               new ForAllStatement(
                                   new Variable("position",typeof(Position)),
                                 new Variable("ShortPositions",typeof(PositionSet)),
                                new IfStatement(
                                    new IsNotInSet(){
                                        LeftExpression = new PropertyAccessor(new Variable("position",typeof(Position)), "Currency")
                                        , RightExpression = new Variable("Bottom3Currencies",typeof(PredefinedDataSet))
                                               
                                    },
                                    new ClosePosition(new Variable("position",typeof(Position)))
                                )
                            )
                             ,
                             new ForAllStatement(
                                 new Variable( "currency",typeof(string)),
                                  new Variable("Bottom3Currencies",typeof(PredefinedDataSet)),
                                  
                                new IfStatement(
                                    new IsNotInSet(){
                                        LeftExpression = new Variable("currency",typeof(Position)), 
                                            RightExpression = new CollectionPropertiesAccessor(
                                            new Variable("ShortPositions",typeof(PositionSet)),
                                            "Currency"
                                        )
                                    },
                                    new OpenPosition(
                                        new Variable("ShortPositions",typeof(PositionSet)),
                                        new Variable("currency",typeof(string))
                                    )
                                )
                                 
                             )
                         }
                ))
                }
            };
            return tradingStrategy;
        }
    }
}
