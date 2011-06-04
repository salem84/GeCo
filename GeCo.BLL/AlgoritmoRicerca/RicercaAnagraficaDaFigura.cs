using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.DAL;
using System.Data.Entity;
using GeCo.Utility;
using GeCo.Model;

namespace GeCo.BLL.AlgoritmoRicerca
{
    public class RicercaAnagraficaDaFigura
    {
        private PavimentalContext context;

        //Mi prendo i punteggi massimi da configurazione DB
        static int PMAX_Hr = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PMAX_HR);
        static int PMAX_Comportamentali = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PMAX_COMPORTAMENTALI);
        static int PMAX_TecnStrategicSupport = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PMAX_TECN_STRATEGIC);
        static int PMAX_TecnCompetitiveAdv = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PMAX_TECN_COMPETITIVE);
        static int PERC_SogliaFoundational = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PERCENTUALE_SOGLIA_FOUNDATIONAL);

        public List<RisultatoRicerca> Cerca(FiguraProfessionale figura)
        {
            List<RisultatoRicerca> risultati = new List<RisultatoRicerca>();

            using (context = new PavimentalContext())
            {
                IEnumerable<Dipendente> dipendenti = GetDipendenti();

                //Per ogni figura mi calcolo gli indici
                foreach (var dipendente in dipendenti)
                {
                    RisultatoRicerca risultato = new RisultatoRicerca();
                    risultato.Nome = string.Format("{0} - {1}", dipendente.Cognome, dipendente.Nome);
                    risultato.PMAX_Hr = PMAX_Hr;
                    risultato.PMAX_Comportamentali = PMAX_Comportamentali;
                    risultato.PMAX_TecnStrategicSupport = PMAX_TecnStrategicSupport;
                    risultato.PMAX_TecnCompetitiveAdv = PMAX_TecnCompetitiveAdv;
                    risultato.PERC_SogliaFoundational = PERC_SogliaFoundational;


                    //Devo lavorare su un sottoinsieme delle conoscenze del dipendente
                    List<ConoscenzaCompetenza> competenzeDaConfrontare = new List<ConoscenzaCompetenza>();
                    //Mi scorro tutte le competenze possedute dal dipendente
                    foreach (var competenza in dipendente.Conoscenze)
                    {
                        //Se è una delle competenze che servono per il confronto
                        if (dipendente.Conoscenze.Contains(competenza, c => c.CompetenzaId))
                        {
                            //l'aggiungo alla lista su cui calcolerò il Punteggio Osservato
                            competenzeDaConfrontare.Add(competenza);
                        }
                        //Se non è presente è come se avessi inserito la competenza con valore 0
                    }

                    //Calcolo punteggio osservato (su dipendente)
                    Punteggi po = Common.CalcolaPunteggi(competenzeDaConfrontare);

                    //Calcolo punteggio atteso
                    Punteggi pa = Common.CalcolaPunteggi(figura.Conoscenze);

                    //Tutte le percentuali vengono calcolate automaticamente
                    risultato.PunteggioOsservato = po;
                    risultato.PunteggioAtteso = pa;

                    risultati.Add(risultato);
                }

            }

            return risultati;

            
        }

        private IEnumerable<Dipendente> GetDipendenti()
        {
            var dipendenti = context.Dipendenti.Include(d => d.Conoscenze.Select(c => c.Competenza))
                                    .Include(d => d.Conoscenze.Select(c => c.LivelloConoscenza))
                                    .Include(d => d.Conoscenze.Select(c => c.Competenza.TipologiaCompetenza))
                                    .AsEnumerable();

            return dipendenti;
        }
    }
}
