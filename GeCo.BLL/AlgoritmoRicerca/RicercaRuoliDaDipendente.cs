using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.DAL;
using System.Data.Entity;
using GeCo.Model;
using GeCo.Infrastructure;
using GeCo.BLL.Services;

namespace GeCo.BLL.AlgoritmoRicerca
{

    /// <summary>
    /// UTILIZZATO DA MODULE_DIPENDENTE
    /// </summary>
    public class RicercaRuoliDaDipendente : IAlgoritmoRicerca
    {

        ParametriConfronto _parametriConfronto;
        private IRepository<Ruolo> _ruoliRepos;
        private ICompetenzeServices _competenzeServices;

        public RicercaRuoliDaDipendente(IRepository<Ruolo> ruoliRepos, ICompetenzeServices competenzeServices)
        {
            _ruoliRepos = ruoliRepos;
            _competenzeServices = competenzeServices;
            _parametriConfronto = new ParametriConfronto();
            
        }
        
        
        
        //A differenza dell'altro metodo considero solo le caratteristiche comuni al dipendente e alla figura
        //Mi devo ricalcolare ogni volta il punteggio osservato
        private List<RisultatoRicerca> Cerca(Dipendente dipendente)
        {
            List<RisultatoRicerca> risultati = new List<RisultatoRicerca>();


            IEnumerable<Ruolo> tuttiRuoli = _ruoliRepos.GetAll();

            //Per ogni figura mi calcolo gli indici
            foreach (var ruolo in tuttiRuoli)
            {
                RisultatoRicerca risultato = new RisultatoRicerca();
                risultato.Nome = ruolo.Titolo;
                risultato.Id = ruolo.Id;
                risultato.PMAX_HrDiscrezionali = _parametriConfronto.PMAX_HrDiscrezionali;
                risultato.PMAX_HrComportamentali = _parametriConfronto.PMAX_HrComportamentali;
                risultato.PMAX_Comportamentali = _parametriConfronto.PMAX_Comportamentali;
                risultato.PMAX_TecnStrategicSupport = _parametriConfronto.PMAX_TecnStrategicSupport;
                risultato.PMAX_TecnCompetitiveAdv = _parametriConfronto.PMAX_TecnCompetitiveAdv;
                risultato.PERC_SogliaFoundational = _parametriConfronto.PERC_SogliaFoundational;


                //Devo lavorare su un sottoinsieme delle conoscenze del dipendente
                List<ConoscenzaCompetenza> competenzeDaConfrontare = new List<ConoscenzaCompetenza>();
                //Mi scorro tutte le competenze possedute dal dipendente
                foreach (var conoscenzaDip in dipendente.Conoscenze)
                {
                    //Se è una delle competenze che servono per il confronto
                    if (ruolo.Conoscenze.Contains(conoscenzaDip, c => c.CompetenzaId))
                    {
                        //l'aggiungo alla lista su cui calcolerò il Punteggio Osservato
                        competenzeDaConfrontare.Add(conoscenzaDip);
                    }
                    //Se non è presente è come se avessi inserito la competenza con valore 0
                }
                
                //***** CORREZIONEEEE
                /*
                LivelloConoscenza nulla = new LivelloConoscenza() { Titolo = "Nulla", Valore = 0};

                //Mi devo scorrere tutte le competenze del ruolo
                foreach (var conoscenzaRuolo in figura.Conoscenze)
                {
                    //Se è una di quelle competenze possedute anche dal dipendente, aggiungo il valore per il confronto
                    if (dipendente.Conoscenze.Contains(conoscenzaRuolo, c => c.CompetenzaId))
                    {
                        //l'aggiungo alla lista su cui calcolerò il Punteggio Osservato
                        competenzeDaConfrontare.Add(conoscenzaRuolo);
                    }
                    else
                    {
                        var competenzaNulla = _competenzeServices.GetCompetenze().Single(c => c.Id == conoscenzaRuolo.CompetenzaId);

                        //competenzeDaConfrontare.Add();
                    }
                }*/


                //Calcolo punteggio osservato (su dipendente)
                Punteggi po = Common.CalcolaPunteggi(competenzeDaConfrontare);

                //Calcolo punteggio atteso
                Punteggi pa = Common.CalcolaPunteggi(ruolo.Conoscenze);

                //Tutte le percentuali vengono calcolate automaticamente
                risultato.PunteggioOsservato = po;
                risultato.PunteggioAtteso = pa;

                risultati.Add(risultato);

                //Lancio l'evento di aggiornamento
                //progressAction();
            }

            

            return risultati;

        }
        
        /*
        private IEnumerable<FiguraProfessionale> GetFigureProfessionali()
        {
            var figure = _ruoliRepos.Include(f => f.Conoscenze.Select(c => c.Competenza))
                                    .Include(f => f.Conoscenze.Select(c => c.LivelloConoscenza))
                                    .Include(f => f.Conoscenze.Select(c => c.Competenza.TipologiaCompetenza))
                                    .AsEnumerable();

            return figure;
        }*/

        public List<RisultatoRicerca> Cerca(Anagrafica dipendente)
        {
            return Cerca(dipendente as Dipendente);
        }
    }
}
