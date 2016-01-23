using StrategyAnalyzerCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
namespace StrategyAnalyzerTest
{
    
    
    /// <summary>
    ///This is a test class for CoreEngineTest and is intended
    ///to contain all CoreEngineTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CoreEngineTest
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
        ///A test for StartAnalysisProcess
        ///</summary>
        [TestMethod()]
        public void StartAnalysisProcessTest()
        {
            CoreEngine target = new CoreEngine(); // TODO: Initialize to an appropriate value
            StrategyFormulizer strategyFormulizer = new StrategyFormulizer(target);
            DateTime startTime = new DateTime(2000,1,7); // TODO: Initialize to an appropriate value
            DateTime endTime = new DateTime(2010,1,15); // TODO: Initialize to an appropriate value
            //IEnumerable<TradeRecord> expected = null; // TODO: Initialize to an appropriate value
            //IEnumerable<TradeRecord> actual;
            DateTime startTimerTime = DateTime.Now;
            target.StartAnalysisProcess(startTime, endTime);
            DateTime endTimerTime = DateTime.Now;
            //Assert.AreEqual(expected, actual);
            Assert.Inconclusive(String.Format("It takes {0}ms to execute the engine", (endTimerTime - startTimerTime).Milliseconds));
        }

        /// <summary>
        ///A test for ExecuteRules
        ///</summary>
        [TestMethod()]
        [DeploymentItem("StrategyAnalyzerCore.exe")]
        public void StartAnalysisProcessTest2()
        {
            CoreEngine target = new CoreEngine(); // TODO: Initialize to an appropriate value
            StrategyFormulizer strategyFormulizer = new StrategyFormulizer(target);
            DateTime startTime = new DateTime(2000,1,7); // TODO: Initialize to an appropriate value
            DateTime endTime = new DateTime(2010,1,15); // TODO: Initialize to an appropriate value
            target.StartAnalysisProcess(startTime, endTime);
            var usdRecords = TradeRecordsCollection.UniqueInstance.TradeRecords.Where(r => r.CurrencyToTrade.Name == "USD");
            var targetRecord = usdRecords.Where(r => r.StartTime == startTime && r.EndTime == startTime.AddDays(7 * 12));
            Assert.IsTrue(targetRecord.Count() == 1);
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
