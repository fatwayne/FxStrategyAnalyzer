using FXStrategy.Analyzer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace StrategyAnalyzerTest
{
    
    
    /// <summary>
    ///This is a test class for FxLanguageAnalyzerTest and is intended
    ///to contain all FxLanguageAnalyzerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FxLanguageAnalyzerTest
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
            string fxCode = @"
global variables{
	define reEntryExRatePercent as 0.05;
	define noOfLongPosition as 3;
	define noOfShortPosition as 3;

}

define Portfolio{
	set BaseCurrency to 'EUR';
	LongPositions: Position<Long>[3];
	ShortPositions: Position<Short>[3];
}

rule reallocation executes on every Friday{
    for all position in Portfolio.LongPositions{
	    if position.Currency is not in Top3Currencies:
		    Close position;
    }

    for all currency in Top3Currencies{
	    if currency is not in Currency of Portfolio.LongPositions:
		    Open Portfolio.LongPositions with currency;
    }

    for all position in Portfolio.ShortPositions{
	    if position.Currency is not in Bottom3Currencies:
		    Close position;
    }

    for all currency in Bottom3Currencies{
	    if currency is not in Currency of Portfolio.ShortPositions:
            Open Portfolio.ShortPositions with currency; 
    }
}

";

            StrategyAnalyzer target = new StrategyAnalyzer(); 
             DateTime startExecutionTime;
            DateTime endExecutionTime;
            DateTime startDate = new DateTime(2000, 1, 1);
            List<List<double>> results = new List<List<double>>();

            StringBuilder output = new StringBuilder();
            for (int j = 0; j < 10; j++)
            {
                results.Add(new List<double>());
                output.AppendLine("Year no.: " + (j + 1));
                for (int i = 0; i < 11; i++)
                {
                    startExecutionTime = DateTime.Now;
                    ExecuteAnalyzer(startDate, i+1, target, fxCode);
                    endExecutionTime = DateTime.Now;
                    results[j].Add((endExecutionTime - startExecutionTime).TotalMilliseconds);
                    output.AppendLine(String.Format("Execution time: {0} ",
                        (endExecutionTime - startExecutionTime).TotalMilliseconds                    
                        )
                        );

                    //results.Add(endExecutionTime - startExecutionTime);
                }
                
            }

            output.AppendLine("Average: ");
            for (int i = 0; i < 11; i++)
            {
                output.AppendLine(" Year " + (i + 1) + ": \t" + results.Select(r => r.ElementAt(i)).Average() );
            }


            using (StreamWriter writer = new StreamWriter(@"D:\Documents\Study Materials\Master Thesis\Main Thesis\executionTimeAnalyzer.txt"))
            {
                
                //for (int i = 1; i <= 10; i++)
                //{
                //    output += String.Format(results[i - 1].Milliseconds + " ms \n");
                //}

                writer.WriteLine(output);
            }

        }

        public void ExecuteAnalyzer(DateTime startDate, int numOfYear, StrategyAnalyzer analyzer, string fxCode)
        {
            DateTime endYear = startDate.AddYears(numOfYear);
            analyzer.Execute(startDate, endYear, 3, fxCode);
        }
    }
}
