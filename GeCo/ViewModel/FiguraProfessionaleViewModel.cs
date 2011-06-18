using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using GeCo;
using GeCo.Controls;
using GeCo.Varie;
using GeCo.DAL;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using GeCo.BLL.AlgoritmoRicerca;
using System.Windows.Input;
using GeCo.Model;

namespace GeCo.ViewModel
{
    public class FiguraProfessionaleViewModel : WorkspaceViewModel, INotifyPropertyChanged
    {
        #region PROPRIETA'

        public override string DisplayTabName
        {
            /*get
            {
                if (EditMode)
                    return "Modifica " + FiguraProfessionale.Titolo;
                else
                    return "Nuovo";
            }*/
            get;
            set;
        }

        private string _selectedMacroGruppo;
        public string SelectedMacroGruppo
        {
            get { return _selectedMacroGruppo; }
            set
            {
                _selectedMacroGruppo = value;
                RaisePropertyChanged("SelectedMacroGruppo");
                UpdateConoscenzeGroup();
            }
        }

        private Ruolo _figuraProfessionale;
        public Ruolo FiguraProfessionale 
        {
            get { return _figuraProfessionale; }
            set 
            {
                _figuraProfessionale = value;
                RaisePropertyChanged("FiguraProfessionale");
                UpdateConoscenzeGroup();    
            }
        }

        private void UpdateConoscenzeGroup()
        {
            var conoscenzeList = FiguraProfessionale.Conoscenze.Where(c => c.Competenza.TipologiaCompetenza.MacroGruppo == SelectedMacroGruppo).OrderBy(c => c.CompetenzaId);
            ConoscenzePerTipologia = CollectionViewSource.GetDefaultView(conoscenzeList);
            ConoscenzePerTipologia.GroupDescriptions.Add(new PropertyGroupDescription()
            {

                PropertyName = "Competenza.TipologiaCompetenza.Titolo"
            });
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

        public List<string> MacroGruppi { get { return new List<string>() { Tipologiche.MG_HR, Tipologiche.MG_COMPORTAMENTALE, Tipologiche.MG_TECNICO} ; } }

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
                        using (PavimentalContext context = new PavimentalContext())
                            CancellaFiguraProfessionale(context, FiguraProfessionale);
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

        #endregion

        private int _figuraProfessionaleId;

        //Non posso inizializzarlo nel costruttore
        public Action<Ruolo> RicercaDipendentePerFigura
        {
            set { ConfrontaCommand = new RelayCommand(() => value(FiguraProfessionale)); }
        }

        public FiguraProfessionaleViewModel()
        {
            DisplayTabName = "Nuovo";
            StartBackgroundAutoProgress(CreaNuovaFiguraProfessionale);
            EditMode = false;
        }

        public FiguraProfessionaleViewModel(Ruolo figuraProf)
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
            using (PavimentalContext context = new PavimentalContext())
            {
                FiguraProfessionale = context.FigureProfessionali.Include(f => f.Conoscenze.Select(c => c.Competenza))
                    .Include(f => f.Conoscenze.Select(c => c.LivelloConoscenza))
                    .Include(f => f.Conoscenze.Select(c => c.Competenza.TipologiaCompetenza))
                    .SingleOrDefault(f => f.Id == _figuraProfessionaleId);
            }

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
            Ruolo fig = new Ruolo();
            fig.Nome = FiguraProfessionale.Nome;
            fig.Descrizione = FiguraProfessionale.Descrizione;


            fig.Conoscenze = new List<ConoscenzaCompetenza>();

            foreach (var c in FiguraProfessionale.Conoscenze)
            {
                ConoscenzaCompetenza conoscenza = new ConoscenzaCompetenza();

                conoscenza.LivelloConoscenzaId = c.LivelloConoscenzaId;
                conoscenza.CompetenzaId = c.CompetenzaId;
                fig.Conoscenze.Add(conoscenza);
            }

            using (PavimentalContext context = new PavimentalContext())
            {
                if (EditMode)
                {
                    CancellaFiguraProfessionale(context, FiguraProfessionale);    
                }
                    
                    
                //context.Entry(dip).State = System.Data.EntityState.Modified;
                context.FigureProfessionali.Add(fig);
                context.SaveChanges();
                
            }

            _figuraProfessionaleId = fig.Id;
            LoadFigura();
            EditMode = true;
        }

        private void CancellaFiguraProfessionale(PavimentalContext context, Ruolo figura)
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

            Ruolo figToRemove = new Ruolo() { Id = figura.Id };
            context.FigureProfessionali.Attach(figToRemove);
            context.FigureProfessionali.Remove(figToRemove);

            context.SaveChanges();
        }


        private void CreaNuovaFiguraProfessionale()
        {
            using (PavimentalContext context = new PavimentalContext())
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

                FiguraProfessionale = new Ruolo() { Conoscenze = knowHowVuoto };

                
            }
        }

    }


}
