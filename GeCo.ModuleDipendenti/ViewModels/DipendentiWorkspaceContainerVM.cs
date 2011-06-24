using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.Infrastructure.Workspace;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using GeCo.Infrastructure.Events;

namespace GeCo.ModuleDipendenti.ViewModels
{
    public class DipendentiWorkspaceContainerVM : WorkspaceContainer
    {
        protected override string containerName { get { return Names.MODULE_NAME; } }

        public DipendentiWorkspaceContainerVM()
        {
            //Mi sottoscrivo all'evento per ricevere l'aggiunta di workspace
            var eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            var addWorkspaceEvent = eventAggregator.GetEvent<AddWorkspaceEvent>();
            addWorkspaceEvent.Subscribe(OnAddWorkspace, ThreadOption.UIThread);

            //Aggiungo il workspace di ricerca (per default)
            var ricercaVM = ServiceLocator.Current.GetInstance<RicercaDipendentiViewModel>();
            ricercaVM.AddToShell();
        }

        private void OnAddWorkspace(AddWorkspaceEvent evento)
        {
            if (evento.Container == Names.MODULE_NAME)
            {
                this.AggiungiPannello(evento.Workspace);
            }   
        }
    }
}
