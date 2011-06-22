using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using GeCo.Infrastructure.Workspace;
using GeCo.Infrastructure.Events;
using Microsoft.Practices.Prism.Events;

namespace GeCo.ModuleRuoli.ViewModels
{
    /// <summary>
    /// Contiene l'implementazione dei command associati al ribbon
    /// </summary>
    public class RuoliRibbonTabVM : ViewModelBase
    {
        //Group Anagrafica
        public ICommand NuovoRuoloCommand { get; private set; }
        public ICommand CercaRuoliCommand { get; private set; }

        //Group Analisi
        public ICommand RicercaDipendente { get; private set; }
        public ICommand DettagliConfrontoCommand { get; private set; }
        public ICommand ExcelCommand { get; private set; }
        public ICommand GraficoCommand { get; private set; }

        /// <summary>
        /// Costruttore
        /// </summary>
        /// <param name="workspaceContainer">viene passato dal container (è un singleton definito nell'init del modulo)</param>
        public RuoliRibbonTabVM()
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            //Group Anagrafica
            CercaRuoliCommand = new RelayCommand(() => CreaTabRicercaRuoli());
            NuovoRuoloCommand = new RelayCommand(() => CreaTabNuovoRuolo());

            //Group Analisi

            DettagliConfrontoCommand = new RelayCommand(VisualizzaDettagliConfronto);
            GraficoCommand = new RelayCommand(ToggleGrafico);
            ExcelCommand = new RelayCommand(EsportaExcel);
        }

        private void CreaTabRicercaRuoli()
        {
            var ricercaVM = ServiceLocator.Current.GetInstance<IUnityContainer>().Resolve<RicercaRuoliViewModel>();
            
            ricercaVM.AddToShell();
        }

        private void CreaTabNuovoRuolo()
        {
            //var ricercaVM = ServiceLocator.Current.GetInstance<DipendenteViewModel>();
            //Se lo faccio caricare al IoC mi crea il VM con il costruttore con più parametri
            var nuovoVM = new RuoloViewModel();

            nuovoVM.AddToShell();
        }


        //Per il momento visualizzato solo nella master
        private void ToggleGrafico()
        {
            var container = ServiceLocator.Current.GetInstance<RuoliWorkspaceContainerVM>();
            var activeWorkspace = container.ActiveWorkspace as ConfrontoRuoloMasterVM;
            if (activeWorkspace != null)
            {
                activeWorkspace.ToggleGrafico();
            }
        }

        //Deve essere abilitato solo nella Details
        private void EsportaExcel()
        {
            var container = ServiceLocator.Current.GetInstance<RuoliWorkspaceContainerVM>();
            var activeWorkspace = container.ActiveWorkspace as ConfrontoRuoloDetailsVM;
            if (activeWorkspace != null)
            {
                activeWorkspace.EsportaExcel();
            }
        }

        //Deve essere abilitato solo nella Master
        private void VisualizzaDettagliConfronto()
        {
            var container = ServiceLocator.Current.GetInstance<RuoliWorkspaceContainerVM>();
            var activeWorkspace = container.ActiveWorkspace as ConfrontoRuoloMasterVM;
            if (activeWorkspace != null)
            {
                activeWorkspace.VisualizzaConfrontoDetails();
            }
        }
    }
}
