using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.DAL;
using System.Data.Entity;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;

namespace GeCo.ViewModel
{
    public class CompetenzeViewModel : WorkspaceViewModel
    {
        public override string DisplayTabName
        {
            get
            {
                return "Gestione Competenze";
            }
        }

        //private string _stato;
        //public override string Stato
        //{
        //    get { return _stato; }
        //    set
        //    {
        //        if (_stato != value)
        //        {
        //            _stato = value;
        //            RaisePropertyChanged("Stato");
        //        }
        //    }
        //}

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

        public string[] MacroGruppi 
        { 
            get 
            {
                return new string[] { "TECN", "HR", "PSICO" };
            }
        }

        public IEnumerable<TipologiaCompetenza> TipologieCompetenze { get; private set; }
        public RelayCommand AddConoscenzaCommand { get; set; }
        public RelayCommand SalvaCommand { get; set; }
        public RelayCommand SalvaTuttoCommand { get; set; }


        public CompetenzeViewModel()
        {
            Stato = "kkk";
            AddConoscenzaCommand = new RelayCommand(
                () => AggiungiCompetenza());

            SalvaCommand = new RelayCommand(
                () => SalvaCompetenza());

            SalvaTuttoCommand = new RelayCommand(
                () => SalvaTutteCompetenze());

            //Carica le competenze
            using (PavimentalContext context = new PavimentalContext())
            {

                CompetenzeLista = new ObservableCollection<Competenza>(context.Competenze.Include(c => c.TipologiaCompetenza));
                TipologieCompetenze = context.TipologieCompetenze.ToList();
            }

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
                using (PavimentalContext context = new PavimentalContext())
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
                }
            }

            Stato = "poppp";
        }
        
        private void SalvaTutteCompetenze()
        {
            using (PavimentalContext context = new PavimentalContext())
            {
                foreach (var competenzaMod in CompetenzeLista)
                {
                    var competenza = context.Competenze.Find(competenzaMod.Id);
                    competenza.Titolo = competenzaMod.Titolo;
                    competenza.Descrizione = competenzaMod.Descrizione;
                    competenza.TipologiaCompetenzaId = competenzaMod.TipologiaCompetenza.Id;
                }

                var ret = context.SaveChanges();
            }
        }

    }
}
