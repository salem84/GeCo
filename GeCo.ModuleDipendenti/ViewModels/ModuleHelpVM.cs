using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GeCo.Infrastructure;
using Microsoft.Practices.Prism.Events;
using GeCo.Infrastructure.Events;
using System.Windows;

namespace GeCo.ModuleDipendenti.ViewModels
{
    public class ModuleHelpVM : ViewModelBase
    {
        private string _titolo;
        public string Titolo
        {
            get { return _titolo; }
            set
            {
                if (_titolo != value)
                {
                    _titolo = value;
                    RaisePropertyChanged("Titolo");
                }
            }
        }

        private string _testo;
        public string Testo
        {
            get { return _testo; }
            set
            {
                if (_testo != value)
                {
                    _testo = value;
                    RaisePropertyChanged("Testo");
                }
            }
        }
        
        
        public ModuleHelpVM()
        {
            var eventAggregator = IoC.Get<IEventAggregator>();
            var changeWorkspaceEvent = eventAggregator.GetEvent<ChangeWorkspaceEvent>();
            changeWorkspaceEvent.Subscribe(OnChangeWorkspace, ThreadOption.UIThread);
        }


        public void OnChangeWorkspace(ChangeWorkspaceEvent e)
        {
            string idActiveWorkspace = e.Workspace.IdWorkspace;
            if (Application.Current.Resources.Contains(idActiveWorkspace))
            {
                var panel = (HelpPanel)Application.Current.Resources[idActiveWorkspace];
                Titolo = panel.Titolo;
                Testo = panel.Testo;
            }
            else
            {
                Titolo = Testo = "";
            }
            
        }
    }

    
}
