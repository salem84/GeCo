using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.Infrastructure.Workspace;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.Events;
using GeCo.Infrastructure.Events;
using GeCo.Infrastructure;

namespace GeCo.ModuleRuoli.ViewModels
{
    public class RuoliWorkspaceContainerVM : WorkspaceContainer
    {
        protected override string containerName { get { return Names.MODULE_NAME; } }

        public RuoliWorkspaceContainerVM()
        {
            var eventAggregator = IoC.Get<IEventAggregator>();
            var addWorkspaceEvent = eventAggregator.GetEvent<AddWorkspaceEvent>();
            addWorkspaceEvent.Subscribe(OnAddWorkspace, ThreadOption.UIThread);

            //Aggiungo il workspace di ricerca (per default)
            var ricercaVM = IoC.Get<RicercaRuoliViewModel>();
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
