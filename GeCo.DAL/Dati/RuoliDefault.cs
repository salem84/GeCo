using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.Model;
using GeCo.DAL;
using GeCo.Infrastructure;

namespace GeCo.DAL.Dati
{
    public class RuoliDefault
    {
        private IDbContextAdapter dbContext;

        public RuoliDefault(IDbContextAdapter context)
        {
            dbContext = context;
        }

        class R
        {
            public string t { get; set; }
            public string v { get; set; }
            public string mg { get; set; }
        }

        private Ruolo SalvaRuolo(R[] lista, string nome, string keyRole)
        {
            /*var reposRuoli = ServiceLocator.Current.GetInstance<IRepository<Ruolo>>();

            var reposComp = ServiceLocator.Current.GetInstance<IRepository<Competenza>>();
            var reposLivelli = ServiceLocator.Current.GetInstance<IRepository<LivelloConoscenza>>();
            var uow = ServiceLocator.Current.GetInstance<IUnitOfWork>();*/
            var reposRuoli = new BaseRepository<Ruolo>(dbContext);

            var reposComp = new BaseRepository<Competenza>(dbContext);
            var reposLivelli = new BaseRepository<LivelloConoscenza>(dbContext);
            var uow = new UnitOfWork(dbContext);


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
        
        public Ruolo SalvaResponsabileUfficioTecnico()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new R() { t="Normative Tecniche", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative Qualità", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative di Settore", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative Ambientali", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative di Sicurezza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Macchinari idonei all'esecuzione", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Movimenti terra", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Opere d'arte", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Contabilità Lavori", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_TECNICO },              

                new R() { t="Planning breve-medio periodo", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Planning medio-lungo periodo", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Monitoraggio e rilievo dell'opera eseguita", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Elaborazione preventivi ed offerte", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Incidenza dei costi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                
                new R() { t="Analisi scostamenti", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Standard di budgeting", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Gestione committente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Tecniche di misurazione", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Tecnica di confezionamento dei c.b. e cementizi", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Tecniche di esecuzione in presenza di traffico ", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new R() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Leadership", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new R() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Networking", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new R() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new R() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Efficienza", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Proattività", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                
                #endregion

                #region HR DISCREZIONALI
                
                new R() { t="Assessment", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},
                new R() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},

                #endregion

                #region HR Comportamentali
                
                new R() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Leadership", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new R() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Networking", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new R() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new R() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Efficienza", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Proattività", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                #endregion

            };

            return SalvaRuolo(lista, "Responsabile ufficio tecnico", "Strategic Support");

        }

        public Ruolo SalvaResponsabileImpiantiMobiliMacchineImpianti()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new R() { t="Normative Qualità", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative Ambientali", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative di Sicurezza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Aspetti Contrattualistici", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normativa Giuslavoristica e Contratti di Lavoro", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Leggi macchine speciali", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Composizione budget macchine", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Predisposizione budget macchine speciali", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Processo realizzazione lavori speciali", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Leggi macchine e codice stradale", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Aspetti tecnici macchine speciali", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                new R() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                
                new R() { t="Clausole Contrattualistiche", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                //new F() { t="Negoziazione Offerta Macchine Speciali/Modifiche", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO }, //TODO quella variabile
                new R() { t="Planning Operativo Movimentazione Macchine", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new R() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Leadership", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new R() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Networking", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new R() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new R() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Efficienza", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Proattività", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                
                #endregion

               #region HR DISCREZIONALI
                
                new R() { t="Assessment", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},
                new R() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},

                #endregion

                #region HR COMPORTAMENTALI

                new R() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Leadership", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new R() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Networking", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new R() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new R() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Efficienza", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Proattività", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                
                #endregion

            };

            return SalvaRuolo(lista, "Responsabile Impianti Mobili e Macchine e Impianti", "Strategic Support");

        }

        public Ruolo SalvaResponsabileControlliLaboratorio()
        {
            var lista = new[] 
            {
                #region TECNICHE

                new R() { t="Normative Tecniche", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative Qualità", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative di Settore", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative Ambientali", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative di Sicurezza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Macchinari idonei all'esecuzione", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Opere d'arte", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Movimenti terra", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Contabilità Lavori", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },              

                //new F() { t="Incidenza dei costi", v=Tipologiche.Livello.OTTIMO},
                
                new R() { t="Gestione committente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Tecniche di misurazione", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Tecnica di confezionamento dei c.b. e cementizi", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Tecniche di esecuzione in presenza di traffico ", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                #endregion

                #region COMPORTAMENTALI

                new R() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Leadership", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new R() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Networking", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new R() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new R() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Efficienza", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Proattività", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                
                #endregion

                #region HR DISCREZIONALI
                
                new R() { t="Assessment", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},
                new R() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},

                #endregion

                #region HR COMPORTAMENTALI

                new R() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Leadership", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new R() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Networking", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new R() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new R() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Efficienza", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Proattività", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                
                #endregion

            };

            return SalvaRuolo(lista, "Responsabile controlli laboratorio", "Strategic Support");
        }

        public Ruolo SalvaCostController()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new R() { t="Normative Tecniche", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative Qualità", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative di Settore", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative Ambientali", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative di Sicurezza", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Macchinari idonei all'esecuzione", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Contabilità Lavori", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Lettura e Interpretazione del Progetto", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                new R() { t="Planning breve-medio periodo", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Planning medio-lungo periodo", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Controlling", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Elaborazione preventivi ed offerte", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Incidenza dei costi", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                
                //E' sbagliata la categoria
                //new F() { t="Analisi scostamenti", v=Tipologiche.Livello.BUONO},
                
                new R() { t="Gestione riserve e contenzioso", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                
                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new R() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="TeamWork", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new R() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Negoziazione", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new R() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Visione d'insieme", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new R() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Efficienza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD
                
                #endregion

                #region HR DISCREZIONALI
                
                new R() { t="Assessment", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},
                new R() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},

                #endregion

                #region HR COMPORTAMENTALI

                new R() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="TeamWork", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new R() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Negoziazione", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new R() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Visione d'insieme", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new R() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Efficienza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD
                
                #endregion

            };

            return SalvaRuolo(lista, "Cost Controller", "Strategic Support");

        }

        public Ruolo SalvaContabilizzatoreSenior()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new R() { t="Normative di Settore", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Contabilità Lavori", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Lettura e Interpretazione del Progetto", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                //new F() { t="Planning breve-medio periodo", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },  //DA MOD
                //new F() { t="Planning medio-lungo periodo", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },  //DA MOD
                new R() { t="Controlling", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Monitoraggio e rilievo dell'opera eseguita", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Incidenza dei costi", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                
                //E' sbagliata la categoria
                //new F() { t="Analisi scostamenti", v=Tipologiche.Livello.SUFFICIENTE},
                
                new R() { t="Nuovi Prezzi", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO }, 
                //new F() { t="Gestione riserve e contenzioso", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO }, //DA MOD
                
                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new R() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="TeamWork", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new R() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Assertività", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Negoziazione", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new R() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Problem solving", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Visione d'insieme", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new R() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Responsabilità", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Efficienza", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD
                
                #endregion

                #region HR DISCREZIONALI
                
                new R() { t="Assessment", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},
                new R() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},

                #endregion

                #region HR COMPORTAMENTALI

                new R() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="TeamWork", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new R() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Assertività", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Negoziazione", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new R() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Problem solving", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Visione d'insieme", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new R() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Responsabilità", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Efficienza", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD
                
                #endregion

            };

            return SalvaRuolo(lista, "Contabilizzatore Senior", "Strategic Support");

        }

