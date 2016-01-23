using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay.Charts;
using StrategyAnalyzerCore;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using StrategyAnalyzerCore.Visualization.Wpf.TabControl;
using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;
using StrategyAnalyzerCore.Visualization.ViewModels;
using StrategyAnalyzerCore.Visualization.Models;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;
using System.Windows.Media;
using FXStrategy.Analyzer;
using System.Text;

namespace Simulation
{

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainView : Window
    {
        StrategyAnalyzer _analyzeManager;
        private StrategyAnalyzer AnalyzeManager
        {
            get { 
                if(_analyzeManager==null)
                    _analyzeManager = new StrategyAnalyzer();
                return _analyzeManager; }
            set { _analyzeManager = value; }
        }
        private Dictionary<StrategyCodeVM, IEnumerable<DateIndex>> _vmDateIndexDict;

        DateTime _startDate;
        DateTime _endDate;
        int _numberOfMonths;

        MainVM _mainVM;

        private DateTime _executionStartTime;
        private TimeSpan _timeElapsed;

        public MainView()
        {
            InitializeComponent();
            startDateEdit.DateTime = new DateTime(2000, 3, 4);
            endDateEdit.DateTime = new DateTime(2010, 3, 4);

            _mainVM = new MainVM();
            this.DataContext = _mainVM;

            _vmDateIndexDict = new Dictionary<StrategyCodeVM, IEnumerable<DateIndex>>();

          
            tabControl.SelectedIndex = 0;
        }
        
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb != null)
                tb.SelectAll();
        }

        private void tabControl_NewTabItem(object sender, NewTabItemEventArgs e)
        {
            _mainVM.CreateNewStrategyCodeCmd.Execute();
        }

        private void runButton_Click(object sender, RoutedEventArgs e)
        {
            if (startDateEdit.DateTime < new DateTime(2000, 1, 1))
            {
                System.Windows.MessageBox.Show(@"The start date cannot be earlier than 1/1/2000", "Date out of range", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (endDateEdit.DateTime > new DateTime(2010, 12, 31))
            {
                System.Windows.MessageBox.Show(@"The end date cannot be later than 31/12/2010", "Date out of range", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (startDateEdit.DateTime > endDateEdit.DateTime)
            {
                System.Windows.MessageBox.Show(@"The start date cannot be later than end date ", "Date out of range", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }


            _executionStartTime = DateTime.Now;
            _startDate = startDateEdit.DateTime;
            _endDate = endDateEdit.DateTime;
            _numberOfMonths = (int)monthSpinEdit.Value;

            // 
            // For testing execution time
            //
            //List<List<TimeSpan>> elapsedTimes = new List<List<TimeSpan>>();

            //for (int i = 0; i < 10; i++)
            //{
            //    elapsedTimes.Add(new List<TimeSpan>());
            //    for (int j = 0; j < 10; j++)
            //    {
            //        plotter.Children.ToList().ForEach(c =>
            //        {
            //            if (c is LineGraph)
            //                plotter.Children.Remove(c);
            //        });
            //        _mainVM.StrategyCodeVMs.Clear();

            //        for (int k = 0; k < i + 1; k++)
            //        {
            //            _mainVM.StrategyCodeVMs.Add(new StrategyCodeVM()
            //            {
            //                Code = testingCode,
            //                Header = "Strategy " + k
            //            });
            //        }

            //        _executionStartTime = DateTime.Now;

            //        plotter.Children.ToList().ForEach(c =>
            //        {
            //            if (c is LineGraph)
            //                plotter.Children.Remove(c);
            //        });



            //        // calculate date index in parallel for each strategy code
            //        _vmDateIndexDict.Clear();

            //        List<Task> tasks = new List<Task>();

            //        _mainVM.StrategyCodeVMs.ToList().ForEach(codeVM =>
            //        {
            //            tasks.Add(Task.Factory.StartNew(() =>
            //            {
            //                _vmDateIndexDict.Add(codeVM,
            //                    this.AnalyzeManager.Execute(
            //                    _startDate, _endDate, _numberOfMonths, codeVM.Code));
            //            }));

            //        });


            //        tasks.ForEach(t => t.Wait());

            //        elapsedTimes[i].Add(DateTime.Now - _executionStartTime);
            //    }
            //}

            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < 10; i++)
            //{
            //    sb.AppendLine("No. Of Strategy: " + i);
            //    for (int j = 0; j < 10; j++)
            //    {
            //        sb.AppendLine(String.Format("Elapsed Time: \t {0}", elapsedTimes[i][j].TotalMilliseconds));
            //    }
            //    sb.AppendLine();
            //}

            //for (int i = 0; i < 10; i++)
            //{
            //    sb.AppendLine(String.Format("Average run time for {0} strategies is : \t {1}", i + 1, elapsedTimes[i].Average(et => et.TotalMilliseconds)));
            //}

            //using (StreamWriter writer = new StreamWriter(@"D:\Documents\Study Materials\Master Thesis\Main Thesis\executionTimeGUIAndAnalyzer.txt"))
            //{
            //    writer.WriteLine(sb);
            //}
            
            tabControl.IsEnabled = false;
            groupBox1.IsEnabled = false;
            plotter.Children.ToList().ForEach(c =>
            {
                if (c is LineGraph)
                    plotter.Children.Remove(c);
            });



            // calculate date index in parallel for each strategy code
            _vmDateIndexDict.Clear();

            List<Task> tasks = new List<Task>();

            _mainVM.StrategyCodeVMs.ToList().ForEach(codeVM =>
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    try
                    {
                        _vmDateIndexDict.Add(codeVM,
                            this.AnalyzeManager.Execute(
                            _startDate, _endDate, _numberOfMonths, codeVM.Code));
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show("An error is occurred. Below is the exception message: \n" + ex.Message,"Error",MessageBoxButton.OK,MessageBoxImage.Error );
                    }
                }));

            });

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.ContinueWhenAll(
                tasks.ToArray(),
                x =>
                {
                    AddLineGraph();
                    _timeElapsed = DateTime.Now - _executionStartTime;
                    tabControl.IsEnabled = true;
                    groupBox1.IsEnabled = true;
                    System.Windows.MessageBox.Show(String.Format("Time Elapsed: {0} ms", _timeElapsed.TotalMilliseconds));
                },
                new CancellationToken(),
                TaskContinuationOptions.None,
                scheduler);        
        }

        private void AddLineGraph()
        {

            foreach (var vm in this._mainVM.StrategyCodeVMs)
            {
                if(_vmDateIndexDict.Keys.Contains(vm))
                    plotter.AddLineGraph(CreateDateIndexDataSource(_vmDateIndexDict[vm]), 1, vm.Header);

            }
        }

        private EnumerableDataSource<DateIndex> CreateDateIndexDataSource(IEnumerable<DateIndex> dateIndexes)
        {
            EnumerableDataSource<DateIndex> ds = new EnumerableDataSource<DateIndex>(dateIndexes);
            ds.SetXMapping(ci => dateAxis.ConvertToDouble(ci.Date));
            ds.SetYMapping(ci => Convert.ToDouble(ci.Index));
            return ds;
        }

    }

}
