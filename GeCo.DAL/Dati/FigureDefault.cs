using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeCo.DAL.Dati
{
    public class FigureDefault
    {
        public static FiguraProfessionale SalvaResponsabileUfficioTecnico()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new { t="Normative Tecniche", v=Tipologiche.Livello.BUONO },
                new { t="Normative Qualità", v=Tipologiche.Livello.BUONO},
                new { t="Normative di Settore", v=Tipologiche.Livello.BUONO},
                new { t="Normative Ambientali", v=Tipologiche.Livello.BUONO},
                new { t="Normative di Sicurezza", v=Tipologiche.Livello.BUONO},
                new { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.DISCRETO},
                new { t="Macchinari Idonei all'Esecuzione", v=Tipologiche.Livello.DISCRETO},
                new { t="Movimenti terra", v=Tipologiche.Livello.BUONO},
                new { t="Opere d'arte", v=Tipologiche.Livello.BUONO},
                new { t="Contabilità Lavori", v=Tipologiche.Livello.OTTIMO},              

                new { t="Planning breve-medio periodo", v=Tipologiche.Livello.BUONO},
                new { t="Planning medio-lungo periodo", v=Tipologiche.Livello.BUONO},
                new { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.BUONO},
                new { t="Monitoraggio e rilievo dell'opera eseguita", v=Tipologiche.Livello.BUONO},
                new { t="Elaborazione preventivi ed offerte", v=Tipologiche.Livello.OTTIMO},
                new { t="Incidenza dei costi", v=Tipologiche.Livello.OTTIMO},
                
                new { t="Analisi scostamenti", v=Tipologiche.Livello.OTTIMO},
                new { t="Standard di budgeting", v=Tipologiche.Livello.OTTIMO},
                new { t="Gestione committente", v=Tipologiche.Livello.BUONO},
                new { t="Tecniche di misurazioni", v=Tipologiche.Livello.DISCRETO},
                new { t="Tecnica di confezionamento dei c.b. e cementizi", v=Tipologiche.Livello.BUONO},
                new { t="Tecniche di esecuzione in presenza di traffico ", v=Tipologiche.Livello.BUONO},

                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new { t="Integrazione", v=Tipologiche.Livello.OTTIMO},
                new { t="TeamWork", v=Tipologiche.Livello.OTTIMO},
                new { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO},
                new { t="Leadership", v=Tipologiche.Livello.BUONO},

                new { t="Comunicazione", v=Tipologiche.Livello.OTTIMO},
                new { t="Assertività", v=Tipologiche.Livello.OTTIMO},
                new { t="Negoziazione", v=Tipologiche.Livello.OTTIMO},
                new { t="Networking", v=Tipologiche.Livello.BUONO},

                new { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO},
                new { t="Problem solving", v=Tipologiche.Livello.OTTIMO},
                new { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO},
                new { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO},

                new { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO},
                new { t="Responsabilità", v=Tipologiche.Livello.OTTIMO},
                new { t="Efficienza", v=Tipologiche.Livello.OTTIMO},
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

                var figura = new FiguraProfessionale()
                {
                    Area = new Area() { Id = 1 },
                    Titolo = "Responsabile ufficio tecnico",
                    Conoscenze = conoscenze
                };

                if (context.FigureProfessionali.SingleOrDefault(f => f.Titolo == figura.Titolo) == null)
                {
                    context.FigureProfessionali.Add(figura);
                    context.SaveChanges();
                }
                return figura;
            }
        }

        public static FiguraProfessionale SalvaResponsabileControlliLaboratorio()
        {
            var lista = new[] 
            {
                #region TECNICHE

                new { t="Normative Tecniche", v=Tipologiche.Livello.BUONO },
                new { t="Normative Qualità", v=Tipologiche.Livello.BUONO},
                new { t="Normative di Settore", v=Tipologiche.Livello.BUONO},
                new { t="Normative Ambientali", v=Tipologiche.Livello.BUONO},
                new { t="Normative di Sicurezza", v=Tipologiche.Livello.BUONO},
                new { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.BUONO},
                new { t="Macchinari Idonei all'Esecuzione", v=Tipologiche.Livello.BUONO},
                new { t="Opere d'arte", v=Tipologiche.Livello.BUONO},
                new { t="Movimenti terra", v=Tipologiche.Livello.BUONO},
                new { t="Contabilità Lavori", v=Tipologiche.Livello.DISCRETO},              

                //new { t="Incidenza dei costi", v=Tipologiche.Livello.OTTIMO},
                
                new { t="Gestione committente", v=Tipologiche.Livello.BUONO},
                new { t="Tecniche di misurazioni", v=Tipologiche.Livello.DISCRETO},
                new { t="Tecnica di confezionamento dei c.b. e cementizi", v=Tipologiche.Livello.BUONO},
                new { t="Tecniche di esecuzione in presenza di traffico ", v=Tipologiche.Livello.BUONO},

                #endregion

                #region COMPORTAMENTALI

                new { t="Integrazione", v=Tipologiche.Livello.OTTIMO},
                new { t="TeamWork", v=Tipologiche.Livello.OTTIMO},
                new { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO},
                new { t="Leadership", v=Tipologiche.Livello.BUONO},

                new { t="Comunicazione", v=Tipologiche.Livello.OTTIMO},
                new { t="Assertività", v=Tipologiche.Livello.OTTIMO},
                new { t="Negoziazione", v=Tipologiche.Livello.OTTIMO},
                new { t="Networking", v=Tipologiche.Livello.BUONO},

                new { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO},
                new { t="Problem solving", v=Tipologiche.Livello.OTTIMO},
                new { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO},
                new { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO},

                new { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO},
                new { t="Responsabilità", v=Tipologiche.Livello.OTTIMO},
                new { t="Efficienza", v=Tipologiche.Livello.OTTIMO},
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

                var figura = new FiguraProfessionale()
                {
                    Area = new Area() { Id = 1 },
                    Titolo = "Responsabile controlli laboratorio",
                    Conoscenze = conoscenze
                };

                if (context.FigureProfessionali.SingleOrDefault(f => f.Titolo == figura.Titolo) == null)
                {
                    context.FigureProfessionali.Add(figura);
                    context.SaveChanges();
                }
                return figura;
            }
        }
    }
}
