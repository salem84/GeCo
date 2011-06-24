using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.DAL;
using System.Data.Entity;
using GeCo.BLL;
using GeCo.Model;
using GeCo.Infrastructure;

namespace GeCo.BLL.AlgoritmoRicerca
{

    /// <summary>
    /// UTILIZZATO DA MODULE_RUOLO
    /// </summary>
    public class RicercaDipendentiDaRuolo : IAlgoritmoRicerca
    {
        ParametriConfronto _parametriConfronto;
        IRepository<Dipendente> _dipendentiRepos;

        public RicercaDipendentiDaRuolo(IRepository<Dipendente> dipendentiRepos)
        {
            _dipendentiRepos = dipendentiRepos;
            _parametriConfronto = new ParametriConfronto();
        }

        
        private List<RisultatoRicerca> Cerca(Ruolo ruolo)
        {
            List<RisultatoRicerca> risultati = new List<RisultatoRicerca>();


            IEnumerable<Dipendente> dipendenti = _dipendentiRepos.GetAll();

            //Per ogni figura mi calcolo gli indici
            foreach (var dipendente in dipendenti)
            {
                RisultatoRicerca risultato = new RisultatoRicerca();
                risultato.Nome = string.Format("{0} - {1}", dipendente.Cognome, dipendente.Nome);
                risultato.Id = dipendente.Id;
                risultato.PMAX_HrDiscrezionali = _parametriConfronto.PMAX_HrDiscrezionali;
                risultato.PMAX_HrComportamentali = _parametriConfronto.PMAX_HrComportamentali;
                risultato.PMAX_Comportamentali = _parametriConfronto.PMAX_Comportamentali;
                risultato.PMAX_TecnStrategicSupport = _parametriConfronto.PMAX_TecnStrategicSupport;
                risultato.PMAX_TecnCompetitiveAdv = _parametriConfronto.PMAX_TecnCompetitiveAdv;
                risultato.PERC_SogliaFoundational = _parametriConfronto.PERC_SogliaFoundational;


                //Devo lavorare su un sottoinsieme delle conoscenze del dipendente
                List<ConoscenzaCompetenza> competenzeDaConfrontare = new List<ConoscenzaCompetenza>();
                //Mi scorro tutte le competenze possedute dal ruolo
                foreach (var competenza in ruolo.Conoscenze)
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
                Punteggi pa = Common.CalcolaPunteggi(dipendente.Conoscenze);

                //Tutte le percentuali vengono calcolate automaticamente
                risultato.PunteggioOsservato = po;
                risultato.PunteggioAtteso = pa;

                risultati.Add(risultato);
            }


            return risultati;


        }

        /*private IEnumerable<Dipendente> GetDipendenti()
        {
            var dipendenti = context.Dipendenti.Include(d => d.Conoscenze.Select(c => c.Competenza))
                                    .Include(d => d.Conoscenze.Select(c => c.LivelloConoscenza))
                                    .Include(d => d.Conoscenze.Select(c => c.Competenza.TipologiaCompetenza))
                                    .AsEnumerable();

            return dipendenti;
        }*/

        public List<RisultatoRicerca> Cerca(Anagrafica ruolo)
        {
            return Cerca(ruolo as Ruolo);
        }
    }
}
