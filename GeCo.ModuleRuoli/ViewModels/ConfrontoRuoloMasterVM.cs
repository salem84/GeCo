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
        public override string IdWorkspace { get { return Names.VIEW_CONFRONTO_MASTER; } }

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

        private Ruolo _ruolo;
        public Ruolo Ruolo
        {
            get
            {
                return _ruolo;
            }
            set
            {
                _ruolo = value;
                AvviaAnalisi();
            }
        }

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
                    //Disabilito i controlli nel caso in cui sto visualizzando il grafico e viceversa
                    ControlliAbilitati = !value;

                    ShowGrafico(_graficoVisibile);
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

        private List<double> _valoriGrafico;
        public List<double> ValoriGrafico
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

        private List<System.Drawing.Color> _paletteColors;
        public List<System.Drawing.Color> PaletteColors
        {
            get { return _paletteColors; }
            set
            {
                if (_paletteColors != value)
                {
                    _paletteColors = value;
                    RaisePropertyChanged("PaletteColors");
                }
            }
        }

        private string _titoloGrafico;
        public string TitoloGrafico
        {
            get { return _titoloGrafico; }
            set
            {
                if (_titoloGrafico != value)
                {
                    _titoloGrafico = value;
                    RaisePropertyChanged("TitoloGrafico");
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

            ControlliAbilitati = true;
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
            confrontoDetailsVM.Atteso = Ruolo;
            confrontoDetailsVM.Osservato = dipendenteSelezionato;
            confrontoDetailsVM.AddToShell();
        }

        private void ShowGrafico(bool visible)
        {
            if (visible == true)
            {
                if (RisultatoSelezionato != null)
                {
                    int cifreDecimali = 1;
                    LabelsGrafico = new List<string>(
                        new string[] { 
                            "HR Discrezionali", 
                            "HR Comportamentali", 
                            "Comportamentali", 
                            "Tecniche Strategic Support", 
                            "Tecniche Competitive Advantage" });

                    TitoloGrafico = string.Format("Confronto tra Ruolo '{0}' e Dipendente {1}", Ruolo.Titolo, RisultatoSelezionato.Nome);

                    var valori = new List<double>();
                    var colors = new List<System.Drawing.Color>();
                    valori.Add(Math.Round(RisultatoSelezionato.PunteggioHrDiscrezionali, cifreDecimali));
                    colors.Add(ColoriPalette.HR_DISCREZIONALI);
                    valori.Add(Math.Round(RisultatoSelezionato.PunteggioHrComportamentali, cifreDecimali));
                    colors.Add(ColoriPalette.HR_COMPORTAMENTALI);
                    valori.Add(Math.Round(RisultatoSelezionato.PunteggioComportamentali, cifreDecimali));
                    colors.Add(ColoriPalette.COMPORTAMENTALI);
                    valori.Add(Math.Round(RisultatoSelezionato.PunteggioTecnStrategic, cifreDecimali));
                    colors.Add(ColoriPalette.TECN_STRATEGIC);
                    valori.Add(Math.Round(RisultatoSelezionato.PunteggioTecnCompetitiveAdv, cifreDecimali));
                    colors.Add(ColoriPalette.TECN_COMPETITIVE);

                    PaletteColors = colors;
                    ValoriGrafico = valori;
                }
            }
            else
            {
                ValoriGrafico = null;
                LabelsGrafico = null;

            }
        }

    }
}
