using FXStrategy.Interpreter.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FXStrategy.MetaModel;
using System.Collections.Generic;

namespace StrategyAnalyzerTest
{
    
    
    /// <summary>
    ///This is a test class for TermManagerTest and is intended
    ///to contain all TermManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TermManagerTest
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
        ///A test for GetTermFromPeriod
        ///</summary>
        [TestMethod()]
        public void GetTermFromPeriodTest()
        {
            DateTime positionStartDate = new DateTime(2000,1,4);
            DateTime positionEndDate = new DateTime(2000,7,15); 
            PeriodicTimeDefinition termLength = new PeriodicTimeDefinition(3, PeriodicType.Month);
            termLength.Initialize(positionStartDate, new DateTime(2001,1,1));
            List<Term> actual;
            actual = TermManager.GetTermFromPeriod(positionStartDate, positionEndDate, termLength);
            Assert.AreEqual(3, actual.Count);
        }

        /// <summary>
        ///A test for GetTermFromPeriod
        ///</summary>
        [TestMethod()]
        public void GetTermFromPeriodTest2()
        {
            DateTime positionStartDate = new DateTime(2000, 4, 18);
            DateTime positionEndDate = new DateTime(2000, 4, 25);
            PeriodicTimeDefinition termLength = new PeriodicTimeDefinition(3, PeriodicType.Month);
            termLength.Initialize(new DateTime(1999,1,5), new DateTime(2001, 1, 1));
            List<Term> actual;
            actual = TermManager.GetTermFromPeriod(positionStartDate, positionEndDate, termLength);
            Assert.AreEqual(1, actual.Count);
        }
    }
}
