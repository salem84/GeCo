using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;
using System.Diagnostics;

namespace GeCo.Infrastructure.Workspace
{
    /// <summary>
    /// ViewModel del contenitore di Workspaces, contiene le operazioni di gestione della collection di Workspace
    /// </summary>
    public class WorkspaceContainer : Workspace
    {
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

    }
}
