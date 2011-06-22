using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;

namespace GeCo.Infrastructure
{
    public static class IoC
    {
        public static TService Get<TService>()
        {
            return ServiceLocator.Current.GetInstance<TService>();
        }

        public static TService Get<TService>(string key)
        {
            return ServiceLocator.Current.GetInstance<TService>(key);
        }


        /// <summary>
        /// Mi restituisce l'istanza del workspace attivo se è di tipo TWorkspace, altrimenti ritorna null
        /// </summary>
        /// <typeparam name="TWorkspaceContainer">WorkspaceContainer</typeparam>
        /// <typeparam name="TWorkspace">Workspace</typeparam>
        /// <returns></returns>
        public static TWorkspace GetActiveWorkspace<TWorkspaceContainer, TWorkspace>() 
            where TWorkspaceContainer : Workspace.WorkspaceContainer 
            where TWorkspace : Workspace.Workspace
        {
            var container = Get<TWorkspaceContainer>();
            var activeWorkspace = container.ActiveWorkspace as TWorkspace;

            return activeWorkspace;
        }
    }
}
