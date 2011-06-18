using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.Events;
using GalaSoft.MvvmLight;
using Microsoft.Practices.Prism.Regions;
using GeCo.Infrastructure.Events;
using GalaSoft.MvvmLight.Command;

namespace GeCo.ModuleCompetenze.ViewModels
{
    public class CompetenzeTaskButtonVM : ViewModelBase, INavigationAware
    {
        #region Fields and Commands
        
        public ICommand ShowModuleView { get; set; }

        private bool? _isChecked;
        public bool? IsChecked
        {
            get { return _isChecked; }

            set
            {
                //base.RaisePropertyChangingEvent("IsChecked");
                _isChecked = value;
                //base.RaisePropertyChangedEvent("IsChecked");
                RaisePropertyChanged("IsChecked");
            }
        }

        #endregion

        #region Constructor

        public CompetenzeTaskButtonVM()
        {
            this.Initialize();
        }

        #endregion

        #region INavigationAware Members

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new System.NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new System.NotImplementedException();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Sets the IsChecked state of the Task Button when navigation is completed.
        /// </summary>
        /// <param name="publisher">The publisher of the event.</param>
        private void OnNavigationCompleted(string publisher)
        {
            // Exit if this module published the event
            if (publisher == Names.MODULE_NAME) return;

            // Otherwise, uncheck this button
            this.IsChecked = false;
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            // Initialize command properties
            this.ShowModuleView = new RelayCommand(Execute);

            // Initialize administrative properties
            this.IsChecked = false;

            // Subscribe to Composite Presentation Events
            var eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            var navigationCompletedEvent = eventAggregator.GetEvent<NavigationCompletedEvent>();
            navigationCompletedEvent.Subscribe(OnNavigationCompleted, ThreadOption.UIThread);
        }

        #endregion

        #region Show Module Command - Execute

        /// <summary>
        /// Executes the ShowModuleAViewCommand
        /// </summary>
        public void Execute()
        {
            // Initialize
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();

            // Show Ribbon Tab
            //var moduleRibbonTab = new Uri("DipendentiRibbonTab", UriKind.Relative);
            //regionManager.RequestNavigate("RibbonRegion", moduleRibbonTab);


            // Richiamo NavigationCompleted() callback all'ultima richiesta

            // Mostra Workspace
            var moduleWorkspace = new Uri("CompetenzeView", UriKind.Relative);
            regionManager.RequestNavigate("WorkspaceRegion", moduleWorkspace, NavigationCompleted);
        }

        /// <summary>
        /// Callback richiamato quando la navigazione è completata
        /// </summary>
        /// <param name="result">Risultato della navigazione</param>
        private void NavigationCompleted(NavigationResult result)
        {
            // Concludi se la navigazione non è andata a buon fine
            if (result.Result != true) return;

            // Notifico l'evento
            var eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            var navigationCompletedEvent = eventAggregator.GetEvent<NavigationCompletedEvent>();
            navigationCompletedEvent.Publish(Names.MODULE_NAME);
        }

        #endregion
    }
}
