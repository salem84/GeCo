using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.DAL;

namespace GeCo.BLL.AlgoritmoRicerca
{
    public class Common
    {
        public static Punteggi CalcolaPunteggi(ICollection<ConoscenzaCompetenza> Conoscenze)
        {
            Punteggi punteggio = new Punteggi();
            IEnumerable<ConoscenzaCompetenza> conoscenzeFiltrate;

            //Calcolo i punteggi osservati per il dipendente in esame
            conoscenzeFiltrate = Conoscenze.Where(cc => cc.Competenza.TipologiaCompetenza.MacroGruppo == Tipologiche.MG_HR);
            punteggio.HR = conoscenzeFiltrate.Sum(c => c.LivelloConoscenza.Valore);

            conoscenzeFiltrate = Conoscenze.Where(cc => cc.Competenza.TipologiaCompetenza.MacroGruppo == Tipologiche.MG_COMPORTAMENTALE);
            punteggio.Comportamentali = conoscenzeFiltrate.Sum(c => c.LivelloConoscenza.Valore);

            //Prendo le competenze Tecniche
            IEnumerable<ConoscenzaCompetenza> compTecniche = Conoscenze.Where(cc => cc.Competenza.TipologiaCompetenza.MacroGruppo == Tipologiche.MG_TECNICO);

            //Calcolo i punteggi osservati delle tre componenti tecniche 
            conoscenzeFiltrate = compTecniche.Where(cc => cc.Competenza.TipologiaCompetenza.Titolo == Tipologiche.TipologiaCompetenza.COMPETITIVE_ADVANTAGE);
            punteggio.TecnCompetitiveAdv = conoscenzeFiltrate.Sum(c => c.LivelloConoscenza.Valore);

            conoscenzeFiltrate = compTecniche.Where(c => c.Competenza.TipologiaCompetenza.Titolo == Tipologiche.TipologiaCompetenza.STRATEGIC_SUPPORT);
            punteggio.TecnStrategicSupport = conoscenzeFiltrate.Sum(c => c.LivelloConoscenza.Valore);

            conoscenzeFiltrate = compTecniche.Where(c => c.Competenza.TipologiaCompetenza.Titolo == Tipologiche.TipologiaCompetenza.FOUNDATIONAL);
            punteggio.TecnFoundational = conoscenzeFiltrate.Sum(c => c.LivelloConoscenza.Valore);

            return punteggio;
        }
    }
}
