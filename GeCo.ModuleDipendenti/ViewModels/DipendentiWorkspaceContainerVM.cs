using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.Infrastructure.Workspace;

namespace GeCo.ModuleDipendenti.ViewModels
{
    public class DipendentiWorkspaceContainerVM : WorkspaceContainer
    {
        public string ora;
        public DipendentiWorkspaceContainerVM()
        {
            ora = DateTime.Now.ToLongTimeString();
        }
    }
}
