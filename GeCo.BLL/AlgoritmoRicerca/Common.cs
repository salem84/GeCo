using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.DAL;
using GeCo.Model;
using GeCo.Infrastructure;

namespace GeCo.BLL.AlgoritmoRicerca
{
    public class Common
    {
        /// <summary>
        /// Funzione generica per il calcolo della conoscenza delle competenze (raggruppo e sommo i punteggi)
        /// </summary>
        /// <param name="Conoscenze"></param>
        /// <returns></returns>
        public static Punteggi CalcolaPunteggi(ICollection<ConoscenzaCompetenza> Conoscenze)
        {
            Punteggi punteggio = new Punteggi();
            IEnumerable<ConoscenzaCompetenza> conoscenzeFiltrate;

            #region HR DISCREZIONALI
                        
            conoscenzeFiltrate = Conoscenze.Where(cc => cc.Competenza.TipologiaCompetenza.MacroGruppo == Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE);
            punteggio.HrDiscrezionali = conoscenzeFiltrate.Sum(c => c.LivelloConoscenza.Valore);

            #endregion

            #region HR COMPORTAMENTALI

            conoscenzeFiltrate = Conoscenze.Where(cc => cc.Competenza.TipologiaCompetenza.MacroGruppo == Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE);
            punteggio.HrComportamentali = conoscenzeFiltrate.Sum(c => c.LivelloConoscenza.Valore);

            #endregion

            #region COMPORTAMENTALI

            conoscenzeFiltrate = Conoscenze.Where(cc => cc.Competenza.TipologiaCompetenza.MacroGruppo == Tipologiche.Macrogruppi.MG_COMPORTAMENTALE);
            punteggio.Comportamentali = conoscenzeFiltrate.Sum(c => c.LivelloConoscenza.Valore);

            #endregion

            #region TECNICHE

            //Prendo le competenze Tecniche
            IEnumerable<ConoscenzaCompetenza> compTecniche = Conoscenze.Where(cc => cc.Competenza.TipologiaCompetenza.MacroGruppo == Tipologiche.Macrogruppi.MG_TECNICO);

            //Calcolo i punteggi osservati delle tre componenti tecniche 
            conoscenzeFiltrate = compTecniche.Where(cc => cc.Competenza.TipologiaCompetenza.Titolo == Tipologiche.TipologiaCompetenza.T_COMPETITIVE_ADVANTAGE);
            punteggio.TecnCompetitiveAdv = conoscenzeFiltrate.Sum(c => c.LivelloConoscenza.Valore);

            conoscenzeFiltrate = compTecniche.Where(c => c.Competenza.TipologiaCompetenza.Titolo == Tipologiche.TipologiaCompetenza.T_STRATEGIC_SUPPORT);
            punteggio.TecnStrategicSupport = conoscenzeFiltrate.Sum(c => c.LivelloConoscenza.Valore);

            conoscenzeFiltrate = compTecniche.Where(c => c.Competenza.TipologiaCompetenza.Titolo == Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL);
            punteggio.TecnFoundational = conoscenzeFiltrate.Sum(c => c.LivelloConoscenza.Valore);

            #endregion


            return punteggio;
        }
    }
}
