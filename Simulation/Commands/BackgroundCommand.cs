using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;

namespace StrategyAnalyzerCore.Visualization.Commands
{
    /// <summary>
    ///     This class allows delegating the commanding logic to methods passed as parameters,
    ///     and enables a View to bind commands to objects that are not part of the element tree.
    /// </summary>
    public class BackgroundCommand : DelegateCommand
    {
        #region Constructors

        /// <summary>
        ///     Constructor
        /// </summary>
        public BackgroundCommand(Action executeMethod)
            : this(executeMethod, null, false)
        {
        }


        /// <summary>
        ///     Constructor
        /// </summary>
        public BackgroundCommand(Action executeMethod, Func<bool> canExecuteMethod)
            : this(executeMethod, canExecuteMethod, false)
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        public BackgroundCommand(Action executeMethod, Func<bool> canExecuteMethod, bool isAutomaticRequeryDisabled)
            : base(executeMethod, canExecuteMethod, isAutomaticRequeryDisabled)
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += PerformAction;
            _backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_backgroundWorker_RunWorkerCompleted);

        }

        void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_finishExecuteMethod != null)
                _finishExecuteMethod();
        }

        #endregion

        #region Public Methods


        /// <summary>
        ///  Execution of the command in background
        /// </summary>
        public override void Execute()
        {
            if (_executeMethod != null && !IsBusy())
            {
                //_executeMethod();
                _backgroundWorker.RunWorkerAsync();
            }
        }

        public bool IsBusy()
        {
            return _backgroundWorker.IsBusy;
        }

        public void Cancel()
        {
            if (_backgroundWorker.IsBusy)
                _backgroundWorker.CancelAsync();
        }



        private void PerformAction(object sender, DoWorkEventArgs e)
        {
            if (_executeMethod != null)
            {
                _executeMethod();
            }
        }



        #endregion


        #region Data

        private readonly Action _finishExecuteMethod = null;
        private BackgroundWorker _backgroundWorker = null;

        #endregion
    }
}