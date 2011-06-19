using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.Model;
using GeCo.DAL;
using GeCo.Infrastructure;
using Microsoft.Practices.ServiceLocation;

namespace GeCo.BLL.Dati
{
    public class FigureDefault
    {
        class F
        {
            public string t { get; set; }
            public string v { get; set; }
            public string mg { get; set; }
        }

        private static Ruolo SalvaFigura(F[] lista, string nome, string keyRole)
        {
            var reposRuoli = ServiceLocator.Current.GetInstance<IRepository<Ruolo>>();

            var reposComp = ServiceLocator.Current.GetInstance<IRepository<Competenza>>();
            var reposLivelli = ServiceLocator.Current.GetInstance<IRepository<LivelloConoscenza>>();
            var uow = ServiceLocator.Current.GetInstance<IUnitOfWork>();

            List<ConoscenzaCompetenza> conoscenze = new List<ConoscenzaCompetenza>();

            foreach (var elemento in lista)
            {
                ConoscenzaCompetenza conoscenza = new ConoscenzaCompetenza();
                conoscenza.Competenza = reposComp.Single(c => c.Titolo == elemento.t && c.TipologiaCompetenza.MacroGruppo == elemento.mg);
                conoscenza.LivelloConoscenza = reposLivelli.Single(lc => lc.Titolo == elemento.v);
                conoscenze.Add(conoscenza);
            }

            var ruolo = new Ruolo()
            {
                Area = new Area() { Nome = keyRole },
                Titolo = nome,
                Conoscenze = conoscenze
            };



            if (reposRuoli.SingleOrDefault(f => f.Titolo == ruolo.Titolo) == null)
            {
                reposRuoli.Add(ruolo);
                uow.Commit();
            }

            return ruolo;

        }

        #region KEY ROLES STRATEGIC SUPPORT
        
        public static Ruolo SalvaResponsabileUfficioTecnico()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new F() { t="Normative Tecniche", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative Qualità", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative di Settore", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative Ambientali", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative di Sicurezza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Macchinari idonei all'esecuzione", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Movimenti terra", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Opere d'arte", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Contabilità Lavori", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_TECNICO },              

                new F() { t="Planning breve-medio periodo", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Planning medio-lungo periodo", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Monitoraggio e rilievo dell'opera eseguita", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Elaborazione preventivi ed offerte", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Incidenza dei costi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                
                new F() { t="Analisi scostamenti", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Standard di budgeting", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Gestione committente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Tecniche di misurazione", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Tecnica di confezionamento dei c.b. e cementizi", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Tecniche di esecuzione in presenza di traffico ", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Leadership", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Networking", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Efficienza", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Proattività", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                
                #endregion

                #region HR DISCREZIONALI
                
                new F() { t="Assessment", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},
                new F() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},

                #endregion

                #region HR Comportamentali
                
                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Leadership", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Networking", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Efficienza", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Proattività", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                #endregion

            };

            return SalvaFigura(lista, "Responsabile ufficio tecnico", "Strategic Support");

        }

        public static Ruolo SalvaResponsabileImpiantiMobiliMacchineImpianti()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new F() { t="Normative Qualità", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative Ambientali", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative di Sicurezza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Aspetti Contrattualistici", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normativa Giuslavoristica e Contratti di Lavoro", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Leggi macchine speciali", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Composizione budget macchine", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Predisposizione budget macchine speciali", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Processo realizzazione lavori speciali", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Leggi macchine e codice stradale", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Aspetti tecnici macchine speciali", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                new F() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                
                new F() { t="Clausole Contrattualistiche", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                //new F() { t="Negoziazione Offerta Macchine Speciali/Modifiche", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO }, //TODO quella variabile
                new F() { t="Planning Operativo Movimentazione Macchine", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Leadership", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Networking", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Efficienza", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Proattività", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                
                #endregion

               #region HR DISCREZIONALI
                
                new F() { t="Assessment", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},
                new F() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},

                #endregion

                #region HR COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Leadership", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Networking", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Efficienza", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Proattività", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                
                #endregion

            };

            return SalvaFigura(lista, "Responsabile Impianti Mobili e Macchine e Impianti", "Strategic Support");

        }

