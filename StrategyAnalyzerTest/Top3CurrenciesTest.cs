using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using FXStrategy.EvaluationContext;
using FXStrategy.DataAccess;

namespace StrategyAnalyzerTest
{
    
    
    /// <summary>
    ///This is a test class for Top3CurrenciesTest and is intended
    ///to contain all Top3CurrenciesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class Top3CurrenciesTest
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


        ///// <summary>
        /////A test for GetCurrencies
        /////</summary>
        //[TestMethod()]
        //public void GetCurrenciesTest()
        //{
        //    Top3Currencies target = new Top3Currencies(); 
        //    DateTime time = new DateTime(2000,1,7);
        //    IEnumerable<Currency> expected = new Currency[]{new Currency("USD"), new Currency("GBP"), new Currency("NOK")}; // TODO: Initialize to an appropriate value
        //    IEnumerable<Currency> actual;
        //    actual = target.GetCurrencies(time);
            
        //    Assert.IsTrue(expected.All(a => actual.Contains(a)));
        //    //Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        /// <summary>
        ///A test for Top3Currencies Constructor
        ///</summary>
        [TestMethod()]
        public void Top3CurrenciesConstructorTest()
        {
            FXEntities.FXEntities fxEntities = new FXEntities.FXEntities();
            Currency baseCurrency = new Currency("EUR");; // TODO: Initialize to an appropriate value
            CurrencyDataSource currencyDataSource = new CurrencyDataSource(fxEntities);
            
            Top3Currencies target = new Top3Currencies(currencyDataSource, baseCurrency);
            var result = target.GetCurrencies(new DateTime(2000, 1, 7));
            Assert.IsTrue(result.Any(r => r == "USD"));
            Assert.IsTrue(result.Any(r => r == "GBP"));
            Assert.IsTrue(result.Any(r => r == "NOK"));
        }
    }
}
