using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows;
using Microsoft.Practices.Prism.Events;
using GeCo.Infrastructure.Events;

namespace GeCo.Infrastructure.Workspace
{
    public abstract class HelpWorkspace : ViewModelBase
    {
        protected abstract string[] views { get; set; }

        private Dictionary<string, Visibility> _visibilityViews;
        public Dictionary<string, Visibility> VisibilityViews
        {
            get { return _visibilityViews; }
            set
            {
                if (_visibilityViews != value)
                {
                    _visibilityViews = value;
                    RaisePropertyChanged("VisibilityViews");
                }
            }
        }

        protected void SubscribeEvent()
        {
            var eventAggregator = IoC.Get<IEventAggregator>();
            var changeWorkspaceEvent = eventAggregator.GetEvent<ChangeWorkspaceEvent>();
            changeWorkspaceEvent.Subscribe(OnChangeWorkspace, ThreadOption.UIThread);
        }

        protected void OnChangeWorkspace(ChangeWorkspaceEvent e)
        {
            string idActiveWorkspace = e.Workspace.IdWorkspace;

            VisibilityViews = CreateDict(idActiveWorkspace);

        }

        protected Dictionary<string, Visibility> CreateDict(string visibleView)
        {
            var data = new Dictionary<string, Visibility>();

            foreach (var v in views)
            {
                Visibility visibility = v == visibleView ? Visibility.Visible : Visibility.Collapsed;
                if (!data.ContainsKey(v))
                    data.Add(v, visibility);
            }

            return data;
        }

        protected void StartView(string startView)
        {
            VisibilityViews = CreateDict(startView);
        }

    }
}
