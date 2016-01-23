using FXStrategy.EvaluationContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FXStrategy.DataAccess;
using System.Collections.Generic;
using System.Linq;
namespace StrategyAnalyzerTest
{
    
    
    /// <summary>
    ///This is a test class for CurrencyDataSourceTest and is intended
    ///to contain all CurrencyDataSourceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CurrencyDataSourceTest
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
        ///A test for GetAllCurrencyData
        ///</summary>
        [TestMethod()]
        public void GetCurrencyNamesSortByInRateTest()
        {
            FXEntities.FXEntities fxEntities = new FXEntities.FXEntities();
            Currency baseCurrency = new Currency("EUR"); // TODO: Initialize to an appropriate value
            CurrencyDataSource target = new CurrencyDataSource(fxEntities); // TODO: Initialize to an appropriate value
            IEnumerable<string> actual;
            actual = target.GetCurrencyNameOrderByInterestRate(new DateTime(2000,1,7)).Reverse();
            Assert.IsTrue(actual.ElementAt(0) == "GBP");
            Assert.IsTrue(actual.ElementAt(1) == "USD");
            Assert.IsTrue(actual.ElementAt(2) == "NOK");
            Assert.IsTrue(actual.ElementAt(3) == "NZD");
            Assert.IsTrue(actual.ElementAt(4) == "AUD");
            Assert.IsTrue(actual.ElementAt(5) == "CAD");
            Assert.IsTrue(actual.ElementAt(6) == "SEK");
            Assert.IsTrue(actual.ElementAt(7) == "EUR");
            Assert.IsTrue(actual.ElementAt(8) == "CHF");
            Assert.IsTrue(actual.ElementAt(9) == "JPY");
        }
    }
}
