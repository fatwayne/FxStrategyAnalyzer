using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using FXStrategy.EvaluationContext;
using FXStrategy.DataAccess;
namespace StrategyAnalyzerTest
{
    
    
    /// <summary>
    ///This is a test class for Bottom3CurrenciesTest and is intended
    ///to contain all Bottom3CurrenciesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class Bottom3CurrenciesTest
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
        ///A test for GetCurrencies
        ///</summary>
        //[TestMethod()]
        //public void GetCurrenciesTest()
        //{
        //    Bottom3Currencies target = new Bottom3Currencies(); // TODO: Initialize to an appropriate value
        //    DateTime time = new DateTime(2000, 1, 7);
        //    IEnumerable<Currency> expected = new Currency[] { new Currency("JPY"), new Currency("CHF") }; // TODO: Initialize to an appropriate value
        //    IEnumerable<Currency> actual;
        //    actual = target.GetCurrencies(time);

        //    Assert.IsTrue(expected.All(a => actual.Contains(a)));
        //}

        /// <summary>
        ///A test for GetCurrencies
        ///</summary>
        [TestMethod()]
        public void GetCurrenciesTest1()
        {
            Currency baseCurrency = new Currency("EUR");
            FXEntities.FXEntities fxEntities = new FXEntities.FXEntities();
            CurrencyDataSource currencyDataSource = new CurrencyDataSource(fxEntities); // TODO: Initialize to an appropriate value
            Bottom3Currencies target = new Bottom3Currencies(currencyDataSource, baseCurrency); // TODO: Initialize to an appropriate value
            DateTime time = new DateTime(2000, 1, 7); // TODO: Initialize to an appropriate value
            IEnumerable<string> expected = new string[]{"JPY","CHF"};
            IEnumerable<string> actual;
            actual = target.GetCurrencies(time);
            Assert.IsTrue(expected.All(a => actual.Contains(a)));
        }
    }
}
