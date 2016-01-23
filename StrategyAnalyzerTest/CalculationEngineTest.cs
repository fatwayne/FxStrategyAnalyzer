using FXStrategy.Interpreter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FXStrategy.MetaModel;
using System.Collections.Generic;
using FXStrategy.DataAccess;
using FXStrategy.EvaluationContext;
using System.Linq;

namespace StrategyAnalyzerTest
{
    
    
    /// <summary>
    ///This is a test class for CalculationEngineTest and is intended
    ///to contain all CalculationEngineTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CalculationEngineTest
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
        ///A test for GetTransformedPositionRecords
        ///</summary>
        [TestMethod()]
        [DeploymentItem("FXStrategyRunTime.dll")]
        public void GetTransformedPositionRecordsTest()
        {
            FXEntities.FXEntities fxEntities = new FXEntities.FXEntities();
            CurrencyDataSource currencyDataSource = new CurrencyDataSource(fxEntities);
           
            var startDate = new DateTime(2000, 1, 4);
            var endDate = new DateTime(2003, 1, 4);
            var periodicTimeDef = new PeriodicTimeDefinition(3, PeriodicType.Month);
            periodicTimeDef.Initialize(startDate, endDate);
            ProfitCalculationEngine_Accessor target = new ProfitCalculationEngine_Accessor(currencyDataSource);

            PositionRuntime positionRuntime = new PositionRuntime(FXStrategy.Interpreter.PositionType.Long);
            positionRuntime.PositionRecords.Add(new PositionRecord(
                new DateTime(2000, 1, 4), new Currency("USD"), new Currency("EUR"),
                 FXStrategy.Interpreter.PositionType.Long, positionRuntime)
                 {
                     EndDate = new DateTime(2001,3,2)
                 });

            //var actual = target.GetTransformedPositionRecords(positionRuntime, periodicTimeDef);

            //Assert.AreEqual(actual.Count, 6);
            //Assert.IsTrue(actual.ElementAt(5).Type == FXStrategy.Interpreter.PositionType.Short);
            //Assert.IsTrue(actual.Take(5).All(t => t.Type == FXStrategy.Interpreter.PositionType.Long));
        }
    }
}
