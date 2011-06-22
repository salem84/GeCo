using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.BLL.AlgoritmoRicerca;
using GeCo.Model;
using GeCo.Infrastructure.Workspace;
using GeCo.BLL.Services;
using GeCo.Infrastructure;
using GeCo.BLL;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;

namespace GeCo.ModuleRuoli.ViewModels
{
    public class ConfrontoRuoloMasterVM : Workspace
    {
        protected override string containerName { get { return Names.MODULE_NAME; } }

        private IEnumerable<RisultatoRicerca> _risultati;
        public IEnumerable<RisultatoRicerca> Risultati
        {
            get { return _risultati; }
            set
            {
                if (_risultati != value)
                {
                    _risultati = value;
                    RaisePropertyChanged("Risultati");
                }
            }
        }

        public Ruolo Ruolo { get; set; }

        public ParametriConfronto ParametriConfronto { get; set; }

        //Variabili utilizzate per la progress e lo status (totali e completate)
        private int DipendentiTotali { get; set; }
        private int DipendentiAnalizzati { get; set; }

        public ICommand DoubleClickCommand { get; set; }

        public RisultatoRicerca RisultatoSelezionato { get; set; }

        #region GRAFICO

        private bool _graficoVisibile;
        public bool GraficoVisibile
        {
            get { return _graficoVisibile; }
            set
            {
                if (_graficoVisibile != value)
                {
                    _graficoVisibile = value;
                    RaisePropertyChanged("GraficoVisibile");
                }
            }
        }

        //Se visualizzo il grafico, disabilito tutto il resto
        private bool _controlliAbilitati;
        public bool ControlliAbilitati
        {
            get { return _controlliAbilitati; }
            set
            {
                if (_controlliAbilitati != value)
                {
                    _controlliAbilitati = value;
                    RaisePropertyChanged("ControlliAbilitati");
                }
            }
        }

        private List<decimal> _valoriGrafico;
        public List<decimal> ValoriGrafico
        {
            get { return _valoriGrafico; }
            set
            {
                if (_valoriGrafico != value)
                {
                    _valoriGrafico = value;
                    RaisePropertyChanged("ValoriGrafico");
                }
            }
        }

        private List<string> _labelsGrafico;
        public List<string> LabelsGrafico
        {
            get { return _labelsGrafico; }
            set
            {
                if (_labelsGrafico != value)
                {
                    _labelsGrafico = value;
                    RaisePropertyChanged("LabelsGrafico");
                }
            }
        }



        #endregion

        private ICompetenzeServices _livelliConoscenzaServices;
        private IRicercaServices _ricercaServices;
        private IDipendentiServices _dipendentiServices;

        public ConfrontoRuoloMasterVM(ICompetenzeServices livelliConoscenzaServices, IRicercaServices ricercaServices, IDipendentiServices dipendentiServices)
        {
            DisplayTabName = "Sostituti";

            _livelliConoscenzaServices = livelliConoscenzaServices;
            _ricercaServices = ricercaServices;
            _dipendentiServices = dipendentiServices;

            DoubleClickCommand = new RelayCommand(VisualizzaConfrontoDetails);
            ParametriConfronto = new ParametriConfronto();

            //Non dovrebbe capitare mai
            /*if (figura.Conoscenze.Count == 0)
            {
                using (PavimentalContext context = new PavimentalContext())
                {
                    //TODO ricontrollare
                    context.FigureProfessionali.Include(f => f.Conoscenze.Select(c => c.Competenza))
                        .Include(f => f.Conoscenze.Select(c => c.LivelloConoscenza))
                        .Include(f => f.Conoscenze.Select(c => c.Competenza.TipologiaCompetenza));
                    Figura = context.FigureProfessionali.Find(figura.Id);
                }

            }
            else
            {
                //dipendente deve avere tutte le conoscenze caricate
                Figura = figura;
            }*/

            //Non faccio l'autoprogress, setto il valore massimo pari al numero di elementi sul db da analizzare
            /*using (PavimentalContext context = new PavimentalContext())
            {
                DipendentiTotali = context.Dipendenti.Count();
            }

            //Faccio partire l'algoritmo in background
            StartBackground(AvviaAlgoritmo);*/
        }


