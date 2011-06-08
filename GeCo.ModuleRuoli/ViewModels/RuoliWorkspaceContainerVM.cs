using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.Infrastructure.Workspace;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.Events;
using GeCo.Infrastructure.Events;

namespace GeCo.ModuleRuoli.ViewModels
{
    public class RuoliWorkspaceContainerVM : WorkspaceContainer
    {
        public RuoliWorkspaceContainerVM()
        {
            var eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            var addWorkspaceEvent = eventAggregator.GetEvent<AddWorkspaceEvent>();
            addWorkspaceEvent.Subscribe(OnAddWorkspace, ThreadOption.UIThread);
        }

        private void OnAddWorkspace(AddWorkspaceEvent evento)
        {
            if (evento.Container == "ModuleRuoli")
            {
                this.AggiungiPannello(evento.Workspace);
            }   
        }
    }
}
