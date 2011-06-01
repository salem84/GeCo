using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.BLL.AlgoritmoRicerca;
using GeCo.DAL;
using System.Data.Entity;
using GeCo.Utility;

namespace GeCo.ViewModel
{
    public class RisultatiDipendentePerFiguraViewModel : WorkspaceViewModel
    {
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

        public FiguraProfessionale Figura { get; set; }

        public object ParametriConfronto { get; set; }

        //Variabili utilizzate per la progress e lo status (totali e completate)
        private int DipendentiTotali { get; set; }
        private int DipendentiAnalizzati { get; set; }


        public RisultatiDipendentePerFiguraViewModel(FiguraProfessionale figura)
        {
            DisplayTabName = "Sostituti per " + figura.Titolo;

            LoadParametri();

            //Non dovrebbe capitare mai
            if (figura.Conoscenze.Count == 0)
            {
                using (PavimentalDb context = new PavimentalDb())
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
            }

            //Non faccio l'autoprogress, setto il valore massimo pari al numero di elementi sul db da analizzare
            using (PavimentalDb context = new PavimentalDb())
            {
                DipendentiTotali = context.Dipendenti.Count();
            }

            //Faccio partire l'algoritmo in background
            StartBackground(AvviaAlgoritmo);
        }


        private void LoadParametri()
        {
            //Carico i parametri da visualizzare
            ParametriConfronto = new
            {
                PMAX_Hr = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PMAX_HR),
                PMAX_Comportamentali = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PMAX_COMPORTAMENTALI),
                PMAX_TecnStrategicSupport = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PMAX_TECN_STRATEGIC),
                PMAX_TecnCompetitiveAdv = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PMAX_TECN_COMPETITIVE),
                PERC_SogliaFoundational = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PERCENTUALE_SOGLIA_FOUNDATIONAL)
            };
        }

        protected void AvviaAlgoritmo()
        {
            RicercaAnagraficaDaFigura algoritmo = new RicercaAnagraficaDaFigura();
            var tempRes = algoritmo.Cerca(Figura);

            //Rielaboro i dati (ordino e nascondo le percentuali)
            Risultati = tempRes.OrderByDescending(r => r.Idoneo).ThenBy(r => r.PercentualeTotale);
        }

        protected void AggiornaProgress()
        {
            DipendentiAnalizzati++;
            ProgressPercent = DipendentiAnalizzati / DipendentiTotali * 100;
            Stato = string.Format("Completato {0} di {1}", DipendentiAnalizzati, DipendentiTotali);

        }
    }
}
