using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Data;
using GalaSoft.MvvmLight.Command;
using GeCo;
using GeCo.DAL;
using System.Configuration;
using System.Windows.Input;
using GeCo.View;

namespace GeCo.ViewModel
{
    public class MainWindowViewModel : WorkspaceViewModel
    {
        public ICommand ResettaDBCommand { get; private set; }
        public ICommand NuovoDipendenteCommand { get; private set; }
        public ICommand CercaDipendenteCommand { get; private set; }
        public ICommand NuovaFiguraProfCommand { get; private set; }
        public ICommand CercaFiguraProfCommand { get; private set; }
        public ICommand CompetenzeViewCommand { get; private set; }
        public ICommand AboutCommand { get; private set; }
        public ICommand QuitCommand { get; private set; }

        //Nascosti
        public ICommand RicercaSostitutoPerFiguraCommand { get; private set; }
        public ICommand RicercaSostitutoPerAnagraficaCommand { get; private set; }

        #region Constructor

        public MainWindowViewModel()
        {
            InizializzaCommands();

            //Devo verificare che il DB esiste, in caso contrario lo dove creare
            string nomeDatabase = ConfigurationManager.ConnectionStrings["PavimentalContext"].ToString();
            //Sostituisco il path DataDirectory
            //nomeDatabase = nomeDatabase.Replace("|DataDirectory|", AppDomain.CurrentDomain.GetData("DataDirectory").ToString());
            nomeDatabase = nomeDatabase.Substring(nomeDatabase.IndexOf('='));
            Stato = string.Format("Caricamento Database: {0}", nomeDatabase);
            StartBackgroundAutoProgress(VerificaDB);
        }

        private void InizializzaCommands()
        {
            //******** MENU
            AboutCommand = new RelayCommand(
                () =>
                {
                    AboutWindow about = new AboutWindow();
                    about.ShowDialog();
                });

            QuitCommand = this.CloseCommand;


            //Pannello amministrazione, crea DB
            CompetenzeViewCommand = new RelayCommand(
                () => AggiungiPannello(new CompetenzeViewModel()));

            ResettaDBCommand = new RelayCommand(
                () => InizializzaDB());



            //********* GRUPPO DIPENDENTE
            //Finestra editor Dipendente
            NuovoDipendenteCommand = new RelayCommand(
                () => AggiungiPannello(new DipendenteViewModel()
                {
                    RicercaFiguraPerDipendente = AggiungiSchedaRisultatiRicercaFiguraPerDipendente
                }));

            CercaDipendenteCommand = new RelayCommand(
                () => AggiungiPannello(new RicercaAnagraficaViewModel()
                {
                    VisualizzaDipendenteAction = AggiungiSchedaRisultatiRicercaAnagrafica
                }));

            //********* GRUPPO FIGURE PROFESSIONALI
            NuovaFiguraProfCommand = new RelayCommand(
                () => AggiungiPannello(new FiguraProfessionaleViewModel()
                    {
                        RicercaDipendentePerFigura = AggiungiSchedaRisultatiRicercaDipendentePerFigura
                    }));

            CercaFiguraProfCommand = new RelayCommand(
                () => AggiungiPannello(new RicercaFiguraProfViewModel()
                    {
                        VisualizzaFiguraProfAction = AggiungiSchedaRisultatiRicercaFiguraProf
                    }));


            //********* GRUPPO CERCA SOSTITUTO (vanno nascosti)
            //Per figura professionale
            RicercaSostitutoPerFiguraCommand = new RelayCommand(
                () => AggiungiPannello(new RisultatiDipendentePerFiguraViewModel(null)));

            //Per Anagrafica
            //TODO fake
            RicercaSostitutoPerAnagraficaCommand = new RelayCommand(
                () => AggiungiPannello(new RisultatiFiguraPerDipendenteViewModel(null)));


        }

