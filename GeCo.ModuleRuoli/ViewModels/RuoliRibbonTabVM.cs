using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace GeCo.ModuleRuoli.ViewModels
{
    /// <summary>
    /// Contiene l'implementazione dei command associati al ribbon
    /// </summary>
    public class RuoliRibbonTabVM : ViewModelBase
    {
        public ICommand NuovoRuoloCommand { get; private set; }
        public ICommand CercaRuoliCommand { get; private set; }

        private RuoliWorkspaceContainerVM workspaceContainer;

        /// <summary>
        /// Costruttore
        /// </summary>
        /// <param name="workspaceContainer">viene passato dal container (è un singleton definito nell'init del modulo)</param>
        public RuoliRibbonTabVM(RuoliWorkspaceContainerVM workspaceContainer)
        {
            this.workspaceContainer = workspaceContainer;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            CercaRuoliCommand = new RelayCommand(() => CreaTabRicercaRuoli());
            NuovoRuoloCommand = new RelayCommand(() => CreaTabNuovoRuolo());
        }

        private void CreaTabRicercaRuoli()
        {
            var ricercaVM = ServiceLocator.Current.GetInstance<IUnityContainer>().Resolve<RicercaRuoliViewModel>();
            
            
            workspaceContainer.AggiungiPannello(ricercaVM);
        }

        private void CreaTabNuovoRuolo()
        {
            //var ricercaVM = ServiceLocator.Current.GetInstance<DipendenteViewModel>();
            //Se lo faccio caricare al IoC mi crea il VM con il costruttore con più parametri
            var nuovoVM = new RuoloViewModel();

            workspaceContainer.AggiungiPannello(nuovoVM);
        }
    }
}