        #endregion

        #region KEY ROLES COMPETITIVE ADVANTAGE

        public Ruolo SalvaResponsabileUfficioAcquisti()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new R() { t="Aspetti Contrattualistici", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Lettura e Interpretazione del Progetto", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Emissione Ordine", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Emissione Contratto", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Sistemi Gestionali", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                new R() { t="Mercato di riferimento", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO }, 
                new R() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO }, 
                new R() { t="Tecniche di Ricerca Mercato specifiche del settore", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO }, 
                new R() { t="Negoziazione dell'offerta", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Procedure acquisti", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Predisposizione budget", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                //ASSENTI

                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new R() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Leadership", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new R() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Networking", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new R() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new R() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Efficienza", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Proattività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                #endregion

                #region HR DISCREZIONALI
                
                new R() { t="Assessment", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},
                new R() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},

                #endregion

                #region HR COMPORTAMENTALI

                new R() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Leadership", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new R() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Networking", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new R() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new R() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Efficienza", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Proattività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                #endregion
            };

            return SalvaRuolo(lista, "Responsabile Ufficio Acquisti", "Competitive Advantage");

        }

        public Ruolo SalvaDirettoreCantiereManutenzione()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new R() { t="Normative Tecniche", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative Qualità", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative di Settore", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative Ambientali", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative di Sicurezza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Aspetti Contrattualistici", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normativa Giuslavoristica e Contratti di Lavoro", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Lettura e Interpretazione del progetto", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Emissione Ordine", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Emissione Contratto", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Opere d'arte", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Leggi macchine e codice stradale", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Composizione budget macchine", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Macchinari idonei all'esecuzione", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Processo realizzazione lavori speciali", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Aspetti tecnici macchine speciali", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Contabilità Lavori", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                new R() { t="Mercato di riferimento", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Contrattualistica Fornitori", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Contrattualistica Subappaltatori", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Planning breve-medio periodo", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Planning medio-lungo periodo", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Controlling", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Monitoraggio e rilievo dell'opera eseguita", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Tecniche di Ricerca Mercato specifiche del settore", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Elaborazione preventivi ed offerte", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Incidenza dei costi", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Predisposizione budget", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                new R() { t="Gestione committente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Allestimento Cantieri", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Tecniche di misurazione", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Tecnica di confezionamento dei c.b. e cementizi", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Report giornaliero/giornale dei lavori", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Clausole Contrattualistiche", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Nuovi prezzi", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Gestione riserve e contenzioso", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Verifica capitolato e norme di contabilizzazione", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Planning Operativo Movimentazione Macchine", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Tecniche di esecuzione in presenza di traffico ", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },


                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new R() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Leadership", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new R() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Networking", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new R() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Orientamento al cliente", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new R() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Efficienza", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Proattività", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                #endregion

               #region HR DISCREZIONALI
                
                new R() { t="Assessment", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},
                new R() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},

                #endregion

                #region HR COMPORTAMENTALI

                new R() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Leadership", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new R() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Networking", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new R() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Orientamento al cliente", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new R() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Efficienza", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Proattività", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                #endregion
            };

            return SalvaRuolo(lista, "Direttore Cantiere Manutenzione", "Competitive Advantage");

        }

        public Ruolo SalvaDirettoreCantiereInfrastrutture()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new R() { t="Normative Tecniche", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative Qualità", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative di Settore", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative Ambientali", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative di Sicurezza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Macchinari idonei all'esecuzione", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Movimenti terra", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Lavori in sotterraneo", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Impalcati", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Opere d'arte", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Contabilità Lavori", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                new R() { t="Mercato di riferimento", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Contrattualistica Fornitori", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Contrattualistica Subappaltatori", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Planning breve-medio periodo", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Planning medio-lungo periodo", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Controlling", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Monitoraggio e rilievo dell'opera eseguita", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Elaborazione preventivi ed offerte", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Incidenza dei costi", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                
                new R() { t="Gestione Committente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Allestimento Cantieri", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Report giornaliero/giornale dei lavori", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Tecniche di esecuzione in presenza di traffico", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },


                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new R() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Leadership", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new R() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Networking", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new R() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                new R() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Efficienza", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Proattività", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },

                #endregion

                #region HR DISCREZIONALI
                
                new R() { t="Assessment", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},
                new R() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},

                #endregion

                #region HR COMPORTAMENTALI

                new R() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Leadership", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new R() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Networking", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new R() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Visione d'insieme", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Orientamento al cliente", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                new R() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Efficienza", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Proattività", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },

                #endregion

            };

            return SalvaRuolo(lista, "Direttore Cantiere Infrastrutture", "Competitive Advantage");

        }

        public Ruolo SalvaCapoCantiereManutenzione()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new R() { t="Normative Tecniche", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative Qualità", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative di settore", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative Ambientali", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative di Sicurezza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Aspetti Contrattualistici", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normativa Giuslavoristica e Contratti di Lavoro", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Caratteristiche dei materiali", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Macchinari idonei all'esecuzione", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Emissione Ordine", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Emissione Contratto", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Opere d'arte", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Leggi macchine e codice stradale", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                //new F() { t="Composizione budget macchine", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO }, //DA MOD
                new R() { t="Processo realizzazione lavori speciali", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Lettura e Interpretazione del Progetto", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Aspetti tecnici macchine speciali", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Contabilità Lavori", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                new R() { t="Mercato di riferimento", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Contrattualistica Fornitori", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Contrattualistica Subappaltatori", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Planning breve-medio periodo", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Planning medio-lungo periodo", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Controlling", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Monitoraggio e rilievo dell'opera eseguita", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                //new F() { t="Tecniche di ricerca di mercato specifiche del settore", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO }, //DA MOD
                new R() { t="Elaborazione preventivi ed offerte", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Incidenza dei costi", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Predisposizione budget", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                new R() { t="Gestione Committente", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Allestimento cantieri", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Tecniche di misurazione", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Tecnica di confezionamento dei c.b. e cementizi", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Clausole Contrattualistiche", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Nuovi Prezzi", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Gestione riserve e contenzioso", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Verifica capitolato e norme di contabilizzazione", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                //new F() { t="Planning Operativo Movimentazione Macchine", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO }, //DA MOD
                new R() { t="Tecniche di esecuzione in presenza di traffico", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },


                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new R() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new R() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Negoziazione", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new R() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Visione d'insieme", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new R() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Efficienza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                #endregion

                #region HR DISCREZIONALI
                
                new R() { t="Assessment", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},
                new R() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},

                #endregion

                #region HR COMPORTAMENTALI

                new R() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new R() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Negoziazione", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new R() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Visione d'insieme", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new R() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Efficienza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                #endregion

            };

            return SalvaRuolo(lista, "Capo Cantiere Manutenzione", "Competitive Advantage");

        }

        public Ruolo SalvaCapoCantiereInfrastrutture()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new R() { t="Normative Tecniche", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative Qualità", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative di Settore", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative Ambientali", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Normative di Sicurezza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Macchinari idonei all'esecuzione", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Movimenti terra", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Lavori in sotterraneo", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Impalcati", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Opere d'arte", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Contabilità Lavori", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                new R() { t="Mercato di riferimento", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Contrattualistica Fornitori", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Contrattualistica Subappaltatori", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Planning breve-medio periodo", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Planning medio-lungo periodo", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Controlling", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Monitoraggio e rilievo dell'opera eseguita", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                //new F() { t="Elaborazione preventivi ed offerte", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO }, //DA MOD
                new R() { t="Incidenza dei costi", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                new R() { t="Gestione committente", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Allestimento cantieri", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Report giornaliero/giornale dei lavori", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Tecniche di esecuzione in presenza di traffico", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new R() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new R() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Negoziazione", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new R() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Visione d'insieme", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new R() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Efficienza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                #endregion

                #region HR DISCREZIONALI
                
                new R() { t="Assessment", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},
                new R() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},

                #endregion

                #region HR COMPORTAMENTALI

                new R() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new R() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Negoziazione", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new R() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Visione d'insieme", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new R() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Efficienza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                #endregion

            };

            return SalvaRuolo(lista, "Capo Cantiere Infrastrutture", "Competitive Advantage");

        }

        public Ruolo SalvaBuyerSeniorSede()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new R() { t="Aspetti Contrattualistici", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Lettura e Interpretazione del Progetto", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Emissione Ordine", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Emissione Contratto", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Sistemi Gestionali", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                new R() { t="Mercato di riferimento", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                //new F() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO }, //DA MOD
                new R() { t="Tecniche di Ricerca Mercato specifiche del settore", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Negoziazione dell'offerta", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Procedure acquisti", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Predisposizione budget", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                //ASSENTI

                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new R() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new R() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Negoziazione", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new R() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Visione d'insieme", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new R() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Efficienza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                #endregion

               #region HR DISCREZIONALI
                
                new R() { t="Assessment", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},
                new R() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},

                #endregion

                #region HR COMPORTAMENTALI

                new R() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="TeamWork", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new R() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Negoziazione", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new R() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Visione d'insieme", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new R() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Efficienza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                #endregion

            };

            return SalvaRuolo(lista, "Buyer Senior Sede", "Competitive Advantage");

        }

        public Ruolo SalvaBuyerSeniorCantiere()
        {
            var lista = new[] 
            {
                #region COMPETENZE TECNICHE

                new R() { t="Aspetti Contrattualistici", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Caratteristiche dei Materiali", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Lettura e Interpretazione del Progetto", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Emissione Ordine", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Emissione Contratto", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Sistemi Gestionali", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                new R() { t="Mercato di riferimento", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                //new F() { t="Planning operativo movimentazione risorse", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_TECNICO }, //DA MOD
                new R() { t="Tecniche di Ricerca Mercato specifiche del settore", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Negoziazione dell'offerta", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Procedure acquisti", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_TECNICO },
                new R() { t="Predisposizione budget", v=Tipologiche.Livello.DISCRETO, mg=Tipologiche.Macrogruppi.MG_TECNICO },

                //ASSENTI

                #endregion

                #region COMPETENZE COMPORTAMENTALI

                new R() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="TeamWork", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new R() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new R() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Visione d'insieme", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                new R() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                new R() { t="Efficienza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE },
                //new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE }, //DA MOD

                #endregion

               #region HR DISCREZIONALI
                
                new R() { t="Assessment", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},
                new R() { t="Considerazioni Gestionali", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},

                #endregion

                #region HR COMPORTAMENTALI

                new R() { t="Integrazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="TeamWork", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Gestione delle Risorse Umane", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD
                //new F() { t="Leadership", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new R() { t="Comunicazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Assertività", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Negoziazione", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Networking", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new R() { t="Capacità di Analisi", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Problem solving", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Visione d'insieme", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Orientamento al cliente", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                new R() { t="Orientamento al risultato", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Responsabilità", v=Tipologiche.Livello.OTTIMO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                new R() { t="Efficienza", v=Tipologiche.Livello.BUONO, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE },
                //new F() { t="Proattività", v=Tipologiche.Livello.SUFFICIENTE, mg=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE }, //DA MOD

                #endregion

            };

            return SalvaRuolo(lista, "Buyer Senior Cantiere", "Competitive Advantage");

        }

        #endregion
    }

}