        private void VerificaDB()
        {
            //string dataDirectory = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
            using (PavimentalContext db = new PavimentalContext())
            {
                
                //Se esiste verifico che sia compatibile
                if (db.Database.Exists())
                {
                    bool compatibile = db.Database.CompatibleWithModel(false);
                    if (!compatibile)
                    {
                        db.Database.Delete();
                        db.Database.Create();
                        InitializeDB.InitalizeAll();
                    }
                }
                else
                {
                    db.Database.Create();
                    InitializeDB.InitalizeAll();
                }

                //Faccio questa query per ottimizzare le successive
                var temp = db.ConoscenzaCompetenze.ToList();
            }

        }

        #endregion //Constructor

        #region Workspaces

        /// <summary>
        /// Returns the collection of available workspaces to display.
        /// A 'workspace' is a ViewModel that can request to be closed.
        /// </summary>
        ObservableCollection<WorkspaceViewModel> _workspaces;

        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_workspaces == null)
                {
                    _workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _workspaces.CollectionChanged += this.OnWorkspacesChanged;
                }
                return _workspaces;
            }
        }

        public WorkspaceViewModel ActiveWorkspace
        {
            get { return GetActiveWorkspace(); }
        }

        void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.OnWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.OnWorkspaceRequestClose;
        }

        void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            this.Workspaces.Remove(sender as WorkspaceViewModel);
        }

        WorkspaceViewModel GetActiveWorkspace()
        {
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
            {
                if (collectionView.CurrentItem != null)
                    return collectionView.CurrentItem as WorkspaceViewModel;
                else //non ho aperto ancora nessun tab, restituisco la main view
                {
                    return this;
                }
            }

            return null;
        }

        #endregion // Workspaces

        #region Private Helpers

        void AggiungiPannello(WorkspaceViewModel vm)
        {
            Workspaces.Add(vm);
            SetActiveWorkspace(vm);
        }      

        void InizializzaDB()
        {
            using (PavimentalContext db = new PavimentalContext())
            {
                db.Database.Delete();
                db.Database.Create();
            }

            InitializeDB.InitalizeAll();
        }

        //Azione eseguita quando faccio doppioclick su un elemento della listview "VisualizzaDipendenteView"
        void AggiungiSchedaRisultatiRicercaAnagrafica(Dipendente dipendente)
        {
            DipendenteViewModel visualizza = new DipendenteViewModel(dipendente)
            {
                //Dipendente = dipendente,
                RicercaFiguraPerDipendente = AggiungiSchedaRisultatiRicercaFiguraPerDipendente
            };

            Workspaces.Add(visualizza);
            SetActiveWorkspace(visualizza);
        }

        //Azione eseguita quando faccio doppioclick su un elemento della listview "VisualizzaDipendenteView"
        void AggiungiSchedaRisultatiRicercaFiguraProf(FiguraProfessionale figuraProf)
        {
            FiguraProfessionaleViewModel visualizza = new FiguraProfessionaleViewModel(figuraProf)
            {
                //Dipendente = dipendente,
                RicercaDipendentePerFigura = AggiungiSchedaRisultatiRicercaDipendentePerFigura
            };

            Workspaces.Add(visualizza);
            SetActiveWorkspace(visualizza);
        }

        void AggiungiSchedaRisultatiRicercaFiguraPerDipendente(Dipendente dipendente)
        {
            RisultatiFiguraPerDipendenteViewModel visualizza = new RisultatiFiguraPerDipendenteViewModel(dipendente);
            Workspaces.Add(visualizza);
            SetActiveWorkspace(visualizza);
        }

        void AggiungiSchedaRisultatiRicercaDipendentePerFigura(FiguraProfessionale figura)
        {
            RisultatiDipendentePerFiguraViewModel visualizza = new RisultatiDipendentePerFiguraViewModel(figura);
            Workspaces.Add(visualizza);
            SetActiveWorkspace(visualizza);
        }

        void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);

            RaisePropertyChanged("ActiveWorkspace");
//            RaisePropertyChanged("Stato");
        }

        #endregion // Private Helpers
    }
}
