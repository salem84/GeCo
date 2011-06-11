using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.BLL.AlgoritmoRicerca;
using GeCo.Utility;
using GeCo.Model;
using GeCo.Infrastructure.Workspace;
using GeCo.BLL.Services;
using GeCo.Infrastructure;

namespace GeCo.ModuleRuoli.ViewModels
{
    public class ConfrontoRuoloVM : Workspace
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

        public FiguraProfessionale Ruolo { get; set; }

        public object ParametriConfronto { get; set; }

        //Variabili utilizzate per la progress e lo status (totali e completate)
        private int DipendentiTotali { get; set; }
        private int DipendentiAnalizzati { get; set; }


        private IDipendentiServices _livelliConoscenzaServices;
        private IRicercaServices _ricercaServices;

        public ConfrontoRuoloVM(IDipendentiServices livelliConoscenzaServices, IRicercaServices ricercaServices)
        {
            DisplayTabName = "Sostituti";

            _livelliConoscenzaServices = livelliConoscenzaServices;
            _ricercaServices = ricercaServices;

            LoadParametri();

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


        private void LoadParametri()
        {
            //Carico i parametri da visualizzare
            ParametriConfronto = new
            {
                PMAX_HrDiscrezionali = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PMAX_HR_DISCREZIONALI),
                PMAX_HrComportamentali = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PMAX_HR_COMPORTAMENTALI),
                PMAX_Comportamentali = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PMAX_COMPORTAMENTALI),
                PMAX_TecnStrategicSupport = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PMAX_TECN_STRATEGIC),
                PMAX_TecnCompetitiveAdv = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PMAX_TECN_COMPETITIVE),
                PERC_SogliaFoundational = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PERCENTUALE_SOGLIA_FOUNDATIONAL)
            };
        }


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
    }
}
