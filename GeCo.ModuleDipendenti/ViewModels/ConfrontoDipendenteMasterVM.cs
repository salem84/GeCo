using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.BLL.AlgoritmoRicerca;
using GeCo.DAL;
using GeCo.Utility;
using GeCo.Model;
using GeCo.Infrastructure.Workspace;
using GeCo.Infrastructure;
using GeCo.BLL.Services;

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

        public object ParametriConfronto { get; set; }

        //Variabili utilizzate per la progress e lo status (totali e completate)
        private int FigureProfessionaliTotali { get; set; }
        private int FigureProfessionaliAnalizzate { get; set; }

        private IDipendentiServices _dipendentiServices;
        private IRicercaServices _ricercaServices;

        public ConfrontoDipendenteMasterVM(IDipendentiServices dipServices, IRicercaServices ricercaServices)
        {
            DisplayTabName = "Sostituti";

            _dipendentiServices = dipServices;
            _ricercaServices = ricercaServices;

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


            
                        
        }

        private void AvviaAnalisi()
        {
            DisplayTabName = "Sostituti per " + Dipendente.Cognome;

            //TODO rivedere
            //Non ho salvato il dipendente e mi mancano però le conoscenze per calcolare gli indici
            if (Dipendente.Id == 0)
            {
                var livelliConoscenza = _dipendentiServices.GetLivelliConoscenza();
                
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

        protected void AvviaAlgoritmoBackground()
        {
            
            //Gli passo la funzione per fare l'aggiornamento del progressivo
            var tempRes = _ricercaServices.CercaRuoloDaDipendente(Dipendente);

            //Rielaboro i dati (ordino e nascondo le percentuali)
            Risultati = tempRes.OrderByDescending(r => r.Idoneo).ThenBy(r => r.PercentualeTotale);
        }

        protected void AggiornaProgress()
        {
            //FigureProfessionaliAnalizzate++;
            //ProgressPercent = FigureProfessionaliAnalizzate / FigureProfessionaliTotali * 100;
            //Stato = string.Format("Completato {0} di {1}", FigureProfessionaliAnalizzate, FigureProfessionaliTotali);

        }
    }
}
