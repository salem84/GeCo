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
    //Agganciato alla vista VisualizzaDipendente per effettuare la ricerca
    //delle figure professionali a partire da un dipendente
    public class RisultatiFiguraPerDipendenteViewModel : WorkspaceViewModel
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
        
        public Dipendente Dipendente { get; set; }

        public object ParametriConfronto { get; set; }

        //Variabili utilizzate per la progress e lo status (totali e completate)
        private int FigureProfessionaliTotali { get; set; }
        private int FigureProfessionaliAnalizzate { get; set; }

        public RisultatiFiguraPerDipendenteViewModel(Dipendente dipendente)
        {
            DisplayTabName = "Sostituti per " + dipendente.Cognome;

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

            LoadParametri();


            //Non ho salvato il dipendente e mi mancano però le conoscenze per calcolare gli indici
            if (dipendente.Id == 0)
            {
                using (PavimentalDb context = new PavimentalDb())
                {
                    foreach (var cc in dipendente.Conoscenze)
                    {
                        cc.LivelloConoscenza = context.LivelliConoscenza.Find(cc.LivelloConoscenzaId);
                    }
                    Dipendente = dipendente;
                }
            }
            else
            {
                //dipendente deve avere tutte le conoscenze caricate
                Dipendente = dipendente;
            }

            //Non faccio l'autoprogress, setto il valore massimo pari al numero di elementi sul db da analizzare
            using (PavimentalDb context = new PavimentalDb())
            {
                FigureProfessionaliTotali = context.FigureProfessionali.Count();
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
            RicercaFiguraDaAnagrafica algoritmo = new RicercaFiguraDaAnagrafica();
            //Gli passo la funzione per fare l'aggiornamento del progressivo
            var tempRes = algoritmo.Cerca(Dipendente, AggiornaProgress);

            //Rielaboro i dati (ordino e nascondo le percentuali)
            Risultati = tempRes.OrderByDescending(r => r.Idoneo).ThenBy(r => r.PercentualeTotale);
        }

        protected void AggiornaProgress()
        {
            FigureProfessionaliAnalizzate++;
            ProgressPercent = FigureProfessionaliAnalizzate / FigureProfessionaliTotali * 100;
            Stato = string.Format("Completato {0} di {1}", FigureProfessionaliAnalizzate, FigureProfessionaliTotali);

        }
    }
}
