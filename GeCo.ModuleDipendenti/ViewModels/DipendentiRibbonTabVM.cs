using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace GeCo.ModuleDipendenti.ViewModels
{
    /// <summary>
    /// Contiene l'implementazione dei command associati al ribbon
    /// </summary>
    public class DipendentiRibbonTabVM : ViewModelBase
    {
        public ICommand NuovoDipendenteCommand { get; private set; }
        public ICommand CercaDipendenteCommand { get; private set; }

        private DipendentiWorkspaceContainerVM workspaceContainer;

        /// <summary>
        /// Costruttore
        /// </summary>
        /// <param name="workspaceContainer">viene passato dal container (è un singleton definito nell'init del modulo)</param>
        public DipendentiRibbonTabVM(DipendentiWorkspaceContainerVM workspaceContainer)
        {
            this.workspaceContainer = workspaceContainer;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            CercaDipendenteCommand = new RelayCommand(
                () => workspaceContainer.AggiungiPannello(new RicercaDipendentiViewModel()));
        }
    }
}
