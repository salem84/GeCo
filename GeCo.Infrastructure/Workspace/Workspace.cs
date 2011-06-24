using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using GeCo.Infrastructure.ClientFacade;
using System.Windows.Threading;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.Events;
using GeCo.Infrastructure.Events;

namespace GeCo.Infrastructure.Workspace
{
    public abstract class Workspace : ViewModelBase
    {
        #region Fields

        RelayCommand _closeCommand;
        
        /// <summary>
        /// Returns the user-friendly name of this object.
        /// Child classes can set this property to a new value,
        /// or override it to determine the value on-demand.
        /// </summary>
        //public virtual string DisplayTabName { get; protected set; }
        private string _displayTabName;
        public virtual string DisplayTabName
        {
            get { return _displayTabName; }
            set
            {
                if (_displayTabName != value)
                {
                    _displayTabName = value;
                    RaisePropertyChanged("DisplayTabName");
                }
            }
        }
        


        /// <summary>
        /// Proprietà per la gestione della status bar
        /// </summary>
        private string _stato;
        public string Stato
        {
            get { return _stato; }
            set
            {
                if (_stato != value)
                {
                    _stato = value;
                    RaisePropertyChanged("Stato");
                }
            }
        }


        protected abstract string containerName { get; }
        public abstract string IdWorkspace { get; }

        #endregion // Fields

        #region Constructor

        protected Workspace()
        {
            IsProgressVisible = false;
        }

        #endregion // Constructor

        #region CloseCommand

        /// <summary>
        /// Returns the command that, when invoked, attempts
        /// to remove this workspace from the user interface.
        /// </summary>
        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                    _closeCommand = new RelayCommand(() => this.OnRequestClose());

                return _closeCommand;
            }
        }

        #endregion // CloseCommand

        #region RequestClose [event]

        /// <summary>
        /// Raised when this workspace should be removed from the UI.
        /// </summary>
        public event EventHandler RequestClose;

        void OnRequestClose()
        {
            EventHandler handler = this.RequestClose;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion // RequestClose [event]

        #region BackgroundWorker Methods
        
        BackgroundWorkerHelper backgroundWorkerHelper = null;
        DispatcherTimer dispatcherTimer = null;

        #region Proprietà Background e StatusBar
        
        private int _progressPercent;
        public int ProgressPercent
        {
            get { return _progressPercent; }
            set
            {
                if (_progressPercent != value)
                {
                    _progressPercent = value;
                    RaisePropertyChanged("ProgressPercent");
                }
            }
        }

        private int _progressMaximum;
        public int ProgressMaximum
        {
            get { return _progressMaximum; }
            set
            {
                if (_progressMaximum != value)
                {
                    _progressMaximum = value;
                    RaisePropertyChanged("ProgressMaximum");
                }
            }
        }

        protected bool IsCancellationPending
        {
            get
            {
                return backgroundWorkerHelper != null ? backgroundWorkerHelper.CancellationPending : false;
            }
        }

        private bool _progressVisible;
        public bool IsProgressVisible
        {
            get { return _progressVisible; }
            set
            {
                if (_progressVisible != value)
                {
                    _progressVisible = value;
                    RaisePropertyChanged("IsProgressVisible");
                }
            }
        }
        
        #endregion

        protected void StartBackgroundAutoProgress(Action DoWork)
        {
            StartBackground(DoWork, true);
        }

        protected void StartBackground(Action DoWork)
        {
            StartBackground(DoWork, false);
        }

        private void StartBackground(Action DoWork, bool automaticProgress)
        {
            backgroundWorkerHelper = new BackgroundWorkerHelper();
            //Non funziona: l'evento progress non viene lanciato neanche nella classe Helper
            backgroundWorkerHelper.WorkerReportsProgress = true;
            backgroundWorkerHelper.WorkerSupportsCancellation = true;

            backgroundWorkerHelper.BackgroundDoWorkEvent += new BackgroundWorkerHelper.BackgroundDoWorkEventHandler(DoWork);
            //backgroundWorkerHelper.BackgroundProgressChangedEvent += new BackgroundWorkerHelper.BackgroundProgressChangedEventHandler(BackgroundWorkerProgressChanged);
            backgroundWorkerHelper.BackgroundCompletedEvent += new BackgroundWorkerHelper.BackgroundCompletedEventHandler(BackgroundWorkerCompleted);
            // Run Background
            backgroundWorkerHelper.RunBackgroundWorker();

            //Resetto e rendo visibile la progressBar
            ProgressPercent = 0;
            IsProgressVisible = true;
            ProgressMaximum = 100;
            
            if (automaticProgress)
            {
                ProgressMaximum = 20;
                //TODO si può mettere l'animation nello xaml?
                dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Interval = new TimeSpan(0,0,0,0,50);
                dispatcherTimer.Tick += (sender, args) =>
                {
                    ProgressPercent = ++ProgressPercent % ProgressMaximum;   
                };
                
                dispatcherTimer.Start();
            }
        }

        protected void BackgroundWorkerCompleted()
        {
            Stato = "Ready";
            ProgressPercent = 100;
            backgroundWorkerHelper = null;
            if (dispatcherTimer != null)
            {
                dispatcherTimer.Stop();
                dispatcherTimer = null;
            }

            IsProgressVisible = false;
        }        

        protected void CancelBackgroundWorker()
        {
            if (backgroundWorkerHelper != null)
                backgroundWorkerHelper.CancelBackgroundWorker();
        }

        #endregion


        /// <summary>
        /// Pubblica un evento intercettato dalla shell, per aggiungere un TAB
        /// </summary>
        public void AddToShell()
        {
            var eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            var addWorkspaceEvent = eventAggregator.GetEvent<AddWorkspaceEvent>();
            addWorkspaceEvent.Workspace = this;
            addWorkspaceEvent.Container = containerName;
            addWorkspaceEvent.Publish(addWorkspaceEvent);
        }
    }
}
