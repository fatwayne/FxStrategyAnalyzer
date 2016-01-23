using StrategyAnalyzerCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
namespace StrategyAnalyzerTest
{
    
    
    /// <summary>
    ///This is a test class for G10CurrenciesDataSourceTest and is intended
    ///to contain all G10CurrenciesDataSourceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class G10CurrenciesDataSourceTest
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
        ///A test for GetEnumerator
        ///</summary>
        [TestMethod()]
        public void GetEnumeratorTest()
        {
            G10CurrenciesDataSource target = G10CurrenciesDataSource.UniqueInstance; // TODO: Initialize to an appropriate value
            target.BaseCurrency = target.G10Currencies.Where(c => c.Name == "EUR").First();
            //IEnumerator<CurrencyPair> expected = null; // TODO: Initialize to an appropriate value
            //Assert.AreEqual(, target.C);
            Assert.AreEqual(target.Count(), 10);
            
        }

        /// <summary>
        ///A test for InitializeOutput
        ///</summary>
        [TestMethod()]
        [DeploymentItem("StrategyAnalyzerCore.exe")]
        public void InitializeOutputTest()
        {
            G10CurrenciesDataSource_Accessor target = new G10CurrenciesDataSource_Accessor(); // TODO: Initialize to an appropriate value
            DateTime startTimerTime = DateTime.Now;
            target.InitializeOutput();
            DateTime endTimerTime = DateTime.Now;
            Process proc = Process.GetCurrentProcess();
            Assert.Inconclusive(String.Format("It takes {0}ms to load the G10CurrenciesDataSource. Consumed memory {1} kb.", (endTimerTime - startTimerTime).Milliseconds, proc.PrivateMemorySize64/1000));
        }
    }
}
