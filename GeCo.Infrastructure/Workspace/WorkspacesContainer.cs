﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;
using System.Diagnostics;
using Microsoft.Practices.Prism.Events;
using GeCo.Infrastructure.Events;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace GeCo.Infrastructure.Workspace
{
    /// <summary>
    /// ViewModel del contenitore di Workspaces, contiene le operazioni di gestione della collection di Workspace
    /// </summary>
    public abstract class WorkspaceContainer : Workspace
    {
        public override string IdWorkspace { get { return ""; } }

        /// <summary>
        /// Returns the collection of available workspaces to display.
        /// A 'workspace' is a ViewModel that can request to be closed.
        /// </summary>
        ObservableCollection<Workspace> _workspaces;

        public ObservableCollection<Workspace> Workspaces
        {
            get
            {
                if (_workspaces == null)
                {
                    _workspaces = new ObservableCollection<Workspace>();
                    _workspaces.CollectionChanged += this.OnWorkspacesChanged;
                }
                return _workspaces;
            }
        }

        public Workspace ActiveWorkspace
        {
            get { return GetActiveWorkspace(); }
        }

        void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (Workspace workspace in e.NewItems)
                    workspace.RequestClose += this.OnWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (Workspace workspace in e.OldItems)
                    workspace.RequestClose -= this.OnWorkspaceRequestClose;
        }

        void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            this.Workspaces.Remove(sender as Workspace);
        }

        Workspace GetActiveWorkspace()
        {
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
            {
                if (collectionView.CurrentItem != null)
                    return collectionView.CurrentItem as Workspace;
                else //non ho aperto ancora nessun tab, restituisco la main view
                {
                    return this;
                }
            }

            return null;
        }

        public void AggiungiPannello(Workspace vm)
        {
            Workspaces.Add(vm);
            SetActiveWorkspace(vm);
        }


        void SetActiveWorkspace(Workspace workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);

            RaisePropertyChanged("ActiveWorkspace");
        }


        /// <summary>
        /// Fatto il binding al SelectedItem di TabControl, attraverso la proprietà Tag di HeaderedContentControl
        /// Deve lanciare solo il messaggio.
        /// TODO ricontrollare binding, va anche in lettura!
        /// </summary>
        private ICommand _selectionChanged;
        public ICommand SelectionChanged
        {
            get
            {
                if (_selectionChanged == null)
                {
                    _selectionChanged = new RelayCommand(SendEventSelectionChanged);
                }
                return _selectionChanged;
            }
        }


        protected void SendEventSelectionChanged()
        {
            var eventAggregator = IoC.Get<IEventAggregator>();
            var changeWorkspaceEvent = eventAggregator.GetEvent<ChangeWorkspaceEvent>();
            changeWorkspaceEvent.Workspace = ActiveWorkspace;
            changeWorkspaceEvent.Publish(changeWorkspaceEvent);
        }
    }
}