        //private void LoadParametri()
        //{
        //    //Carico i parametri da visualizzare
        //    ParametriConfronto = new ParametriConfronto();
        //}


        private void AvviaAnalisi()
        {
            DisplayTabName = "Sostituti per " + Ruolo.Titolo;

            //TODO rivedere
            //Non ho salvato il dipendente e mi mancano però le conoscenze per calcolare gli indici
            if (Ruolo.Id == 0)
            {
                var livelliConoscenza = _livelliConoscenzaServices.GetLivelliConoscenza();

                foreach (var c in Ruolo.Conoscenze)
                {
                    c.LivelloConoscenza = livelliConoscenza.Single(lc => lc.Id == c.LivelloConoscenzaId);
                }
            }

            //Non faccio l'autoprogress, setto il valore massimo pari al numero di elementi sul db da analizzare
            /*using (PavimentalContext context = new PavimentalContext())
            {
                FigureProfessionaliTotali = context.FigureProfessionali.Count();
            }*/

            //Faccio partire l'algoritmo in background
            StartBackgroundAutoProgress(AvviaAlgoritmoBackground);
        }

        protected void AvviaAlgoritmoBackground()
        {
            /*RicercaAnagraficaDaFigura algoritmo = new RicercaAnagraficaDaFigura();
            var tempRes = algoritmo.Cerca(Figura);

            //Rielaboro i dati (ordino e nascondo le percentuali)
            Risultati = tempRes.OrderByDescending(r => r.Idoneo).ThenBy(r => r.PercentualeTotale);*/

            //Gli passo la funzione per fare l'aggiornamento del progressivo
            var tempRes = _ricercaServices.CercaDipendenteDaRuolo(Ruolo);

            //Rielaboro i dati (ordino e nascondo le percentuali)
            Risultati = tempRes.OrderByDescending(r => r.Idoneo).ThenByDescending(r => r.PunteggioTotale);
        }

        protected void AggiornaProgress()
        {
            /*DipendentiAnalizzati++;
            ProgressPercent = DipendentiAnalizzati / DipendentiTotali * 100;
            Stato = string.Format("Completato {0} di {1}", DipendentiAnalizzati, DipendentiTotali);*/

        }


        public void VisualizzaConfrontoDetails()
        {
            ConfrontoRuoloDetailsVM confrontoDetailsVM = ServiceLocator.Current.GetInstance<ConfrontoRuoloDetailsVM>();

            Dipendente dipendenteSelezionato = _dipendentiServices.CaricaDipendente(RisultatoSelezionato.Id);
            confrontoDetailsVM.Atteso = dipendenteSelezionato;
            confrontoDetailsVM.Osservato = Ruolo;
            confrontoDetailsVM.AddToShell();
        }

        public void ToggleGrafico()
        {
            if (GraficoVisibile == false)
            {
                if (RisultatoSelezionato != null)
                {
                    LabelsGrafico = new List<string>(new string[] { "HrDiscrezionali", "HrComportamentali", "Comportamentali", "TecnicStrategic", "TecnicCompetitiveAdvantage" });

                    var valori = new List<decimal>();
                    valori.Add(Convert.ToDecimal(RisultatoSelezionato.PunteggioHrDiscrezionali));
                    valori.Add(Convert.ToDecimal(RisultatoSelezionato.PunteggioHrComportamentali));
                    valori.Add(Convert.ToDecimal(RisultatoSelezionato.PunteggioComportamentali));
                    valori.Add(Convert.ToDecimal(RisultatoSelezionato.PunteggioTecnStrategic));
                    valori.Add(Convert.ToDecimal(RisultatoSelezionato.PunteggioTecnCompetitiveAdv));

                    ValoriGrafico = valori;
                    GraficoVisibile = true;
                    ControlliAbilitati = false;
                }
            }
            else
            {
                //Se era visibile lo nascondo
                GraficoVisibile = false;
                ControlliAbilitati = true;
            }
        }

    }
}
