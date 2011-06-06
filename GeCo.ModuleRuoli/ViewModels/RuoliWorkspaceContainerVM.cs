using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.Infrastructure.Workspace;

namespace GeCo.ModuleRuoli.ViewModels
{
    public class RuoliWorkspaceContainerVM : WorkspaceContainer
    {
        public string ora;
        public RuoliWorkspaceContainerVM()
        {
            ora = DateTime.Now.ToLongTimeString();
        }
    }
}
