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
        public DipendentiWorkspaceContainerVM()
        {
            var eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            var addWorkspaceEvent = eventAggregator.GetEvent<AddWorkspaceEvent>();
            addWorkspaceEvent.Subscribe(OnAddWorkspace, ThreadOption.UIThread);
        }

        private void OnAddWorkspace(AddWorkspaceEvent evento)
        {
            if (evento.Container == "ModuleDipendenti")
            {
                this.AggiungiPannello(evento.Workspace);
            }   
        }
    }
}
