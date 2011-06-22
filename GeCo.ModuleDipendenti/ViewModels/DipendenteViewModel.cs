using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using GeCo.BLL.AlgoritmoRicerca;
using System.Windows.Input;
using GeCo.Model;
using GeCo.Infrastructure.Workspace;
using Microsoft.Practices.ServiceLocation;
using GeCo.BLL.Services;
using GeCo.Infrastructure;

namespace GeCo.ModuleDipendenti.ViewModels
{
    public class DipendenteViewModel : Workspace, INotifyPropertyChanged
    {
        #region PROPRIETA'

        protected override string containerName { get { return Names.MODULE_NAME; } }

        /// <summary>
        /// Macrogruppo selezionato nella combobox (Tecniche, Comportamentali, HR)
        /// </summary>
        private string _macroGruppoSelected;
        public string MacroGruppoSelected
        {
            get { return _macroGruppoSelected; }
            set
            {
                _macroGruppoSelected = value;
                RaisePropertyChanged("MacroGruppoSelected");
                RaisePropertyChanged("CompetenzeDisponibiliDaAggiungere");
                //Aggiorno la listview delle competenze del dipendente
                UpdateConoscenzeGroup();
            }
        }

        /// <summary>
        /// Istanza del dipendente (in modifica o nuovo)
        /// </summary>
        private Dipendente _dipendente;
        public Dipendente Dipendente 
        {
            get { return _dipendente; }
            set 
            { 
                _dipendente = value;
                RaisePropertyChanged("Dipendente");
                UpdateConoscenzeGroup();    
            }
        }

        
        /// <summary>
        /// CollectionView delle conoscenze raggruppate per tipologia (e filtrate per macrogruppo)
        /// </summary>
        private ICollectionView _conoscenzePerTipologia;
        public ICollectionView ConoscenzePerTipologia
        {
            get { return _conoscenzePerTipologia; }
            set
            {
                if (_conoscenzePerTipologia == value) return;
                _conoscenzePerTipologia = value;
                RaisePropertyChanged("ConoscenzePerTipologia");
            }
        }

        /// <summary>
        /// Indica se la vista è aperta in modalità Edit o Nuovo (creazione di un nuovo dipendente)
        /// </summary>
        private bool _editMode;
        public bool EditMode
        {
            get { return _editMode; }
            set
            {
                if (_editMode != value)
                {
                    _editMode = value;
                    RaisePropertyChanged("EditMode");
                    //Notifico alla view che è stato cambiata la modalità (così viene ricaricato il nome della scheda)
                    //RaisePropertyChanged("DisplayTabName");
                }
            }
        }


        private ICollection<Competenza> _competenzeTotali;
        protected ICollection<Competenza> CompetenzeTotali
        {
            get
            {
                if (_competenzeTotali == null)
                {
                    //using (PavimentalContext context = new PavimentalContext())
                    //{
                    //    _competenzeTotali = context.Competenze.Include(c => c.TipologiaCompetenza).ToList();
                    //}
                    var service = ServiceLocator.Current.GetInstance<ICompetenzeServices>();
                    _competenzeTotali = service.GetCompetenze();
                }
                return _competenzeTotali;
            }
        }

        public Competenza CompetenzaDisponibileSelezionata { get; set; }

        public IEnumerable<Competenza> CompetenzeDisponibiliDaAggiungere
        {
            get
            {
                if (MacroGruppoSelected != null)
                {
                    IEnumerable<Competenza> tutteMacrogruppo = CompetenzeTotali.Where(c => c.TipologiaCompetenza.MacroGruppo == MacroGruppoSelected);
                    IEnumerable<Competenza> competenzePresenti = Dipendente.Conoscenze.Select(c => c.Competenza).Where(c => c.TipologiaCompetenza.MacroGruppo == MacroGruppoSelected);
                    IEnumerable<Competenza> rimanenti = tutteMacrogruppo.Except(competenzePresenti, c => c.Id);
                    
                    return rimanenti;
                }
                else
                {
                    return null;
                }
            }
        }


        /// <summary>
        /// Lista dei macrogruppi
        /// </summary>
        //TODO questa lista dovrebbe essere letta da DB
        public List<string> MacroGruppi 
        { 
            get 
            {
                return Tipologiche.Macrogruppi.GetAll();
            } 
        }

        #endregion //PROPRIETA'


        #region COMMANDS

        private ICommand _salvaCommand;
        public ICommand SalvaCommand
        {
            get
            {
                if (_salvaCommand == null)
                {
                    _salvaCommand = new RelayCommand(SalvaDipendente,
                        //Abilitato
                    //() => Dipendente != null && !string.IsNullOrEmpty(Dipendente.Nome) && !string.IsNullOrEmpty(Dipendente.Cognome) 
                    () => Dipendente != null && !string.IsNullOrEmpty(Dipendente.Matricola) 
                    );
                }
                return _salvaCommand;
            }
        }

