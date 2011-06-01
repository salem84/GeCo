using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace GeCo.Varie.ClientFacade
{
    public class BackgroundWorkerHelper : IDisposable
    {
        #region Private Properties
        private bool disposed = false;
        private BackgroundWorker _backgroundWorker;
        private bool _supportsCancellation = false;
        private bool _reportsProgress = false;
        private bool _cancelPending = false;
        #endregion Private Properties

        #region Constructor
        public BackgroundWorkerHelper() { }
        #endregion Constructor

        #region Public Properties
        /// <summary>
        /// Gets/Sets whether worker supports cancellation
        /// </summary>
        public bool WorkerSupportsCancellation
        {
            get { lock (this) { return _supportsCancellation; } }
            set { lock (this) { _supportsCancellation = value; } }
        }
        /// <summary>
        /// Gets/Sets whether worker supports progress reporting
        /// </summary>
        public bool WorkerReportsProgress
        {
            get { lock (this) { return _reportsProgress; } }
            set { lock (this) { _reportsProgress = value; } }
        }
        /// <summary>
        /// Gets whether there is a pending cancellation
        /// </summary>
        public bool CancellationPending
        {
            get { lock (this) { return _cancelPending; } }
        }
        /// <summary>
        /// Sets the progress value as integer percentage
        /// </summary>
        public int SetProgressPercent
        {
            set { _backgroundWorker.ReportProgress(value); }
        }
        #endregion Public Properties

        #region Event Handlers
        public delegate void BackgroundDoWorkEventHandler();
        public event BackgroundDoWorkEventHandler BackgroundDoWorkEvent;

        public delegate void BackgroundCompletedEventHandler();
        public event BackgroundCompletedEventHandler BackgroundCompletedEvent;

        public delegate void BackgroundProgressChangedEventHandler(int percentComplete);
        public event BackgroundProgressChangedEventHandler BackgroundProgressChangedEvent;
        #endregion Event Handlers

        #region Method Handlers
        public delegate void BackgroundMethodHandler();
        public BackgroundMethodHandler DoWorkMethod;
        public BackgroundMethodHandler RunWorkerCompletedMethod;

        public delegate void BackgroundProgressMethodHandler(int percentComplete);
        public BackgroundProgressMethodHandler ProgressChangedMethod;
        #endregion Method Handlers


        #region Public Methods
        /// <summary>
        /// Runs the events created in the background thread
        /// </summary>
        public void RunBackgroundWorker()
        {
            RunBackgroundWorker(null, null, null);
        }
        /// <summary>
        /// Runs the methods in the background thread
        /// </summary>
        /// <param name="doWorkMethod"></param>
        /// <param name="progressChangedMethod"></param>
        /// <param name="runWorkerCompletedMethod"></param>
        public void RunBackgroundWorker(BackgroundMethodHandler doWorkMethod,
            BackgroundProgressMethodHandler progressChangedMethod,
            BackgroundMethodHandler runWorkerCompletedMethod)
        {
            // Set methods:
            if (doWorkMethod != null)
            {
                DoWorkMethod = doWorkMethod;
                ProgressChangedMethod = progressChangedMethod;
                RunWorkerCompletedMethod = runWorkerCompletedMethod;
                // Set events to null
                BackgroundDoWorkEvent = null;
                BackgroundCompletedEvent = null;
                BackgroundProgressChangedEvent = null;
            }

            // BackgroundWorker
            _backgroundWorker = new BackgroundWorker();
            // DoWork
            _backgroundWorker.DoWork +=
                new DoWorkEventHandler(BackgroundWorker_DoWork);
            // RunWorkerCompleted
            _backgroundWorker.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
            // ProgressChanged
            _backgroundWorker.ProgressChanged +=
                new ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);

            _backgroundWorker.WorkerReportsProgress = _reportsProgress;
            _backgroundWorker.WorkerSupportsCancellation = _supportsCancellation;
            _backgroundWorker.RunWorkerAsync();
        }
        /// <summary>
        /// CancelBackgroundWorker
        /// </summary>
        public void CancelBackgroundWorker()
        {
            if (_supportsCancellation)
            {
                lock (this)
                {
                    _cancelPending = true;
                    _backgroundWorker.CancelAsync();
                }
            }
        }
        #endregion Public Methods

        #region Private Methods
        /// <summary>
        /// Event for DoWork
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (DoWorkMethod != null)
            {
                _cancelPending = false;
                DoWorkMethod();
            }
            else if (BackgroundDoWorkEvent != null)
            {
                _cancelPending = false;
                BackgroundDoWorkEvent();
            }
        }
        /// <summary>
        /// Event for Run Worker Completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (RunWorkerCompletedMethod != null)
            {
                _cancelPending = false;
                RunWorkerCompletedMethod();
            }
            else if (BackgroundCompletedEvent != null)
                BackgroundCompletedEvent();
        }
        /// <summary>
        /// Event for Progress Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (ProgressChangedMethod != null)
            {
                _cancelPending = false;
                ProgressChangedMethod(e.ProgressPercentage);
            }
            else if (BackgroundProgressChangedEvent != null && _reportsProgress)
                BackgroundProgressChangedEvent(e.ProgressPercentage);
        }
        #endregion Private Methods

        #region IDisposable Members
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
            }
        }
        ~BackgroundWorkerHelper()
        {
            Dispose(false);
        }
        #endregion IDisposable Members
    }
}
