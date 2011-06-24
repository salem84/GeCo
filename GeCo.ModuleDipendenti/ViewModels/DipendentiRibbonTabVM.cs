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
using GeCo.Infrastructure;

namespace GeCo.ModuleDipendenti.ViewModels
{
    /// <summary>
    /// Contiene l'implementazione dei command associati al ribbon
    /// </summary>
    public class DipendentiRibbonTabVM : ViewModelBase
    {
        //Group Anagrafica
        public ICommand NuovoDipendenteCommand { get; private set; }
        public ICommand RicercaDipendenteCommand { get; private set; }

        //Group Analisi
        public ICommand VisualizzaConfrontoMasterCommand { get; private set; }
        public ICommand VisualizzaConfrontoDetailsCommand { get; private set; }
        public ICommand ExcelCommand { get; private set; }
        public ICommand GraficoCommand { get; private set; }
              
        

        /// <summary>
        /// Costruttore
        /// </summary>
        public DipendentiRibbonTabVM()
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            //Group Anagrafica
            RicercaDipendenteCommand = new RelayCommand(CreaTabRicercaDipendenti);
            NuovoDipendenteCommand = new RelayCommand(CreaTabNuovoDipendente);

            //Group Analisi
            VisualizzaConfrontoMasterCommand = new RelayCommand(VisualizzaConfrontoMaster, CanVisualizzaConfrontoMaster);
            VisualizzaConfrontoDetailsCommand = new RelayCommand(VisualizzaConfrontoDetails, CanVisualizzaConfrontoDetails);
            GraficoCommand = new RelayCommand(ToggleGrafico, CanToggleGrafico);
            ExcelCommand = new RelayCommand(EsportaExcel, CanEsportaExcel);
        }

        #region COMMANDS
        
        private void CreaTabRicercaDipendenti()
        {
            var ricercaVM = IoC.Get<RicercaDipendentiViewModel>();

            ricercaVM.AddToShell();
        }

        private void CreaTabNuovoDipendente()
        {
            //var ricercaVM = ServiceLocator.Current.GetInstance<DipendenteViewModel>();
            //Se lo faccio caricare al IoC mi crea il VM con il costruttore con più parametri
            var nuovoVM = new DipendenteViewModel();

            nuovoVM.AddToShell();
        }

        #region VISUALIZZA CONFRONTO COMMAND

        //Per il momento visualizzato solo nella master
        private void VisualizzaConfrontoMaster()
        {
            var activeWorkspace = IoC.GetActiveWorkspace<DipendentiWorkspaceContainerVM, DipendenteViewModel>();
            //L'if non serve visto che il button è disabilitato quando non è attivo il workspace giusto
            activeWorkspace.AvviaConfronto();
        }

        /// <summary>
        /// Definisce quando è abilitato il RibbonToggleButton del Chart
        /// </summary>
        /// <returns>false se il workspace attivo non è ConfrontoDipendenteMaster o non è selezionato alcun elemento</returns>
        private bool CanVisualizzaConfrontoMaster()
        {
            var activeWorkspace = IoC.GetActiveWorkspace<DipendentiWorkspaceContainerVM, DipendenteViewModel>();

            //Se sto nella schermata di master, l'activeworkspace è diverso da null
            return activeWorkspace != null;
        }

        #endregion

        #region TOGGLE GRAFICO COMMAND

        //Per il momento visualizzato solo nella master
        private void ToggleGrafico()
        {
            var activeWorkspace = IoC.GetActiveWorkspace<DipendentiWorkspaceContainerVM, ConfrontoDipendenteMasterVM>();
            //L'if non serve visto che il button è disabilitato quando non è attivo il workspace giusto
            
            //C'è il campo GraficoVisibile del RibbonVM che conserva l'informazione per lo stato del ToggleButton
            activeWorkspace.GraficoVisibile = !activeWorkspace.GraficoVisibile;
        }

        /// <summary>
        /// Definisce quando è abilitato il RibbonToggleButton del Chart
        /// </summary>
        /// <returns>false se il workspace attivo non è ConfrontoDipendenteMaster o non è selezionato alcun elemento</returns>
        private bool CanToggleGrafico()
        {
            var activeWorkspace = IoC.GetActiveWorkspace<DipendentiWorkspaceContainerVM, ConfrontoDipendenteMasterVM>();

            //Se sto nella schermata di master, l'activeworkspace è diverso da null
            return activeWorkspace != null && activeWorkspace.RisultatoSelezionato != null;
        }

        #endregion

        #region ESPORTA EXCEL COMMAND

        private void EsportaExcel()
        {
            var activeWorkspace = IoC.GetActiveWorkspace<DipendentiWorkspaceContainerVM, ConfrontoDipendenteDetailsVM>();
            activeWorkspace.EsportaExcel();
        }

        //Deve essere abilitato solo nella Details
        private bool CanEsportaExcel()
        {
            var activeWorkspace = IoC.GetActiveWorkspace<DipendentiWorkspaceContainerVM, ConfrontoDipendenteDetailsVM>();
            return activeWorkspace != null;
        }

        #endregion

        #region VISUALIZZA DETTAGLI COMMAND
        
        //Deve essere abilitato solo nella Master
        private void VisualizzaConfrontoDetails()
        {
            var activeWorkspace = IoC.GetActiveWorkspace<DipendentiWorkspaceContainerVM, ConfrontoDipendenteMasterVM>();
            activeWorkspace.VisualizzaConfrontoDetails();
        }

        private bool CanVisualizzaConfrontoDetails()
        {
            var activeWorkspace = IoC.GetActiveWorkspace<DipendentiWorkspaceContainerVM, ConfrontoDipendenteMasterVM>();
            return activeWorkspace != null && activeWorkspace.RisultatoSelezionato != null;
        }

        #endregion

        #endregion

       
    }
}
