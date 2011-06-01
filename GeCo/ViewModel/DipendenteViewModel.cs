using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using GeCo;
using GeCo.Controls;
using GeCo.DAL;
using GeCo.Utility;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using GeCo.BLL.AlgoritmoRicerca;
using System.Windows.Input;

namespace GeCo.ViewModel
{
    public class DipendenteViewModel : WorkspaceViewModel, INotifyPropertyChanged
    {
        #region PROPRIETA'

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
                    using (PavimentalDb context = new PavimentalDb())
                    {
                        _competenzeTotali = context.Competenze.Include(c => c.TipologiaCompetenza).ToList();
                    }
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
                return new List<string>()  
                { 
                    Tipologiche.MG_HR, 
                    Tipologiche.MG_COMPORTAMENTALE, 
                    Tipologiche.MG_TECNICO
                } ;
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
                    _salvaCommand = new RelayCommand(() =>
                    {
                        Stato = "Salvataggio in corso";
                        SalvaDipendente();
                        Stato = "Salvato";
                    },
                        //Abilitato
                    () => Dipendente != null && !string.IsNullOrEmpty(Dipendente.Nome) && !string.IsNullOrEmpty(Dipendente.Cognome) 
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
                    _deleteCommand = new RelayCommand(() =>
                    {
                        Stato = "Cancellazione in corso";
                        using (PavimentalDb context = new PavimentalDb())
                            CancellaDipendente(context, Dipendente);
                        Stato = "Cancellato";
                    },
                        //Abilitato se sto in modifica
                    () => EditMode
                    );
                }
                return _deleteCommand;
            }
        }

        
        public ICommand ConfrontaCommand { get; set; }

        //E' legato al button che permette di aggiungere una competenza disponibile alle competenze del dipendente
        private ICommand _aggiungiCompetenzaCommand;
        public ICommand AggiungiCompetenzaCommand
        {
            get
            {
                if (_aggiungiCompetenzaCommand == null)
                {
                    _aggiungiCompetenzaCommand = new RelayCommand(() =>
                    {
                        AggiungiCompetenza();
                    }
                    );
                }
                return _aggiungiCompetenzaCommand;
            }
        }

        #endregion

        private int _dipendenteId;

        //Inizializzo il comando quando gli configuro l'Action
        public Action<Dipendente> RicercaFiguraPerDipendente
        { 
            set { ConfrontaCommand = new RelayCommand(() => value(Dipendente)); }
        }

        /// <summary>
        /// Costruttore senza parametri per la creazione di un nuovo dipendente
        /// </summary>
        public DipendenteViewModel() : this(null) { }

        /// <summary>
        /// Costruttore con parametro per la modifica di un dipendente esistente
        /// </summary>
        /// <param name="dipendente"></param>
        public DipendenteViewModel(Dipendente dipendente)
        {
            if (dipendente == null)
            {
                DisplayTabName = "Nuovo";
                StartBackgroundAutoProgress(CreaNuovoDipendente);
                EditMode = false;
            }
            else
            {
                DisplayTabName = "Modifica";
                _dipendenteId = dipendente.Id;
                StartBackgroundAutoProgress(LoadDipendente);
                EditMode = true;
        
            }

        }

        //private void LoadDipendente(int dipendenteId)
        private void LoadDipendente()
        {
            //Nella ricerca non carico le proprietà correlate, quindi devo effettuare la query su DB,
            //per ricaricare tutto
            using (PavimentalDb context = new PavimentalDb())
            {
                Dipendente = context.Dipendenti.Include(a => a.Conoscenze.Select(c => c.Competenza))
                    .Include(a => a.Conoscenze.Select(c => c.LivelloConoscenza))
                    .Include(a => a.Conoscenze.Select(c => c.Competenza.TipologiaCompetenza))
                    .SingleOrDefault(a => a.Id == _dipendenteId);
            }

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
            //Ricreo l'oggetto
            Dipendente dip = new Dipendente();
            dip.Cognome = Dipendente.Cognome;
            dip.Nome = Dipendente.Nome;
            dip.DataNascita = Dipendente.DataNascita;


            dip.Conoscenze = new List<ConoscenzaCompetenza>();

            foreach (var c in Dipendente.Conoscenze)
            {
                ConoscenzaCompetenza conoscenza = new ConoscenzaCompetenza();

                conoscenza.LivelloConoscenzaId = c.LivelloConoscenzaId;
                conoscenza.CompetenzaId = c.CompetenzaId;
                dip.Conoscenze.Add(conoscenza);
            }

            using (PavimentalDb context = new PavimentalDb())
            {
                if (EditMode)
                {
                    CancellaDipendente(context, Dipendente);    
                }
                    
                    
                //context.Entry(dip).State = System.Data.EntityState.Modified;
                context.Dipendenti.Add(dip);
                context.SaveChanges();
                
            }

            _dipendenteId = dip.Id;
            LoadDipendente();
            EditMode = true;
        }

        private void CancellaDipendente(PavimentalDb context, Dipendente dipendente)
        {
            
                /*foreach (var c in dipendente.Conoscenze)
                {
                    ConoscenzaCompetenza con = new ConoscenzaCompetenza() { Id = c.Id };
                    context.ConoscenzaCompetenze.Attach(con);
                    context.ConoscenzaCompetenze.Remove(con);
                }
                context.SaveChanges();

                Dipendente dipToRemove = new Dipendente() { Id = dipendente.Id };
                context.Dipendenti.Attach(dipToRemove);
                context.Dipendenti.Remove(dipToRemove);

                context.SaveChanges();*/


            Dipendente dipToRemove = new Dipendente() { Id = dipendente.Id };
            context.Dipendenti.Attach(dipToRemove);
            context.Dipendenti.Remove(dipToRemove);

            context.SaveChanges();
        }


        private void CreaNuovoDipendente()
        {
            using (PavimentalDb context = new PavimentalDb())
            {
                var livelloNullo = context.LivelliConoscenza.Single(lc => lc.Titolo == Tipologiche.Livello.INSUFFICIENTE);
                var allCompetenze = context.Competenze.Include(c => c.TipologiaCompetenza).ToList();

                var knowHowVuoto = (from c in allCompetenze
                                    select new ConoscenzaCompetenza()
                                    {
                                        Competenza = c,
                                        //LivelloConoscenza = livelloNullo
                                        CompetenzaId = c.Id,
                                        LivelloConoscenzaId = livelloNullo.Id
                                    }).ToList();

                Dipendente = new Dipendente() { Conoscenze = knowHowVuoto };

                
            }
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
            using (PavimentalDb context = new PavimentalDb())
            {
                var livelloNullo = context.LivelliConoscenza.Single(lc => lc.Titolo == Tipologiche.Livello.INSUFFICIENTE);

                Dipendente.Conoscenze.Add(new ConoscenzaCompetenza()
                    {
                        Competenza = CompetenzaDisponibileSelezionata,
                        //LivelloConoscenza = livelloNullo
                        CompetenzaId = CompetenzaDisponibileSelezionata.Id,
                        LivelloConoscenzaId = livelloNullo.Id
                    });

                RaisePropertyChanged("CompetenzeDisponibiliDaAggiungere");
            }
        }

    }

}
