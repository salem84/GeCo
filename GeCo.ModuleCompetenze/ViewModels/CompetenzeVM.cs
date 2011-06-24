using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using GeCo.Model;
using GeCo.Infrastructure.Workspace;
using System.Windows.Input;
using GeCo.Infrastructure;
using GeCo.BLL.Services;

namespace GeCo.ModuleCompetenze.ViewModels
{
    public class CompetenzeVM : Workspace
    {
        #region PROPRIETA'

        protected override string containerName { get { return Names.MODULE_NAME; } }
        public override string IdWorkspace { get { return Names.VIEW_COMPETENZE; } }

        public override string DisplayTabName
        {
            get
            {
                return "Gestione Competenze";
            }
        }

        

        private ObservableCollection<Competenza> _competenzeLista;
        public ObservableCollection<Competenza> CompetenzeLista
        {
            get { return _competenzeLista; }
            set
            {
                if (_competenzeLista != value)
                {
                    _competenzeLista = value;
                    RaisePropertyChanged("CompetenzeLista");
                }
            }
        }

        private Competenza _competenzaSelezionata;
        public Competenza CompetenzaSelezionata
        {
            get { return _competenzaSelezionata; }
            set
            {
                if (_competenzaSelezionata != value)
                {
                    _competenzaSelezionata = value;
                    RaisePropertyChanged("CompetenzaSelezionata");
                }
            }
        }

        public List<string> MacroGruppi 
        { 
            get 
            {
                return Tipologiche.Macrogruppi.GetAll();
            }
        }

        public IEnumerable<TipologiaCompetenza> TipologieCompetenze { get; private set; }

        #endregion


        #region COMMANDS
        
        public ICommand AddConoscenzaCommand { get; set; }
        public ICommand SalvaCommand { get; set; }
        public ICommand SalvaTuttoCommand { get; set; }

        #endregion

        #region SERVICES

        ICompetenzeServices _competenzeServices;

        #endregion

        public CompetenzeVM(ICompetenzeServices competenzeServices)
        {
            Stato = "Competenze";

            _competenzeServices = competenzeServices;

            AddConoscenzaCommand = new RelayCommand(AggiungiCompetenza);
            SalvaCommand = new RelayCommand(SalvaCompetenza);
            SalvaTuttoCommand = new RelayCommand(SalvaTutteCompetenze);

            //Carica le competenze
            CompetenzeLista = new ObservableCollection<Competenza>(_competenzeServices.GetCompetenze());
            TipologieCompetenze = _competenzeServices.GetTipologieCompetenze();
            
            /*using (PavimentalContext context = new PavimentalContext())
            {

                CompetenzeLista = new ObservableCollection<Competenza>(context.Competenze.Include(c => c.TipologiaCompetenza));
                TipologieCompetenze = context.TipologieCompetenze.ToList();
            }*/

        }

        private void AggiungiCompetenza()
        {
            var comp = new Competenza() { Titolo = "Nuova" };
            CompetenzeLista.Add(comp);
            CompetenzaSelezionata = comp;
        }

        private void SalvaCompetenza()
        {
            if (CompetenzaSelezionata != null)
            {
                _competenzeServices.SalvaCompetenza(CompetenzaSelezionata);
                /*using (PavimentalContext context = new PavimentalContext())
                {
                    //Sto facendo un update
                    if (CompetenzaSelezionata.Id == 0)
                    {
                        context.Competenze.Add(CompetenzaSelezionata);
                    }
                    else
                    {
                        var competenza = context.Competenze.Find(CompetenzaSelezionata.Id);
                        competenza.Titolo = CompetenzaSelezionata.Titolo;
                        competenza.Descrizione = CompetenzaSelezionata.Descrizione;
                        competenza.TipologiaCompetenzaId = CompetenzaSelezionata.TipologiaCompetenza.Id;
                    }

                    var ret = context.SaveChanges();
                }*/
            }

            
        }
        
        private void SalvaTutteCompetenze()
        {
            foreach (var competenzaMod in CompetenzeLista)
            {
                _competenzeServices.SalvaCompetenza(competenzaMod);
            }
            
            /*using (PavimentalContext context = new PavimentalContext())
            {
                foreach (var competenzaMod in CompetenzeLista)
                {
                    var competenza = context.Competenze.Find(competenzaMod.Id);
                    competenza.Titolo = competenzaMod.Titolo;
                    competenza.Descrizione = competenzaMod.Descrizione;
                    competenza.TipologiaCompetenzaId = competenzaMod.TipologiaCompetenza.Id;
                }

                var ret = context.SaveChanges();
            }*/
        }

    }
}
