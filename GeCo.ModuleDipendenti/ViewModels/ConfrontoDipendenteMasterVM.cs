using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.BLL.AlgoritmoRicerca;
using GeCo.DAL;
using GeCo.Model;
using GeCo.Infrastructure.Workspace;
using GeCo.Infrastructure;
using GeCo.BLL.Services;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Command;
using GeCo.BLL;

namespace GeCo.ModuleDipendenti.ViewModels
{
    //Agganciato alla vista VisualizzaDipendente per effettuare la ricerca
    //delle figure professionali a partire da un dipendente
    public class ConfrontoDipendenteMasterVM : Workspace
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

        private Dipendente _dipendente;
        public Dipendente Dipendente 
        { 
            get
            {
                return _dipendente;
            }
            set
            {
                _dipendente = value;
                AvviaAnalisi();
            }
        }

        public ParametriConfronto ParametriConfronto { get; set; }

        //Variabili utilizzate per la progress e lo status (totali e completate)
        private int FigureProfessionaliTotali { get; set; }
        private int FigureProfessionaliAnalizzate { get; set; }

        public RelayCommand DoubleClickCommand { get; set; }

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


        private ICompetenzeServices _competenzeServices;
        private IRicercaServices _ricercaServices;
        private IRuoliServices _ruoliServices;

        public ConfrontoDipendenteMasterVM(ICompetenzeServices compServices, IRuoliServices ruoliServices, IRicercaServices ricercaServices)
        {
            DisplayTabName = "Sostituti";

            _competenzeServices = compServices;
            _ruoliServices = ruoliServices;
            _ricercaServices = ricercaServices;

            DoubleClickCommand = new RelayCommand(ToggleGrafico);
            ParametriConfronto = new ParametriConfronto();

           // _dipendentiServices = ServiceLocator.Current.GetInstance<IDipendentiServices>();
            //_ricercaServices = ServiceLocator.Current.GetInstance<IRicercaServices>();


            /*//Sta arrivando un oggetto con solo l'ID
            if (dipendente.Conoscenze.Count == 0) non mi arriva mai
            {
                //dipendente deve avere tutte le conoscenze caricate
                using (PavimentalDb context = new PavimentalDb())
                {
                    context.Anagrafica.Include(a => a.Conoscenze.Select(c => c.Competenza))
                        .Include(a => a.Conoscenze.Select(c => c.LivelloConoscenza))
                        .Include(a => a.Conoscenze.Select(c => c.Competenza.TipologiaCompetenza));
                    Dipendente = context.Anagrafica.Find(dipendente.Id);
                }
            }*/

            //LoadParametri();


            
                        
        }

        private void AvviaAnalisi()
        {
            DisplayTabName = "Sostituti per " + Dipendente.Cognome;

            //TODO rivedere
            //Non ho salvato il dipendente e mi mancano però le conoscenze per calcolare gli indici
            if (Dipendente.Id == 0)
            {
                var livelliConoscenza = _competenzeServices.GetLivelliConoscenza();
                
                foreach (var c in Dipendente.Conoscenze)
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

       /* private void LoadParametri()
        {
            //Carico i parametri da visualizzare
            ParametriConfronto = new ParametriConfronto();
        }*/

        protected void AvviaAlgoritmoBackground()
        {
            
            //Gli passo la funzione per fare l'aggiornamento del progressivo
            var tempRes = _ricercaServices.CercaRuoloDaDipendente(Dipendente);

            //Rielaboro i dati (ordino e nascondo le percentuali)
            Risultati = tempRes.OrderByDescending(r => r.Idoneo).ThenByDescending(r => r.PunteggioTotale);
            ControlliAbilitati = true;
        }

        protected void AggiornaProgress()
        {
            //FigureProfessionaliAnalizzate++;
            //ProgressPercent = FigureProfessionaliAnalizzate / FigureProfessionaliTotali * 100;
            //Stato = string.Format("Completato {0} di {1}", FigureProfessionaliAnalizzate, FigureProfessionaliTotali);

        }

        public void VisualizzaConfrontoDetails()
        {
            ConfrontoDipendenteDetailsVM confrontoDetailsVM = ServiceLocator.Current.GetInstance<ConfrontoDipendenteDetailsVM>();

            Ruolo ruoloSelezionato = _ruoliServices.CaricaRuolo(RisultatoSelezionato.Id);
            confrontoDetailsVM.Atteso = ruoloSelezionato;
            confrontoDetailsVM.Osservato = Dipendente;
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
