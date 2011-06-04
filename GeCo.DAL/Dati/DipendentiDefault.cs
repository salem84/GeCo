using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.Model;

namespace GeCo.DAL.Dati
{
    public class DipendentiDefault
    {
        class D
        {
            public string t { get; set; }
            public string v { get; set; }
        }

        private static Dipendente SalvaDipendente(D[] lista, string cognome, string nome)
        {
            using (PavimentalContext context = new PavimentalContext())
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

                    Cognome = cognome,
                    Nome = nome,
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


        //Punteggi osservati dal foglio Responsabile Ufficio Tecnico
        public static Dipendente SalvaDipendente1()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new D() { t="Normative Tecniche", v=Tipologiche.Livello.DISCRETO },
                new D() { t="Normative Qualità", v=Tipologiche.Livello.DISCRETO},
                new D() { t="Normative di Settore", v=Tipologiche.Livello.DISCRETO},
                new D() { t="Normative Ambientali", v=Tipologiche.Livello.DISCRETO},
                new D() { t="Normative di Sicurezza", v=Tipologiche.Livello.DISCRETO},
                new D() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.DISCRETO},
                new D() { t="Macchinari idonei all'esecuzione", v=Tipologiche.Livello.DISCRETO},
                new D() { t="Contabilità Lavori", v=Tipologiche.Livello.BUONO},

                //NON c'è sul foglio
                new D() { t="Processo realizzazione lavori speciali", v=Tipologiche.Livello.OTTIMO},
                              
                new D() { t="Planning breve-medio periodo", v=Tipologiche.Livello.SUFFICIENTE},
                new D() { t="Planning medio-lungo periodo", v=Tipologiche.Livello.SUFFICIENTE},
                new D() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.SUFFICIENTE},
                new D() { t="Monitoraggio e rilievo dell'opera eseguita", v=Tipologiche.Livello.OTTIMO},
                new D() { t="Elaborazione preventivi ed offerte", v=Tipologiche.Livello.OTTIMO},
                new D() { t="Incidenza dei costi", v=Tipologiche.Livello.OTTIMO},
                
                new D() { t="Analisi scostamenti", v=Tipologiche.Livello.DISCRETO},
                new D() { t="Tecniche di misurazione", v=Tipologiche.Livello.DISCRETO},
                new D() { t="Tecnica di confezionamento dei c.b. e cementizi", v=Tipologiche.Livello.DISCRETO},
                new D() { t="Tecniche di esecuzione in presenza di traffico ", v=Tipologiche.Livello.DISCRETO},

                //Non c'è sul foglio
                new D() { t="Planning Operativo Movimentazione Macchine", v=Tipologiche.Livello.OTTIMO},

                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new D() { t="Integrazione", v=Tipologiche.Livello.OTTIMO},
                new D() { t="TeamWork", v=Tipologiche.Livello.OTTIMO},
                new D() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.BUONO},
                new D() { t="Leadership", v=Tipologiche.Livello.BUONO},

                new D() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO},
                new D() { t="Assertività", v=Tipologiche.Livello.OTTIMO},
                new D() { t="Negoziazione", v=Tipologiche.Livello.BUONO},
                new D() { t="Networking", v=Tipologiche.Livello.BUONO},

                new D() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO},
                new D() { t="Problem solving", v=Tipologiche.Livello.OTTIMO},
                new D() { t="Visione d'insieme", v=Tipologiche.Livello.BUONO},
                new D() { t="Orientamento al cliente", v=Tipologiche.Livello.OTTIMO},

                new D() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO},
                new D() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO},
                new D() { t="Efficienza", v=Tipologiche.Livello.BUONO},
                new D() { t="Proattività", v=Tipologiche.Livello.BUONO},
                
                #endregion

                #region HR
                new D() { t="Assessment", v=Tipologiche.Livello.OTTIMO},
                new D() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO},

                #endregion

            };

            return SalvaDipendente(lista, "Dipendente Osservato 1", "o1");
            
        }
    }
}
