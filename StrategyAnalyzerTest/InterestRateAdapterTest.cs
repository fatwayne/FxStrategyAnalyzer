using FXStrategy.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FXEntities;
using System.Collections.Generic;
using System.Linq;
using FXStrategy.DataAccess;

namespace StrategyAnalyzerTest
{
    
    
    /// <summary>
    ///This is a test class for InterestRateAdapterTest and is intended
    ///to contain all InterestRateAdapterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class InterestRateAdapterTest
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
        ///A test for GetInterestRates
        ///</summary>
        [TestMethod()]
        public void GetInterestRatesTest()
        {
            FXEntities.FXEntities fxEntities = new FXEntities.FXEntities();
            InterestRateAdapter target = new InterestRateAdapter(fxEntities); 
            string currencyCode = "USD"; 
            var actual = target.GetInterestRates(currencyCode);

        }

        /// <summary>
        ///A test for GetInterestRates
        ///</summary>
        [TestMethod()]
        public void GetInterestRatesTest1()
        {
            FXEntities.FXEntities fxEntities = new FXEntities.FXEntities();
            InterestRateAdapter target = new InterestRateAdapter(fxEntities); // TODO: Initialize to an appropriate value
            string currencyCode = "NZD"; // TODO: Initialize to an appropriate value
            var actual = target.GetInterestRates(currencyCode);
            Assert.IsTrue(actual.Count() > 0);
        }
    }
}
