using FXStrategy.Interpreter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace StrategyAnalyzerTest
{
    
    
    /// <summary>
    ///This is a test class for ProfitCalculationEngineTest and is intended
    ///to contain all ProfitCalculationEngineTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProfitCalculationEngineTest
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
        ///A test for CalculateForwardRate
        ///</summary>
        [TestMethod()]
        [DeploymentItem("FXStrategyRunTime.dll")]
        public void CalculateForwardRateTest()
        {
            int numberOfDays = 90; 
            Decimal variableSpotRate = new Decimal(0.6275); 
            Decimal baseInRate = new Decimal(0.0336);
            Decimal variableInRate = new Decimal(0.0613); 
            Decimal expected = new Decimal(0.6318); 
            Decimal actual;
            actual = ProfitCalculationEngine_Accessor.CalculateForwardRate(numberOfDays, variableSpotRate, baseInRate, variableInRate);
            Assert.AreEqual(Math.Round(expected,4), Math.Round(actual,4));
        }
    }
}
