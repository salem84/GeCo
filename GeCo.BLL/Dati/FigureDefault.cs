using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.Model;
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
        }

        private static FiguraProfessionale SalvaFigura(F[] lista, string nome)
        {
            var reposRuoli = ServiceLocator.Current.GetInstance<IRepository<FiguraProfessionale>>();

            var reposComp = ServiceLocator.Current.GetInstance<IRepository<Competenza>>();
            var reposLivelli = ServiceLocator.Current.GetInstance<IRepository<LivelloConoscenza>>();
            var uow = ServiceLocator.Current.GetInstance<IUnitOfWork>();

            List<ConoscenzaCompetenza> conoscenze = new List<ConoscenzaCompetenza>();

            foreach (var elemento in lista)
            {
                ConoscenzaCompetenza conoscenza = new ConoscenzaCompetenza();
                conoscenza.Competenza = reposComp.Single(c => c.Titolo == elemento.t);
                conoscenza.LivelloConoscenza = reposLivelli.Single(lc => lc.Titolo == elemento.v);
                conoscenze.Add(conoscenza);
            }

            var ruolo = new FiguraProfessionale()
            {
                Area = new Area() { Id = 1 },
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

        public static FiguraProfessionale SalvaResponsabileUfficioTecnico()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new F() { t="Normative Tecniche", v=Tipologiche.Livello.BUONO },
                new F() { t="Normative Qualità", v=Tipologiche.Livello.BUONO},
                new F() { t="Normative di Settore", v=Tipologiche.Livello.BUONO},
                new F() { t="Normative Ambientali", v=Tipologiche.Livello.BUONO},
                new F() { t="Normative di Sicurezza", v=Tipologiche.Livello.BUONO},
                new F() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Macchinari idonei all'esecuzione", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Movimenti terra", v=Tipologiche.Livello.BUONO},
                new F() { t="Opere d'arte", v=Tipologiche.Livello.BUONO},
                new F() { t="Contabilità Lavori", v=Tipologiche.Livello.OTTIMO},              

                new F() { t="Planning breve-medio periodo", v=Tipologiche.Livello.BUONO},
                new F() { t="Planning medio-lungo periodo", v=Tipologiche.Livello.BUONO},
                new F() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.BUONO},
                new F() { t="Monitoraggio e rilievo dell'opera eseguita", v=Tipologiche.Livello.BUONO},
                new F() { t="Elaborazione preventivi ed offerte", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Incidenza dei costi", v=Tipologiche.Livello.OTTIMO},
                
                new F() { t="Analisi scostamenti", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Standard di budgeting", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Gestione committente", v=Tipologiche.Livello.BUONO},
                new F() { t="Tecniche di misurazione", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Tecnica di confezionamento dei c.b. e cementizi", v=Tipologiche.Livello.BUONO},
                new F() { t="Tecniche di esecuzione in presenza di traffico ", v=Tipologiche.Livello.BUONO},

                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Leadership", v=Tipologiche.Livello.BUONO},

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Networking", v=Tipologiche.Livello.BUONO},

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO},

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Efficienza", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Proattività", v=Tipologiche.Livello.BUONO},
                
                #endregion

                #region HR
                new F() { t="Assessment", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO},

                #endregion

            };

            return SalvaFigura(lista, "Responsabile ufficio tecnico");

        }

        public static FiguraProfessionale SalvaResponsabileControlliLaboratorio()
        {
            var lista = new[] 
            {
                #region TECNICHE

                new F() { t="Normative Tecniche", v=Tipologiche.Livello.BUONO },
                new F() { t="Normative Qualità", v=Tipologiche.Livello.BUONO},
                new F() { t="Normative di Settore", v=Tipologiche.Livello.BUONO},
                new F() { t="Normative Ambientali", v=Tipologiche.Livello.BUONO},
                new F() { t="Normative di Sicurezza", v=Tipologiche.Livello.BUONO},
                new F() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.BUONO},
                new F() { t="Macchinari idonei all'esecuzione", v=Tipologiche.Livello.BUONO},
                new F() { t="Opere d'arte", v=Tipologiche.Livello.BUONO},
                new F() { t="Movimenti terra", v=Tipologiche.Livello.BUONO},
                new F() { t="Contabilità Lavori", v=Tipologiche.Livello.DISCRETO},              

                //new F() { t="Incidenza dei costi", v=Tipologiche.Livello.OTTIMO},
                
                new F() { t="Gestione committente", v=Tipologiche.Livello.BUONO},
                new F() { t="Tecniche di misurazione", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Tecnica di confezionamento dei c.b. e cementizi", v=Tipologiche.Livello.BUONO},
                new F() { t="Tecniche di esecuzione in presenza di traffico ", v=Tipologiche.Livello.BUONO},

                #endregion

                #region COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Leadership", v=Tipologiche.Livello.BUONO},

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Networking", v=Tipologiche.Livello.BUONO},

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO},

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Efficienza", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Proattività", v=Tipologiche.Livello.BUONO},
                
                #endregion

                #region HR
                new F() { t="Assessment", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO},

                #endregion

            };

            return SalvaFigura(lista, "Responsabile controlli laboratorio");
        }

        public static FiguraProfessionale SalvaResponsabileImpiantiMobiliMacchineImpianti()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new F() { t="Normative Qualità", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Normative Ambientali", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Normative di Sicurezza", v=Tipologiche.Livello.BUONO},
                new F() { t="Aspetti Contrattualistici", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Normativa Giuslavoristica e Contratti di Lavoro", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Leggi macchine speciali", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Composizione budget macchine", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Predisposizione budget macchine speciali", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Processo realizzazione lavori speciali", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Leggi macchine e codice stradale", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Aspetti tecnici macchine speciali", v=Tipologiche.Livello.DISCRETO},

                new F() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.DISCRETO},
                
                new F() { t="Clausole Contrattualistiche", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Negoziazione Offerta Macchine Speciali/Modifiche", v=Tipologiche.Livello.SUFFICIENTE}, //TODO quella variabile
                new F() { t="Planning Operativo Movimentazione Macchine", v=Tipologiche.Livello.BUONO},
                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Leadership", v=Tipologiche.Livello.BUONO},

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Networking", v=Tipologiche.Livello.OTTIMO},

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO},

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Efficienza", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Proattività", v=Tipologiche.Livello.BUONO},
                
                #endregion

                #region HR
                new F() { t="Assessment", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO},

                #endregion

            };

            return SalvaFigura(lista, "Responsabile Impianti Mobili e Macchine e Impianti");

        }

        public static FiguraProfessionale SalvaCostController()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new F() { t="Normative Tecniche", v=Tipologiche.Livello.DISCRETO },
                new F() { t="Normative Qualità", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Normative di Settore", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Normative Ambientali", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Normative di Sicurezza", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Macchinari idonei all'esecuzione", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Contabilità Lavori", v=Tipologiche.Livello.BUONO},
                new F() { t="Lettura e Interpretazione del Progetto", v=Tipologiche.Livello.BUONO},

                new F() { t="Planning breve-medio periodo", v=Tipologiche.Livello.BUONO},
                new F() { t="Planning medio-lungo periodo", v=Tipologiche.Livello.BUONO},
                new F() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Controlling", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Elaborazione preventivi ed offerte", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Incidenza dei costi", v=Tipologiche.Livello.BUONO},
                
                //E' sbagliata la categoria
                //new F() { t="Analisi scostamenti", v=Tipologiche.Livello.BUONO},
                
                new F() { t="Gestione riserve e contenzioso", v=Tipologiche.Livello.DISCRETO},
                
                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="TeamWork", v=Tipologiche.Livello.BUONO},
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD
                new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Negoziazione", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.BUONO},
                new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Efficienza", v=Tipologiche.Livello.BUONO},
                new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD
                
                #endregion

                #region HR
                new F() { t="Assessment", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO},

                #endregion

            };

            return SalvaFigura(lista, "Cost Controller");

        }


        public static FiguraProfessionale SalvaContabilizzatoreSenior()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new F() { t="Normative di Settore", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Contabilità Lavori", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Lettura e Interpretazione del Progetto", v=Tipologiche.Livello.BUONO},

                new F() { t="Planning breve-medio periodo", v=Tipologiche.Livello.SUFFICIENTE},  //DA MOD
                new F() { t="Planning medio-lungo periodo", v=Tipologiche.Livello.SUFFICIENTE},  //DA MOD
                new F() { t="Controlling", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Monitoraggio e rilievo dell'opera eseguita", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Incidenza dei costi", v=Tipologiche.Livello.BUONO},
                
                //E' sbagliata la categoria
                //new F() { t="Analisi scostamenti", v=Tipologiche.Livello.SUFFICIENTE},
                
                new F() { t="Nuovi Prezzi", v=Tipologiche.Livello.DISCRETO}, 
                new F() { t="Gestione riserve e contenzioso", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD
                
                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="TeamWork", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD
                new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Assertività", v=Tipologiche.Livello.BUONO},
                new F() { t="Negoziazione", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD
                new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Problem solving", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD
                new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Responsabilità", v=Tipologiche.Livello.BUONO},
                new F() { t="Efficienza", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD
                new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD
                
                #endregion

                #region HR
                new F() { t="Assessment", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO},

                #endregion

            };

            return SalvaFigura(lista, "Contabilizzatore Senior");

        }

        public static FiguraProfessionale SalvaResponsabileUfficioAcquisti()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new F() { t="Aspetti Contrattualistici", v=Tipologiche.Livello.BUONO},
                new F() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.BUONO},
                new F() { t="Lettura e Interpretazione del Progetto", v=Tipologiche.Livello.BUONO},
                new F() { t="Emissione Ordine", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Emissione Contratto", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Sistemi Gestionali", v=Tipologiche.Livello.BUONO},

                new F() { t="Mercato di riferimento", v=Tipologiche.Livello.BUONO}, 
                new F() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.DISCRETO}, 
                new F() { t="Tecniche di Ricerca Mercato specifiche del settore", v=Tipologiche.Livello.BUONO}, 
                new F() { t="Negoziazione dell'offerta", v=Tipologiche.Livello.BUONO},
                new F() { t="Procedure acquisti", v=Tipologiche.Livello.BUONO},
                new F() { t="Predisposizione budget", v=Tipologiche.Livello.BUONO},

                //ASSENTI

                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Leadership", v=Tipologiche.Livello.OTTIMO},

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Networking", v=Tipologiche.Livello.OTTIMO},

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO},

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Efficienza", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Proattività", v=Tipologiche.Livello.OTTIMO},

                #endregion

                #region HR
                new F() { t="Assessment", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO},

                #endregion

            };

            return SalvaFigura(lista, "Responsabile Ufficio Acquisti");

        }

        public static FiguraProfessionale SalvaDirettoreCantiereManutenzione()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new F() { t="Normative Tecniche", v=Tipologiche.Livello.BUONO},
                new F() { t="Normative Qualità", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Normative di Settore", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Normative Ambientali", v=Tipologiche.Livello.BUONO},
                new F() { t="Normative di Sicurezza", v=Tipologiche.Livello.BUONO},
                new F() { t="Aspetti Contrattualistici", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Normativa Giuslavoristica e Contratti di Lavoro", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Lettura e Interpretazione del progetto", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Emissione Ordine", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Emissione Contratto", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Opere d'arte", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Leggi macchine e codice stradale", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Composizione budget macchine", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Macchinari idonei all'esecuzione", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Processo realizzazione lavori speciali", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Aspetti tecnici macchine speciali", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Contabilità Lavori", v=Tipologiche.Livello.BUONO},

                new F() { t="Mercato di riferimento", v=Tipologiche.Livello.BUONO},
                new F() { t="Contrattualistica Fornitori", v=Tipologiche.Livello.BUONO},
                new F() { t="Contrattualistica Subappaltatori", v=Tipologiche.Livello.BUONO},
                new F() { t="Planning breve-medio periodo", v=Tipologiche.Livello.BUONO},
                new F() { t="Planning medio-lungo periodo", v=Tipologiche.Livello.BUONO},
                new F() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.BUONO},
                new F() { t="Controlling", v=Tipologiche.Livello.BUONO},
                new F() { t="Monitoraggio e rilievo dell'opera eseguita", v=Tipologiche.Livello.BUONO},
                new F() { t="Tecniche di Ricerca Mercato specifiche del settore", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Elaborazione preventivi ed offerte", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Incidenza dei costi", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Predisposizione budget", v=Tipologiche.Livello.SUFFICIENTE},

                new F() { t="Gestione committente", v=Tipologiche.Livello.BUONO},
                new F() { t="Allestimento Cantieri", v=Tipologiche.Livello.BUONO},
                new F() { t="Tecniche di misurazione", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Tecnica di confezionamento dei c.b. e cementizi", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Report giornaliero/giornale dei lavori", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Clausole Contrattualistiche", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Nuovi prezzi", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Gestione riserve e contenzioso", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Verifica capitolato e norme di contabilizzazione", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Planning Operativo Movimentazione Macchine", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Tecniche di esecuzione in presenza di traffico ", v=Tipologiche.Livello.BUONO},


                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Leadership", v=Tipologiche.Livello.OTTIMO},

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Networking", v=Tipologiche.Livello.OTTIMO},

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Orientamento al cliente", v=Tipologiche.Livello.OTTIMO},

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Efficienza", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Proattività", v=Tipologiche.Livello.BUONO},

                #endregion

                #region HR
                new F() { t="Assessment", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO},

                #endregion

            };

            return SalvaFigura(lista, "Direttore Cantiere Manutenzione");

        }

        public static FiguraProfessionale SalvaDirettoreCantiereInfrastrutture()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new F() { t="Normative Tecniche", v=Tipologiche.Livello.BUONO},
                new F() { t="Normative Qualità", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Normative di Settore", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Normative Ambientali", v=Tipologiche.Livello.BUONO},
                new F() { t="Normative di Sicurezza", v=Tipologiche.Livello.BUONO},
                new F() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Macchinari idonei all'esecuzione", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Movimenti terra", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Lavori in sotterraneo", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Impalcati", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Opere d'arte", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Contabilità Lavori", v=Tipologiche.Livello.BUONO},

                new F() { t="Mercato di riferimento", v=Tipologiche.Livello.BUONO},
                new F() { t="Contrattualistica Fornitori", v=Tipologiche.Livello.BUONO},
                new F() { t="Contrattualistica Subappaltatori", v=Tipologiche.Livello.BUONO},
                new F() { t="Planning breve-medio periodo", v=Tipologiche.Livello.BUONO},
                new F() { t="Planning medio-lungo periodo", v=Tipologiche.Livello.BUONO},
                new F() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.BUONO},
                new F() { t="Controlling", v=Tipologiche.Livello.BUONO},
                new F() { t="Monitoraggio e rilievo dell'opera eseguita", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Elaborazione preventivi ed offerte", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Incidenza dei costi", v=Tipologiche.Livello.BUONO},
                
                new F() { t="Gestione Committente", v=Tipologiche.Livello.BUONO},
                new F() { t="Allestimento Cantieri", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Report giornaliero/giornale dei lavori", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Tecniche di esecuzione in presenza di traffico", v=Tipologiche.Livello.DISCRETO},


                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Leadership", v=Tipologiche.Livello.OTTIMO},

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Networking", v=Tipologiche.Livello.OTTIMO},

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO},

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Efficienza", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Proattività", v=Tipologiche.Livello.BUONO},

                #endregion

                #region HR
                new F() { t="Assessment", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO},

                #endregion

            };

            return SalvaFigura(lista, "Direttore Cantiere Infrastrutture");

        }

        public static FiguraProfessionale SalvaCapoCantiereManutenzione()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new F() { t="Normative Tecniche", v=Tipologiche.Livello.BUONO},
                new F() { t="Normative Qualità", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Normative di settore", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Normative Ambientali", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Normative di Sicurezza", v=Tipologiche.Livello.BUONO},
                new F() { t="Aspetti Contrattualistici", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Normativa Giuslavoristica e Contratti di Lavoro", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Caratteristiche dei materiali", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Macchinari idonei all'esecuzione", v=Tipologiche.Livello.BUONO},
                new F() { t="Emissione Ordine", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Emissione Contratto", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Opere d'arte", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Leggi macchine e codice stradale", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Composizione budget macchine", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD
                new F() { t="Processo realizzazione lavori speciali", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Lettura e Interpretazione del Progetto", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Aspetti tecnici macchine speciali", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Contabilità Lavori", v=Tipologiche.Livello.DISCRETO},

                new F() { t="Mercato di riferimento", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Contrattualistica Fornitori", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Contrattualistica Subappaltatori", v=Tipologiche.Livello.BUONO},
                new F() { t="Planning breve-medio periodo", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Planning medio-lungo periodo", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Controlling", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Monitoraggio e rilievo dell'opera eseguita", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Tecniche di ricerca di mercato specifiche del settore", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD
                new F() { t="Elaborazione preventivi ed offerte", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Incidenza dei costi", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Predisposizione budget", v=Tipologiche.Livello.SUFFICIENTE},

                new F() { t="Gestione Committente", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Allestimento cantieri", v=Tipologiche.Livello.BUONO},
                new F() { t="Tecniche di misurazione", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Tecnica di confezionamento dei c.b. e cementizi", v=Tipologiche.Livello.BUONO},
                new F() { t="Clausole Contrattualistiche", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Nuovi Prezzi", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Gestione riserve contenzioso", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Verifica capitolato e norme di contabilizzazione", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Planning Operativo Movimentazione Macchine", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD
                new F() { t="Tecniche di esecuzione in presenza di traffico", v=Tipologiche.Livello.BUONO},


                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.BUONO},
                new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Negoziazione", v=Tipologiche.Livello.BUONO},
                new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.BUONO},
                new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Efficienza", v=Tipologiche.Livello.BUONO},
                new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD

                #endregion

                #region HR
                new F() { t="Assessment", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO},

                #endregion

            };

            return SalvaFigura(lista, "Capo Cantiere Manutenzione");

        }

        public static FiguraProfessionale SalvaCapoCantiereInfrastrutture()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new F() { t="Normative Tecniche", v=Tipologiche.Livello.BUONO},
                new F() { t="Normative Qualità", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Normative di Settore", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Normative Ambientali", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Normative di Sicurezza", v=Tipologiche.Livello.BUONO},
                new F() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Macchinari idonei all'esecuzione", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Movimenti terra", v=Tipologiche.Livello.BUONO},
                new F() { t="Lavori in sotterraneo", v=Tipologiche.Livello.BUONO},
                new F() { t="Impalcati", v=Tipologiche.Livello.BUONO},
                new F() { t="Opere d'arte", v=Tipologiche.Livello.BUONO},
                new F() { t="Contabilità Lavori", v=Tipologiche.Livello.DISCRETO},

                new F() { t="Mercato di riferimento", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Contrattualistica Fornitori", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Contrattualistica Subappaltatori", v=Tipologiche.Livello.BUONO},
                new F() { t="Planning breve-medio periodo", v=Tipologiche.Livello.BUONO},
                new F() { t="Planning medio-lungo periodo", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Controlling", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Monitoraggio e rilievo dell'opera eseguita", v=Tipologiche.Livello.SUFFICIENTE},
                new F() { t="Elaborazione preventivi ed offerte", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD
                new F() { t="Incidenza dei costi", v=Tipologiche.Livello.SUFFICIENTE},

                new F() { t="Gestione committente", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Allestimento cantieri", v=Tipologiche.Livello.BUONO},
                new F() { t="Report giornaliero/giornale dei lavori", v=Tipologiche.Livello.BUONO},
                new F() { t="Tecniche di esecuzione in presenza di traffico", v=Tipologiche.Livello.BUONO},

                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.BUONO},
                new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Negoziazione", v=Tipologiche.Livello.BUONO},
                new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.BUONO},
                new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Efficienza", v=Tipologiche.Livello.BUONO},
                new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD

                #endregion

                #region HR
                new F() { t="Assessment", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO},

                #endregion

            };

            return SalvaFigura(lista, "Capo Cantiere Infrastrutture");

        }


        public static FiguraProfessionale SalvaBuyerSeniorSede()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new F() { t="Aspetti Contrattualistici", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.BUONO},
                new F() { t="Lettura e Interpretazione del Progetto", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Emissione Ordine", v=Tipologiche.Livello.BUONO},
                new F() { t="Emissione Contratto", v=Tipologiche.Livello.BUONO},
                new F() { t="Sistemi Gestionali", v=Tipologiche.Livello.BUONO},

                new F() { t="Mercato di riferimento", v=Tipologiche.Livello.BUONO},
                new F() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD
                new F() { t="Tecniche di Ricerca Mercato specifiche del settore", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Negoziazione dell'offerta", v=Tipologiche.Livello.BUONO},
                new F() { t="Procedure acquisti", v=Tipologiche.Livello.BUONO},
                new F() { t="Predisposizione budget", v=Tipologiche.Livello.DISCRETO},

                //ASSENTI

                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="TeamWork", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD
                new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Negoziazione", v=Tipologiche.Livello.BUONO},
                new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.BUONO},
                new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Efficienza", v=Tipologiche.Livello.BUONO},
                new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD

                #endregion

                #region HR
                new F() { t="Assessment", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO},

                #endregion

            };

            return SalvaFigura(lista, "Buyer Senior Sede");

        }


        public static FiguraProfessionale SalvaBuyerSeniorCantiere()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new F() { t="Aspetti Contrattualistici", v=Tipologiche.Livello.BUONO},
                new F() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.BUONO},
                new F() { t="Lettura ed Interpretazione del Progetto", v=Tipologiche.Livello.DISCRETO},
                new F() { t="Emissione Ordine", v=Tipologiche.Livello.BUONO},
                new F() { t="Emissione Contratto", v=Tipologiche.Livello.BUONO},
                new F() { t="Sistemi Gestionali", v=Tipologiche.Livello.BUONO},

                new F() { t="Mercato di riferimento", v=Tipologiche.Livello.BUONO},
                new F() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD
                new F() { t="Tecniche di Ricerca Mercato specifiche del settore", v=Tipologiche.Livello.BUONO},
                new F() { t="Negoziazione dell'offerta", v=Tipologiche.Livello.BUONO},
                new F() { t="Procedura acquisti", v=Tipologiche.Livello.BUONO},
                new F() { t="Predisposizione budget", v=Tipologiche.Livello.DISCRETO},

                //ASSENTI

                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new F() { t="Integrazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="TeamWork", v=Tipologiche.Livello.BUONO},
                new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD
                new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD

                new F() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Assertività", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD

                new F() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Problem solving", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Visione d'insieme", v=Tipologiche.Livello.BUONO},
                new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD

                new F() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Efficienza", v=Tipologiche.Livello.BUONO},
                new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE}, //DA MOD

                #endregion

                #region HR
                new F() { t="Assessment", v=Tipologiche.Livello.OTTIMO},
                new F() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO},

                #endregion

            };

            return SalvaFigura(lista, "Buyer Senior Cantiere");

        }

    }

}
