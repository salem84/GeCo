using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GeCo.Infrastructure;
using GeCo.Infrastructure.Workspace;
using Microsoft.Practices.Prism.Events;
using GeCo.Infrastructure.Events;
using System.Windows;
using System.Windows.Data;

namespace GeCo.ModuleDipendenti.ViewModels
{
    public class ModuleHelpVM : HelpWorkspace
    {

        protected override string[] views { get; set; }
       

        
        public ModuleHelpVM()
        {
            views = new string[] 
            { 
                Names.START_VIEW, 
                Names.VIEW_CONFRONTO_DETAILS, 
                Names.VIEW_CONFRONTO_MASTER, 
                Names.VIEW_DIPENDENTE, 
                Names.VIEW_RICERCA 
            };

            SubscribeEvent();
            StartView(Names.START_VIEW);
        }

        
    }


   
}