        public static Ruolo SalvaResponsabileControlliLaboratorio()
        {
            var lista = new[] 
            {
                #region TECNICHE

                new F() { t="Normative Tecniche", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative Qualità", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative di Settore", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative Ambientali", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative di Sicurezza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Macchinari idonei all'esecuzione", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Opere d'arte", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Movimenti terra", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Contabilità Lavori", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },              

                //new F() { t="Incidenza dei costi", v=Tipologiche.Livello.OTTIMO},
                
                new F() { t="Gestione committente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Tecniche di misurazione", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Tecnica di confezionamento dei c.b. e cementizi", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Tecniche di esecuzione in presenza di traffico ", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                #endregion

                #region COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Leadership", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Networking", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Efficienza", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Proattività", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                
                #endregion

                #region HR DISCREZIONALI
                
                new F() { t="Assessment", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},
                new F() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},

                #endregion

                #region HR COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Leadership", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Networking", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Efficienza", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Proattività", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                
                #endregion

            };

            return SalvaFigura(lista, "Responsabile controlli laboratorio", "Strategic Support");
        }

        public static Ruolo SalvaCostController()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new F() { t="Normative Tecniche", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative Qualità", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative di Settore", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative Ambientali", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative di Sicurezza", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Macchinari idonei all'esecuzione", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Contabilità Lavori", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Lettura e Interpretazione del Progetto", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                new F() { t="Planning breve-medio periodo", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Planning medio-lungo periodo", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Controlling", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Elaborazione preventivi ed offerte", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Incidenza dei costi", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                
                //E' sbagliata la categoria
                //new F() { t="Analisi scostamenti", v=Tipologiche.Livello.BUONO},
                
                new F() { t="Gestione riserve e contenzioso", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                
                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="TeamWork", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Negoziazione", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Efficienza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD
                
                #endregion

                #region HR DISCREZIONALI
                
                new F() { t="Assessment", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},
                new F() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},

                #endregion

                #region HR COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="TeamWork", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Negoziazione", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Efficienza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD
                
                #endregion

            };

            return SalvaFigura(lista, "Cost Controller", "Strategic Support");

        }

        public static Ruolo SalvaContabilizzatoreSenior()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new F() { t="Normative di Settore", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Contabilità Lavori", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Lettura e Interpretazione del Progetto", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                //new F() { t="Planning breve-medio periodo", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },  //DA MOD
                //new F() { t="Planning medio-lungo periodo", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },  //DA MOD
                new F() { t="Controlling", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Monitoraggio e rilievo dell'opera eseguita", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Incidenza dei costi", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                
                //E' sbagliata la categoria
                //new F() { t="Analisi scostamenti", v=Tipologiche.Livello.SUFFICIENTE},
                
                new F() { t="Nuovi Prezzi", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO }, 
                //new F() { t="Gestione riserve e contenzioso", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO }, //DA MOD
                
                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="TeamWork", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Assertività", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Negoziazione", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Problem solving", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Visione d'insieme", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Responsabilità", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Efficienza", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD
                
                #endregion

                #region HR DISCREZIONALI
                
                new F() { t="Assessment", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},
                new F() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},

                #endregion

                #region HR COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="TeamWork", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Assertività", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Negoziazione", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Problem solving", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Visione d'insieme", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Responsabilità", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Efficienza", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD
                
                #endregion

            };

            return SalvaFigura(lista, "Contabilizzatore Senior", "Strategic Support");

        }

        #endregion

        #region KEY ROLES COMPETITIVE ADVANTAGE

        public static Ruolo SalvaResponsabileUfficioAcquisti()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new F() { t="Aspetti Contrattualistici", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Lettura e Interpretazione del Progetto", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Emissione Ordine", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Emissione Contratto", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Sistemi Gestionali", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                new F() { t="Mercato di riferimento", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO }, 
                new F() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO }, 
                new F() { t="Tecniche di Ricerca Mercato specifiche del settore", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO }, 
                new F() { t="Negoziazione dell'offerta", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Procedure acquisti", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Predisposizione budget", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                //ASSENTI

                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Leadership", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Networking", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Efficienza", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Proattività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                #endregion

                #region HR DISCREZIONALI
                
                new F() { t="Assessment", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},
                new F() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},

                #endregion

