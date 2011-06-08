using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Events;
using GeCo.Infrastructure.Events;
using GeCo.Infrastructure.Workspace;

namespace GeCo.ModuleDipendenti.ViewModels
{
    /// <summary>
    /// Contiene l'implementazione dei command associati al ribbon
    /// </summary>
    public class DipendentiRibbonTabVM : ViewModelBase
    {
        public ICommand NuovoDipendenteCommand { get; private set; }
        public ICommand CercaDipendenteCommand { get; private set; }

        /// <summary>
        /// Costruttore
        /// </summary>
        /// <param name="workspaceContainer">viene passato dal container (è un singleton definito nell'init del modulo)</param>
        public DipendentiRibbonTabVM()
        {
            //this.workspaceContainer = workspaceContainer;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            CercaDipendenteCommand = new RelayCommand(() => CreaTabRicercaDipendenti());
            NuovoDipendenteCommand = new RelayCommand(() => CreaTabNuovoDipendente());
        }

        private void CreaTabRicercaDipendenti()
        {
            var ricercaVM = ServiceLocator.Current.GetInstance<IUnityContainer>().Resolve<RicercaDipendentiViewModel>();

            PubblicaEvento(ricercaVM);
        }

        private void CreaTabNuovoDipendente()
        {
            //var ricercaVM = ServiceLocator.Current.GetInstance<DipendenteViewModel>();
            //Se lo faccio caricare al IoC mi crea il VM con il costruttore con più parametri
            var nuovoVM = new DipendenteViewModel();

            PubblicaEvento(nuovoVM);
        }

        private void PubblicaEvento(Workspace workspace)
        {
            var eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            var addWorkspaceEvent = eventAggregator.GetEvent<AddWorkspaceEvent>();
            addWorkspaceEvent.Workspace = workspace;
            addWorkspaceEvent.Container = "ModuleDipendenti";
            addWorkspaceEvent.Publish(addWorkspaceEvent);
        }
    }
}