        private ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    //Abilitato se sto in modifica
                    _deleteCommand = new RelayCommand(CancellaDipendente, () => EditMode);
                }
                return _deleteCommand;
            }
        }

        //private ICommand _confrontaCommand;
        //public ICommand ConfrontaCommand
        //{
        //    get
        //    {
        //        if (_confrontaCommand == null)
        //        {
        //            _confrontaCommand = new RelayCommand(AvviaConfronto);
        //        }
        //        return _confrontaCommand;
        //    }
        //}

        //E' legato al button che permette di aggiungere una competenza disponibile alle competenze del dipendente
        //Abilitato quando è selezionata una competenza
        private ICommand _aggiungiCompetenzaCommand;
        public ICommand AggiungiCompetenzaCommand
        {
            get
            {
                if (_aggiungiCompetenzaCommand == null)
                {
                    _aggiungiCompetenzaCommand = new RelayCommand(AggiungiCompetenza,
                        () => CompetenzaDisponibileSelezionata != null);
                }
                return _aggiungiCompetenzaCommand;
            }
        }

        #endregion

        private int _dipendenteId;

        
        /// <summary>
        /// Costruttore senza parametri per la creazione di un nuovo dipendente
        /// </summary>
        public DipendenteViewModel()
        {
            DisplayTabName = "Nuovo";
            StartBackgroundAutoProgress(CreaNuovoDipendente);
            EditMode = false;
        }

        /// <summary>
        /// Costruttore con parametro per la modifica di un dipendente esistente
        /// </summary>
        /// <param name="dipendente"></param>
        public DipendenteViewModel(Dipendente dipendente)
        {
            DisplayTabName = "Modifica";
            _dipendenteId = dipendente.Id;
            StartBackgroundAutoProgress(LoadDipendente);
            EditMode = true;

            //DisplayTabName = "Modifica " + Dipendente.Matricola ?? "";
        }

        //private void LoadDipendente(int dipendenteId)
        private void LoadDipendente()
        {
            //Nella ricerca non carico le proprietà correlate, quindi devo effettuare la query su DB,
            //per ricaricare tutto
            //using (PavimentalContext context = new PavimentalContext())
            //{
            //    Dipendente = context.Dipendenti.Include(a => a.Conoscenze.Select(c => c.Competenza))
            //        .Include(a => a.Conoscenze.Select(c => c.LivelloConoscenza))
            //        .Include(a => a.Conoscenze.Select(c => c.Competenza.TipologiaCompetenza))
            //        .SingleOrDefault(a => a.Id == _dipendenteId);
            //}
            var service = ServiceLocator.Current.GetInstance<IDipendentiServices>();
            Dipendente = service.CaricaDipendente(_dipendenteId);


            //Può essere null magari perchè ho cancellato quell'entità ed è rimasta aperta la scheda
            if (Dipendente == null)
            {
                //TODO
                Stato = "Utente non trovato";
            }
            
        }






        /// <summary>
        /// Metodo per il salvataggio del dipendente, per evitare duplicazioni devo leggermi tutte le competenze e ricrearle
        /// </summary>
        private void SalvaDipendente()
        {
            Stato = "Salvataggio in corso";
                        
            var service = ServiceLocator.Current.GetInstance<IDipendentiServices>();
            var result = service.SalvaDipendente(Dipendente);

            Dipendente = result;

            EditMode = true;
            DisplayTabName = "Modifica " + Dipendente.Matricola;

            Stato = "Salvato";
        }


        public void AvviaConfronto()
        {
            //RisultatiFiguraPerDipendenteViewModel visualizza = new RisultatiFiguraPerDipendenteViewModel(dipendente);
            var confrontoMaster = ServiceLocator.Current.GetInstance<ConfrontoDipendenteMasterVM>();
            confrontoMaster.Dipendente = Dipendente;
            confrontoMaster.AddToShell();
        }

        private void CancellaDipendente()
        {
            Stato = "Cancellazione in corso";
            
            var service = ServiceLocator.Current.GetInstance<IDipendentiServices>();
            service.EliminaDipendente(Dipendente.Id);

            Stato = "Cancellato";
        }


        private void CreaNuovoDipendente()
        {
            Dipendente = new Dipendente();
        }

        /// <summary>
        /// Metodo richiamato quando cambio la selezione del macrogruppo (aggiorno le conoscenze visualizzate)
        /// </summary>
        private void UpdateConoscenzeGroup()
        {
            var conoscenzeList = Dipendente.Conoscenze.Where(c => c.Competenza.TipologiaCompetenza.MacroGruppo == MacroGruppoSelected).OrderBy(c => c.CompetenzaId);
            ConoscenzePerTipologia = CollectionViewSource.GetDefaultView(conoscenzeList);
            ConoscenzePerTipologia.GroupDescriptions.Add(new PropertyGroupDescription()
            {

                PropertyName = "Competenza.TipologiaCompetenza.Titolo"
            });
        }


        /// <summary>
        /// Legge la competenza selezionata nella combobox e l'aggiunge a quelle del dipendente
        /// </summary>
        protected void AggiungiCompetenza()
        {
            if (CompetenzaDisponibileSelezionata != null)
            {
                var service = ServiceLocator.Current.GetInstance<ICompetenzeServices>();
                var livelloNullo = service.GetLivelliConoscenza().Single(lc => lc.Titolo == Tipologiche.Livello.INSUFFICIENTE);


                Dipendente.Conoscenze.Add(new ConoscenzaCompetenza()
                    {
                        Competenza = CompetenzaDisponibileSelezionata,
                        //LivelloConoscenza = livelloNullo
                        CompetenzaId = CompetenzaDisponibileSelezionata.Id,
                        LivelloConoscenzaId = livelloNullo.Id
                    });

                RaisePropertyChanged("CompetenzeDisponibiliDaAggiungere");
                UpdateConoscenzeGroup();
            }
        }

        
    }

}