                #region HR COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Leadership", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Networking", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Efficienza", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Proattività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                #endregion
            };

            return SalvaFigura(lista, "Responsabile Ufficio Acquisti", "Competitive Advantage");

        }

        public static Ruolo SalvaDirettoreCantiereManutenzione()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new F() { t="Normative Tecniche", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative Qualità", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative di Settore", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative Ambientali", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative di Sicurezza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Aspetti Contrattualistici", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normativa Giuslavoristica e Contratti di Lavoro", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Lettura e Interpretazione del progetto", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Emissione Ordine", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Emissione Contratto", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Opere d'arte", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Leggi macchine e codice stradale", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Composizione budget macchine", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Macchinari idonei all'esecuzione", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Processo realizzazione lavori speciali", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Aspetti tecnici macchine speciali", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Contabilità Lavori", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                new F() { t="Mercato di riferimento", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Contrattualistica Fornitori", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Contrattualistica Subappaltatori", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Planning breve-medio periodo", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Planning medio-lungo periodo", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Controlling", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Monitoraggio e rilievo dell'opera eseguita", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Tecniche di Ricerca Mercato specifiche del settore", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Elaborazione preventivi ed offerte", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Incidenza dei costi", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Predisposizione budget", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                new F() { t="Gestione committente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Allestimento Cantieri", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Tecniche di misurazione", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Tecnica di confezionamento dei c.b. e cementizi", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Report giornaliero/giornale dei lavori", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Clausole Contrattualistiche", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Nuovi prezzi", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Gestione riserve e contenzioso", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Verifica capitolato e norme di contabilizzazione", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Planning Operativo Movimentazione Macchine", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Tecniche di esecuzione in presenza di traffico ", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },


                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Leadership", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Networking", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Orientamento al cliente", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Efficienza", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Proattività", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                #endregion

               #region HR DISCREZIONALI
                
                new F() { t="Assessment", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},
                new F() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},

                #endregion

                #region HR COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Leadership", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Networking", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Orientamento al cliente", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Efficienza", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Proattività", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                #endregion
            };

            return SalvaFigura(lista, "Direttore Cantiere Manutenzione", "Competitive Advantage");

        }

        public static Ruolo SalvaDirettoreCantiereInfrastrutture()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new F() { t="Normative Tecniche", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative Qualità", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative di Settore", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative Ambientali", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative di Sicurezza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Macchinari idonei all'esecuzione", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Movimenti terra", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Lavori in sotterraneo", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Impalcati", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Opere d'arte", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Contabilità Lavori", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                new F() { t="Mercato di riferimento", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Contrattualistica Fornitori", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Contrattualistica Subappaltatori", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Planning breve-medio periodo", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Planning medio-lungo periodo", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Controlling", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Monitoraggio e rilievo dell'opera eseguita", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Elaborazione preventivi ed offerte", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Incidenza dei costi", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                
                new F() { t="Gestione Committente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Allestimento Cantieri", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Report giornaliero/giornale dei lavori", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Tecniche di esecuzione in presenza di traffico", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },


                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Leadership", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Networking", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Efficienza", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Proattività", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                #endregion

                #region HR DISCREZIONALI
                
                new F() { t="Assessment", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},
                new F() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},

                #endregion

                #region HR COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Leadership", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Networking", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Efficienza", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Proattività", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                #endregion

            };

            return SalvaFigura(lista, "Direttore Cantiere Infrastrutture", "Competitive Advantage");

        }

        public static Ruolo SalvaCapoCantiereManutenzione()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new F() { t="Normative Tecniche", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative Qualità", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative di settore", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative Ambientali", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative di Sicurezza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Aspetti Contrattualistici", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normativa Giuslavoristica e Contratti di Lavoro", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Caratteristiche dei materiali", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Macchinari idonei all'esecuzione", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Emissione Ordine", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Emissione Contratto", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Opere d'arte", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Leggi macchine e codice stradale", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                //new F() { t="Composizione budget macchine", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO }, //DA MOD
                new F() { t="Processo realizzazione lavori speciali", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Lettura e Interpretazione del Progetto", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Aspetti tecnici macchine speciali", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Contabilità Lavori", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                new F() { t="Mercato di riferimento", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Contrattualistica Fornitori", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Contrattualistica Subappaltatori", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Planning breve-medio periodo", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Planning medio-lungo periodo", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Controlling", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Monitoraggio e rilievo dell'opera eseguita", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                //new F() { t="Tecniche di ricerca di mercato specifiche del settore", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO }, //DA MOD
                new F() { t="Elaborazione preventivi ed offerte", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Incidenza dei costi", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Predisposizione budget", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                new F() { t="Gestione Committente", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Allestimento cantieri", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Tecniche di misurazione", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Tecnica di confezionamento dei c.b. e cementizi", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Clausole Contrattualistiche", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Nuovi Prezzi", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Gestione riserve e contenzioso", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Verifica capitolato e norme di contabilizzazione", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                //new F() { t="Planning Operativo Movimentazione Macchine", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO }, //DA MOD
                new F() { t="Tecniche di esecuzione in presenza di traffico", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },


                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Negoziazione", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Efficienza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                #endregion

                #region HR DISCREZIONALI
                
                new F() { t="Assessment", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},
                new F() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},

                #endregion

                #region HR COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Negoziazione", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Efficienza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                #endregion

            };

            return SalvaFigura(lista, "Capo Cantiere Manutenzione", "Competitive Advantage");

        }

        public static Ruolo SalvaCapoCantiereInfrastrutture()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new F() { t="Normative Tecniche", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative Qualità", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative di Settore", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative Ambientali", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Normative di Sicurezza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Macchinari idonei all'esecuzione", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Movimenti terra", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Lavori in sotterraneo", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Impalcati", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Opere d'arte", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Contabilità Lavori", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                new F() { t="Mercato di riferimento", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Contrattualistica Fornitori", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Contrattualistica Subappaltatori", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Planning breve-medio periodo", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Planning medio-lungo periodo", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Controlling", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Monitoraggio e rilievo dell'opera eseguita", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                //new F() { t="Elaborazione preventivi ed offerte", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO }, //DA MOD
                new F() { t="Incidenza dei costi", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                new F() { t="Gestione committente", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Allestimento cantieri", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Report giornaliero/giornale dei lavori", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Tecniche di esecuzione in presenza di traffico", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Negoziazione", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Efficienza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                #endregion

                #region HR DISCREZIONALI
                
                new F() { t="Assessment", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},
                new F() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},

                #endregion

                #region HR COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Negoziazione", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Efficienza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                #endregion

            };

            return SalvaFigura(lista, "Capo Cantiere Infrastrutture", "Competitive Advantage");

        }

        public static Ruolo SalvaBuyerSeniorSede()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new F() { t="Aspetti Contrattualistici", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Lettura e Interpretazione del Progetto", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Emissione Ordine", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Emissione Contratto", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Sistemi Gestionali", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                new F() { t="Mercato di riferimento", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                //new F() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO }, //DA MOD
                new F() { t="Tecniche di Ricerca Mercato specifiche del settore", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Negoziazione dell'offerta", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Procedure acquisti", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Predisposizione budget", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                //ASSENTI

                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Negoziazione", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Efficienza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                #endregion

               #region HR DISCREZIONALI
                
                new F() { t="Assessment", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},
                new F() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},

                #endregion

                #region HR COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Negoziazione", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Efficienza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                #endregion

            };

            return SalvaFigura(lista, "Buyer Senior Sede", "Competitive Advantage");

        }

        public static Ruolo SalvaBuyerSeniorCantiere()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new F() { t="Aspetti Contrattualistici", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Lettura e Interpretazione del Progetto", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Emissione Ordine", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Emissione Contratto", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Sistemi Gestionali", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                new F() { t="Mercato di riferimento", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                //new F() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO }, //DA MOD
                new F() { t="Tecniche di Ricerca Mercato specifiche del settore", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Negoziazione dell'offerta", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Procedure acquisti", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new F() { t="Predisposizione budget", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                //ASSENTI

                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="TeamWork", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new F() { t="Efficienza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                #endregion

               #region HR DISCREZIONALI
                
                new F() { t="Assessment", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},
                new F() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},

                #endregion

                #region HR COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="TeamWork", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new F() { t="Efficienza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                #endregion

            };

            return SalvaFigura(lista, "Buyer Senior Cantiere", "Competitive Advantage");

        }

        #endregion
    }

}
