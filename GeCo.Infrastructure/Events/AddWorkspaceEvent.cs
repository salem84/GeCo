using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;

namespace GeCo.Infrastructure.Events
{
    public class AddWorkspaceEvent : CompositePresentationEvent<AddWorkspaceEvent>
    {
        public Workspace.Workspace Workspace { get; set; }
        public string Container { get; set; }
    }
}
