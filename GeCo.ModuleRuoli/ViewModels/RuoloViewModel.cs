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
using GeCo.Infrastructure;
using Microsoft.Practices.ServiceLocation;
using GeCo.BLL.Services;
using GeCo.Utility;

namespace GeCo.ModuleRuoli.ViewModels
{
    public class RuoloViewModel : Workspace, INotifyPropertyChanged
    {
        #region PROPRIETA'

        

        private string _macroGruppoSelected;
        public string MacroGruppoSelected
        {
            get { return _macroGruppoSelected; }
            set
            {
                _macroGruppoSelected = value;
                RaisePropertyChanged("MacroGruppoSelected");
                RaisePropertyChanged("CompetenzeDisponibiliDaAggiungere");
                UpdateConoscenzeGroup();
            }
        }

        private FiguraProfessionale _figuraProfessionale;
        public FiguraProfessionale FiguraProfessionale 
        {
            get { return _figuraProfessionale; }
            set 
            {
                _figuraProfessionale = value;
                RaisePropertyChanged("FiguraProfessionale");
                UpdateConoscenzeGroup();    
            }
        }

        

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
                    RaisePropertyChanged("DisplayTabName");
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
                    var service = ServiceLocator.Current.GetInstance<IDipendentiServices>();
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
                    IEnumerable<Competenza> competenzePresenti = FiguraProfessionale.Conoscenze.Select(c => c.Competenza).Where(c => c.TipologiaCompetenza.MacroGruppo == MacroGruppoSelected);
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
                };
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
                        SalvaFigura();
                        Stato = "Salvato";
                    },
                        //Abilitato
                    () => FiguraProfessionale != null && !string.IsNullOrEmpty(FiguraProfessionale.Titolo)
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
                        //using (PavimentalContext context = new PavimentalContext())
                            CancellaFiguraProfessionale(FiguraProfessionale);
                        Stato = "Cancellato";
                    },
                        //Abilitato
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

        private int _figuraProfessionaleId;

        //Non posso inizializzarlo nel costruttore
        public Action<FiguraProfessionale> RicercaDipendentePerFigura
        {
            set { ConfrontaCommand = new RelayCommand(() => value(FiguraProfessionale)); }
        }

        public RuoloViewModel()
        {
            DisplayTabName = "Nuovo";
            StartBackgroundAutoProgress(CreaNuovaFiguraProfessionale);
            EditMode = false;
        }

        public RuoloViewModel(FiguraProfessionale figuraProf)
        {
            DisplayTabName = "Modifica";
            _figuraProfessionaleId = figuraProf.Id;
            StartBackgroundAutoProgress(LoadFigura);
            EditMode = true;
        }

        //private void LoadDipendente(int dipendenteId)
        private void LoadFigura()
        {
            //Nella ricerca non carico le proprietà correlate, quindi devo effettuare la query su DB,
            //per ricaricare tutto
            //using (PavimentalContext context = new PavimentalContext())
            //{
            //    FiguraProfessionale = context.FigureProfessionali.Include(f => f.Conoscenze.Select(c => c.Competenza))
            //        .Include(f => f.Conoscenze.Select(c => c.LivelloConoscenza))
            //        .Include(f => f.Conoscenze.Select(c => c.Competenza.TipologiaCompetenza))
            //        .SingleOrDefault(f => f.Id == _figuraProfessionaleId);
            //}

            var service = ServiceLocator.Current.GetInstance<IRuoliServices>();
            FiguraProfessionale = service.CaricaRuolo(_figuraProfessionaleId);

            //Può essere null magari perchè ho cancellato quell'entità ed è rimasta aperta la scheda
            if (FiguraProfessionale == null)
            {
                //TODO
                Stato = "Figura professionale non trovata";
            }
            

        }







        private void SalvaFigura()
        {
            //Ricreo l'oggetto
            //FiguraProfessionale fig = new FiguraProfessionale();
            //fig.Titolo = FiguraProfessionale.Titolo;
            //fig.Descrizione = FiguraProfessionale.Descrizione;


            //fig.Conoscenze = new List<ConoscenzaCompetenza>();

            //foreach (var c in FiguraProfessionale.Conoscenze)
            //{
            //    ConoscenzaCompetenza conoscenza = new ConoscenzaCompetenza();

            //    conoscenza.LivelloConoscenzaId = c.LivelloConoscenzaId;
            //    conoscenza.CompetenzaId = c.CompetenzaId;
            //    fig.Conoscenze.Add(conoscenza);
            //}

            //using (PavimentalContext context = new PavimentalContext())
            //{
            //    if (EditMode)
            //    {
            //        CancellaFiguraProfessionale(context, FiguraProfessionale);    
            //    }
                    
                    
            //    //context.Entry(dip).State = System.Data.EntityState.Modified;
            //    context.FigureProfessionali.Add(fig);
            //    context.SaveChanges();
                
            //}

            //_figuraProfessionaleId = fig.Id;
            //LoadFigura();

            var service = ServiceLocator.Current.GetInstance<IRuoliServices>();
            var result = service.SalvaRuolo(FiguraProfessionale);

            FiguraProfessionale = result;

            EditMode = true;
        }

        private void CancellaFiguraProfessionale(FiguraProfessionale figura)
        {
            /*
            
                foreach (var c in figura.Conoscenze)
                {
                    ConoscenzaCompetenza con = new ConoscenzaCompetenza() { Id = c.Id };
                    context.ConoscenzaCompetenze.Attach(con);
                    context.ConoscenzaCompetenze.Remove(con);
                }
                context.SaveChanges();

                FiguraProfessionale figToRemove = new FiguraProfessionale() { Id = figura.Id };
                context.FigureProfessionali.Attach(figToRemove);
                context.FigureProfessionali.Remove(figToRemove);

                context.SaveChanges();

                figura = null;
            */

            //FiguraProfessionale figToRemove = new FiguraProfessionale() { Id = figura.Id };
            //context.FigureProfessionali.Attach(figToRemove);
            //context.FigureProfessionali.Remove(figToRemove);

            //context.SaveChanges();

            var service = ServiceLocator.Current.GetInstance<IRuoliServices>();
            service.EliminaRuolo(figura.Id);

        }


        private void CreaNuovaFiguraProfessionale()
        {
            /*using (PavimentalContext context = new PavimentalContext())
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

                FiguraProfessionale = new FiguraProfessionale() { Conoscenze = knowHowVuoto };

                
            }*/

            FiguraProfessionale = new FiguraProfessionale();
        }


        /// <summary>
        /// Metodo richiamato quando cambio la selezione del macrogruppo (aggiorno le conoscenze visualizzate)
        /// </summary>
        private void UpdateConoscenzeGroup()
        {
            var conoscenzeList = FiguraProfessionale.Conoscenze.Where(c => c.Competenza.TipologiaCompetenza.MacroGruppo == MacroGruppoSelected).OrderBy(c => c.CompetenzaId);
            ConoscenzePerTipologia = CollectionViewSource.GetDefaultView(conoscenzeList);
            ConoscenzePerTipologia.GroupDescriptions.Add(new PropertyGroupDescription()
            {

                PropertyName = "Competenza.TipologiaCompetenza.Titolo"
            });
        }


        /// <summary>
        /// Legge la competenza selezionata nella combobox e l'aggiunge a quelle del ruolo
        /// </summary>
        protected void AggiungiCompetenza()
        {
            //using (PavimentalContext context = new PavimentalContext())
            //{

            var service = ServiceLocator.Current.GetInstance<IDipendentiServices>();
            var livelloNullo = service.GetLivelliConoscenza().Single(lc => lc.Titolo == Tipologiche.Livello.INSUFFICIENTE);
            //var livelloNullo = context.LivelliConoscenza.Single(lc => lc.Titolo == Tipologiche.Livello.INSUFFICIENTE);

            FiguraProfessionale.Conoscenze.Add(new ConoscenzaCompetenza()
            {
                Competenza = CompetenzaDisponibileSelezionata,
                //LivelloConoscenza = livelloNullo
                CompetenzaId = CompetenzaDisponibileSelezionata.Id,
                LivelloConoscenzaId = livelloNullo.Id
            });

            RaisePropertyChanged("CompetenzeDisponibiliDaAggiungere");
            UpdateConoscenzeGroup();
            //}
        }
    }


}
