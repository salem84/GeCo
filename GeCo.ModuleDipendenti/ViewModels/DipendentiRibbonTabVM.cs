﻿using System;
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
        //Group Anagrafica
        public ICommand NuovoDipendenteCommand { get; private set; }
        public ICommand CercaDipendenteCommand { get; private set; }

        //Group Analisi
        public ICommand RicercaRuolo { get; private set; }
        public ICommand DettagliConfrontoCommand { get; private set; }
        public ICommand ExcelCommand { get; private set; }
        public ICommand GraficoCommand { get; private set; }

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
            //Group Anagrafica
            CercaDipendenteCommand = new RelayCommand(CreaTabRicercaDipendenti);
            NuovoDipendenteCommand = new RelayCommand(CreaTabNuovoDipendente);

            //Group Analisi

            DettagliConfrontoCommand = new RelayCommand(VisualizzaDettagliConfronto);
            GraficoCommand = new RelayCommand(ToggleGrafico);
            ExcelCommand = new RelayCommand(EsportaExcel);
        }

        private void CreaTabRicercaDipendenti()
        {
            var ricercaVM = ServiceLocator.Current.GetInstance<IUnityContainer>().Resolve<RicercaDipendentiViewModel>();

            ricercaVM.AddToShell();
        }

        private void CreaTabNuovoDipendente()
        {
            //var ricercaVM = ServiceLocator.Current.GetInstance<DipendenteViewModel>();
            //Se lo faccio caricare al IoC mi crea il VM con il costruttore con più parametri
            var nuovoVM = new DipendenteViewModel();

            nuovoVM.AddToShell();
        }

        //Per il momento visualizzato solo nella master
        private void ToggleGrafico()
        {
            var container = ServiceLocator.Current.GetInstance<DipendentiWorkspaceContainerVM>();
            var activeWorkspace = container.ActiveWorkspace as ConfrontoDipendenteMasterVM;
            if (activeWorkspace != null)
            {
                activeWorkspace.ToggleGrafico();
            }
        }

        //Deve essere abilitato solo nella Details
        private void EsportaExcel()
        {
            var container = ServiceLocator.Current.GetInstance<DipendentiWorkspaceContainerVM>();
            var activeWorkspace = container.ActiveWorkspace as ConfrontoDipendenteDetailsVM;
            if (activeWorkspace != null)
            {
                activeWorkspace.EsportaExcel();
            }
        }

        //Deve essere abilitato solo nella Master
        private void VisualizzaDettagliConfronto()
        {
            var container = ServiceLocator.Current.GetInstance<DipendentiWorkspaceContainerVM>();
            var activeWorkspace = container.ActiveWorkspace as ConfrontoDipendenteMasterVM;
            if (activeWorkspace != null)
            {
                activeWorkspace.VisualizzaConfrontoDetails();
            }
        }
    }
}
