using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeCo.DAL.Dati
{
    public class DipendentiDefault
    {
        //Punteggi osservati dal foglio Responsabile Ufficio Tecnico
        public static Dipendente SalvaDipendente1()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new { t="Normative Tecniche", v=Tipologiche.Livello.DISCRETO },
                new { t="Normative Qualità", v=Tipologiche.Livello.DISCRETO},
                new { t="Normative di Settore", v=Tipologiche.Livello.DISCRETO},
                new { t="Normative Ambientali", v=Tipologiche.Livello.DISCRETO},
                new { t="Normative di Sicurezza", v=Tipologiche.Livello.DISCRETO},
                new { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.DISCRETO},
                new { t="Macchinari Idonei all'Esecuzione", v=Tipologiche.Livello.DISCRETO},
                new { t="Contabilità Lavori", v=Tipologiche.Livello.BUONO},

                //NON c'è sul foglio
                new { t="Processo realizzazione lavori speciali", v=Tipologiche.Livello.OTTIMO},
                              
                new { t="Planning breve-medio periodo", v=Tipologiche.Livello.SUFFICIENTE},
                new { t="Planning medio-lungo periodo", v=Tipologiche.Livello.SUFFICIENTE},
                new { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.SUFFICIENTE},
                new { t="Monitoraggio e rilievo dell'opera eseguita", v=Tipologiche.Livello.OTTIMO},
                new { t="Elaborazione preventivi ed offerte", v=Tipologiche.Livello.OTTIMO},
                new { t="Incidenza dei costi", v=Tipologiche.Livello.OTTIMO},
                
                new { t="Analisi scostamenti", v=Tipologiche.Livello.DISCRETO},
                new { t="Tecniche di misurazioni", v=Tipologiche.Livello.DISCRETO},
                new { t="Tecnica di confezionamento dei c.b. e cementizi", v=Tipologiche.Livello.DISCRETO},
                new { t="Tecniche di esecuzione in presenza di traffico ", v=Tipologiche.Livello.DISCRETO},

                //Non c'è sul foglio
                new { t="Planning Operativo Movimentazione Macchine", v=Tipologiche.Livello.OTTIMO},

                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new { t="Integrazione", v=Tipologiche.Livello.OTTIMO},
                new { t="TeamWork", v=Tipologiche.Livello.OTTIMO},
                new { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.BUONO},
                new { t="Leadership", v=Tipologiche.Livello.BUONO},

                new { t="Comunicazione", v=Tipologiche.Livello.OTTIMO},
                new { t="Assertività", v=Tipologiche.Livello.OTTIMO},
                new { t="Negoziazione", v=Tipologiche.Livello.BUONO},
                new { t="Networking", v=Tipologiche.Livello.BUONO},

                new { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO},
                new { t="Problem solving", v=Tipologiche.Livello.OTTIMO},
                new { t="Visione d'insieme", v=Tipologiche.Livello.BUONO},
                new { t="Orientamento al cliente", v=Tipologiche.Livello.OTTIMO},

                new { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO},
                new { t="Responsabilità", v=Tipologiche.Livello.OTTIMO},
                new { t="Efficienza", v=Tipologiche.Livello.BUONO},
                new { t="Proattività", v=Tipologiche.Livello.BUONO},
                
                #endregion

                #region HR
                new { t="Assessment", v=Tipologiche.Livello.OTTIMO},
                new { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO},

                #endregion

            };


            using (PavimentalDb context = new PavimentalDb())
            {
                List<ConoscenzaCompetenza> conoscenze = new List<ConoscenzaCompetenza>();

                foreach (var elemento in lista)
                {
                    ConoscenzaCompetenza conoscenza = new ConoscenzaCompetenza();
                    conoscenza.Competenza = context.Competenze.Single(c => c.Titolo == elemento.t);
                    conoscenza.LivelloConoscenza = context.LivelliConoscenza.Single(lc => lc.Titolo == elemento.v);
                    conoscenze.Add(conoscenza);
                }

                var dipendente = new Dipendente()
                {
                    
                    Cognome = "Dipendente Osservato 1",
                    Nome = "o1",
                    Conoscenze = conoscenze
                };

                if (context.Dipendenti.SingleOrDefault(d => d.Cognome == dipendente.Cognome) == null)
                {
                    context.Dipendenti.Add(dipendente);
                    context.SaveChanges();
                }

                return dipendente;
            }
        }
    }
}
