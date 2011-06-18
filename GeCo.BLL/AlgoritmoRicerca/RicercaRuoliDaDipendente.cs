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
    public class RicercaRuoliDaDipendente : IAlgoritmoRicerca
    {

        #region PUNTEGGI MASSIMI E SOGLIE

        //Mi prendo i punteggi massimi da configurazione DB
        static int PMAX_HrDiscrezionali = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PMAX_HR_DISCREZIONALI);
        static int PMAX_HrComportamentali = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PMAX_HR_COMPORTAMENTALI);
        static int PMAX_Comportamentali = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PMAX_COMPORTAMENTALI);
        static int PMAX_TecnStrategicSupport = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PMAX_TECN_STRATEGIC);
        static int PMAX_TecnCompetitiveAdv = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PMAX_TECN_COMPETITIVE);
        static int PERC_SogliaFoundational = ParamsHelper.GetParamValueInt(Tipologiche.Parametro.PERCENTUALE_SOGLIA_FOUNDATIONAL);

        #endregion

        private IRepository<FiguraProfessionale> _ruoliRepos;

        public RicercaRuoliDaDipendente(IRepository<FiguraProfessionale> ruoliRepos)
        {
            _ruoliRepos = ruoliRepos;
        }
        
        //Mi prendo tutte le conoscenze e calcolo i punteggi
        //Che succede se il dipendente ha conoscenze che non sono servono per quel ruolo? Le devo scartare?
        /*public List<RisultatoRicerca> Cerca_SenzaConfronto1a1(Dipendente dipendente)
        {
            using (context = new PavimentalContext())
            {
                //Mi prendo i punteggi massimi da configurazione DB
                
                int PMAX_Hr = 20;
                int PMAX_Comportamentali = 30;
                int PMAX_TecnStrategicSupport = 30;
                int PMAX_TecnCompetitiveAdv = 20;

                int PERC_SogliaFoundational = 90;

                
                //Calcolo punteggio osservato (su dipendente)
                Punteggi po = Common.CalcolaPunteggi(dipendente.Conoscenze);
                

                #region CALCOLO DATI PER OGNI FIGURA
                
                IEnumerable<FiguraProfessionale> figure = GetFigureProfessionali();

                //Per ogni figura mi calcolo gli indici
                foreach(var figura in figure)
                {
                    RisultatoRicerca risultato = new RisultatoRicerca();
                    risultato.PMAX_Hr = PMAX_Hr;
                    risultato.PMAX_Comportamentali = PMAX_Comportamentali;
                    risultato.PMAX_TecnStrategicSupport = PMAX_TecnStrategicSupport;
                    risultato.PMAX_TecnCompetitiveAdv = PMAX_TecnCompetitiveAdv;
                    risultato.PERC_SogliaFoundational = PERC_SogliaFoundational;

                    //Calcolo punteggio atteso
                    Punteggi pa = Common.CalcolaPunteggi(figura.Conoscenze);

                    risultato.PunteggioOsservato = po;
                    risultato.PunteggioAtteso = pa;
                }

                #endregion
            }

            return null;

        }*/

        
        //A differenza dell'altro metodo considero solo le caratteristiche comuni al dipendente e alla figura
        //Mi devo ricalcolare ogni volta il punteggio osservato
        private List<RisultatoRicerca> Cerca(Dipendente dipendente)
        {
            List<RisultatoRicerca> risultati = new List<RisultatoRicerca>();


            IEnumerable<FiguraProfessionale> figure = _ruoliRepos.GetAll();

            //Per ogni figura mi calcolo gli indici
            foreach (var figura in figure)
            {
                RisultatoRicerca risultato = new RisultatoRicerca();
                risultato.Nome = figura.Titolo;
                risultato.Id = figura.Id;
                risultato.PMAX_HrDiscrezionali = PMAX_HrDiscrezionali;
                risultato.PMAX_HrComportamentali = PMAX_HrComportamentali;
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
                    if (figura.Conoscenze.Contains(competenza, c => c.CompetenzaId))
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
