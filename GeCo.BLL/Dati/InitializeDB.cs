using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.Model;
using GeCo.Infrastructure;
using GeCo.DAL;
using GeCo.BLL.Dati;
using Microsoft.Practices.ServiceLocation;

namespace GeCo.BLL
{
    public class InitializeDB
    {
        public static void InitalizeAll()
        {
            InitializeDB.InsertParametriDefault();
            InitializeDB.InsertTipologieCompetenze();
            InitializeDB.InsertCompetenzeTecniche();
            InitializeDB.InsertCompetenzeComportamentali();
            InitializeDB.InsertCompetenzeHrDiscrezionali();
            InitializeDB.InsertCompetenzeHrComportamentali();
            InitializeDB.InsertAltro();
            
            //Key Roles Strategic Support
            RuoliDefault.SalvaResponsabileUfficioTecnico();
            RuoliDefault.SalvaResponsabileImpiantiMobiliMacchineImpianti();
            RuoliDefault.SalvaResponsabileControlliLaboratorio();
            RuoliDefault.SalvaCostController();
            RuoliDefault.SalvaContabilizzatoreSenior();

            //Key Roles Competitive Advantage
            RuoliDefault.SalvaResponsabileUfficioAcquisti();
            RuoliDefault.SalvaDirettoreCantiereManutenzione();
            RuoliDefault.SalvaDirettoreCantiereInfrastrutture();
            RuoliDefault.SalvaCapoCantiereManutenzione();
            RuoliDefault.SalvaCapoCantiereInfrastrutture();
            RuoliDefault.SalvaBuyerSeniorSede();
            RuoliDefault.SalvaBuyerSeniorCantiere();

            DipendentiDefault.SalvaDipendente1();
        }

        private static void InsertParametriDefault()
        {
            var lista = new[]
                {
                    new { n=Tipologiche.Parametro.PMAX_HR_DISCREZIONALI, v=20},
                    new { n=Tipologiche.Parametro.PMAX_HR_COMPORTAMENTALI, v=30},
                    new { n=Tipologiche.Parametro.PMAX_COMPORTAMENTALI, v=20},
                    new { n=Tipologiche.Parametro.PMAX_TECN_STRATEGIC, v=12}, //40% di 30
                    new { n=Tipologiche.Parametro.PMAX_TECN_COMPETITIVE, v=18}, //60% di 30
                    new { n=Tipologiche.Parametro.PERCENTUALE_SOGLIA_FOUNDATIONAL, v=70},
                };

            var repos = ServiceLocator.Current.GetInstance<IRepository<Parametro>>();
            var uow = ServiceLocator.Current.GetInstance<IUnitOfWork>();


            foreach (var elemento in lista)
            {
                repos.Add(new Parametro()
                {
                    Nome = elemento.n,
                    Valore = elemento.v.ToString()
                });
            }

            uow.Commit();

        }


        public static void InsertTipologieCompetenze()
        {
            var lista = new[]
                {
                    //Tecniche
                    new { t=Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL, m=Tipologiche.Macrogruppi.MG_TECNICO},
                    new { t=Tipologiche.TipologiaCompetenza.T_STRATEGIC_SUPPORT, m=Tipologiche.Macrogruppi.MG_TECNICO},
                    new { t=Tipologiche.TipologiaCompetenza.T_COMPETITIVE_ADVANTAGE, m=Tipologiche.Macrogruppi.MG_TECNICO},

                    //Comportamentali
                    new { t=Tipologiche.TipologiaCompetenza.C_MANAGERIALI, m=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE},
                    new { t=Tipologiche.TipologiaCompetenza.C_RELAZIONALI, m=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE},
                    new { t=Tipologiche.TipologiaCompetenza.C_COGNITIVE, m=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE},
                    new { t=Tipologiche.TipologiaCompetenza.C_REALIZZATIVE, m=Tipologiche.Macrogruppi.MG_COMPORTAMENTALE},

                    //HR Discrezionali
                    new { t=Tipologiche.TipologiaCompetenza.HR_DISCREZIONALI, m=Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE},

                    //HR Comportamentali
                    new { t=Tipologiche.TipologiaCompetenza.HR_C_MANAGERIALI, m=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE},
                    new { t=Tipologiche.TipologiaCompetenza.HR_C_RELAZIONALI, m=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE},
                    new { t=Tipologiche.TipologiaCompetenza.HR_C_COGNITIVE, m=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE},
                    new { t=Tipologiche.TipologiaCompetenza.HR_C_REALIZZATIVE, m=Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE}
                };

            var repos = ServiceLocator.Current.GetInstance<IRepository<TipologiaCompetenza>>();
            var uow = ServiceLocator.Current.GetInstance<IUnitOfWork>();



            foreach (var elemento in lista)
            {
                repos.Add(new TipologiaCompetenza()
                {
                    Titolo = elemento.t,
                    MacroGruppo = elemento.m
                });
            }

            uow.Commit();

        }

        public static void InsertCompetenzeTecniche()
        {
            var lista = new[] 
            {
                new { t="Normative Tecniche", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL},
                new { t="Normative Qualità", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL},
                new { t="Normative di Settore", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL},
                new { t="Normative Ambientali", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL},
                new { t="Normative di Sicurezza", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL},
                new { t="Caratteristiche dei Materiali", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL},
                new { t="Contabilità Lavori", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL},
                new { t="Lettura e Interpretazione del Progetto", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL},
                new { t="Opere d'arte", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL},
                new { t="Movimenti terra", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL},
                new { t="Aspetti Contrattualistici", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL},
                new { t="Normativa Giuslavoristica e Contratti di Lavoro", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL},
                new { t="Leggi macchine speciali", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL},
                new { t="Composizione budget macchine", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL},
                new { t="Predisposizione budget macchine speciali", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL},
                new { t="Processo realizzazione lavori speciali", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL},
                new { t="Leggi macchine e codice stradale", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL},
                new { t="Aspetti tecnici macchine speciali", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL},
                new { t="Emissione Ordine", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL},
                new { t="Emissione Contratto", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL},
                new { t="Sistemi Gestionali", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL},
                new { t="Macchinari idonei all'esecuzione", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL},
                new { t="Lavori in sotterraneo", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL},
                new { t="Impalcati", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL},
                                               
                new { t="Planning breve-medio periodo", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_STRATEGIC_SUPPORT},
                new { t="Planning medio-lungo periodo", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_STRATEGIC_SUPPORT},
                new { t="Planning operativo movimentazione risorse", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_STRATEGIC_SUPPORT},
                new { t="Controlling", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_STRATEGIC_SUPPORT},
                new { t="Monitoraggio e rilievo dell'opera eseguita", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_STRATEGIC_SUPPORT},
                new { t="Elaborazione preventivi ed offerte", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_STRATEGIC_SUPPORT},
                new { t="Incidenza dei costi", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_STRATEGIC_SUPPORT},
                new { t="Mercato di riferimento", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_STRATEGIC_SUPPORT},
                new { t="Tecniche di Ricerca Mercato specifiche del settore", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_STRATEGIC_SUPPORT},
                new { t="Negoziazione dell'offerta", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_STRATEGIC_SUPPORT},
                new { t="Procedure acquisti", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_STRATEGIC_SUPPORT},
                new { t="Predisposizione budget", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_STRATEGIC_SUPPORT},
                new { t="Contrattualistica Fornitori", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_STRATEGIC_SUPPORT},
                new { t="Contrattualistica Subappaltatori", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_STRATEGIC_SUPPORT},

                new { t="Analisi scostamenti", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_COMPETITIVE_ADVANTAGE},
                new { t="Standard di budgeting", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_COMPETITIVE_ADVANTAGE},
                new { t="Gestione committente", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_COMPETITIVE_ADVANTAGE},
                new { t="Tecniche di misurazione", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_COMPETITIVE_ADVANTAGE},
                new { t="Tecnica di confezionamento dei c.b. e cementizi", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_COMPETITIVE_ADVANTAGE},
                new { t="Tecniche di esecuzione in presenza di traffico", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_COMPETITIVE_ADVANTAGE},
                new { t="Clausole Contrattualistiche", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_COMPETITIVE_ADVANTAGE},
                new { t="Negoziazione Offerta Macchine Speciali/Modifiche", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_COMPETITIVE_ADVANTAGE},
                new { t="Planning Operativo Movimentazione Macchine", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_COMPETITIVE_ADVANTAGE},
                new { t="Nuovi Prezzi", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_COMPETITIVE_ADVANTAGE},
                new { t="Allestimento Cantieri", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_COMPETITIVE_ADVANTAGE},
                new { t="Report giornaliero/giornale dei lavori", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_COMPETITIVE_ADVANTAGE},
                new { t="Gestione riserve e contenzioso", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_COMPETITIVE_ADVANTAGE},
                new { t="Verifica capitolato e norme di contabilizzazione", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.T_COMPETITIVE_ADVANTAGE},



            };

            var reposComp = ServiceLocator.Current.GetInstance<IRepository<Competenza>>();
            var reposTipologie = ServiceLocator.Current.GetInstance<IRepository<TipologiaCompetenza>>();
            var uow = ServiceLocator.Current.GetInstance<IUnitOfWork>();

            foreach (var elemento in lista)
            {
                reposComp.Add(new Competenza()
                {
                    Titolo = elemento.t,
                    Descrizione = elemento.d,
                    Peso = elemento.p,
                    TipologiaCompetenza = reposTipologie.Single(t => t.Titolo == elemento.tipo && t.MacroGruppo == Tipologiche.Macrogruppi.MG_TECNICO)
                });
            }

            uow.Commit();

        }

        public static void InsertCompetenzeComportamentali()
        {

            var lista = new[] 
            {
                new { t="Integrazione", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.C_MANAGERIALI},
                new { t="TeamWork", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.C_MANAGERIALI},
                new { t="Gestione delle Risorse Umane", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.C_MANAGERIALI},
                new { t="Leadership", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.C_MANAGERIALI},

                new { t="Comunicazione", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.C_RELAZIONALI},
                new { t="Assertività", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.C_RELAZIONALI},
                new { t="Negoziazione", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.C_RELAZIONALI},
                new { t="Networking", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.C_RELAZIONALI},

                new { t="Capacità di Analisi", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.C_COGNITIVE},
                new { t="Problem solving", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.C_COGNITIVE},
                new { t="Visione d'insieme", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.C_COGNITIVE},
                new { t="Orientamento al cliente", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.C_COGNITIVE},

                new { t="Orientamento al risultato", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.C_REALIZZATIVE},
                new { t="Responsabilità", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.C_REALIZZATIVE},
                new { t="Efficienza", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.C_REALIZZATIVE},
                new { t="Proattività", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.C_REALIZZATIVE},
                
            };


            var reposComp = ServiceLocator.Current.GetInstance<IRepository<Competenza>>();
            var reposTipologie = ServiceLocator.Current.GetInstance<IRepository<TipologiaCompetenza>>();
            var uow = ServiceLocator.Current.GetInstance<IUnitOfWork>();

            foreach (var elemento in lista)
            {
                reposComp.Add(new Competenza()
                {
                    Titolo = elemento.t,
                    Descrizione = elemento.d,
                    Peso = elemento.p,
                    TipologiaCompetenza = reposTipologie.Single(t => t.Titolo == elemento.tipo && t.MacroGruppo == Tipologiche.Macrogruppi.MG_COMPORTAMENTALE)
                });
            }

            uow.Commit();
        }

        public static void InsertCompetenzeHrDiscrezionali()
        {

            var lista = new[] 
            {
                new { t="Assessment", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.HR_DISCREZIONALI},
                new { t="Considerazioni Gestionali", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.HR_DISCREZIONALI}                
            };

            var reposComp = ServiceLocator.Current.GetInstance<IRepository<Competenza>>();
            var reposTipologie = ServiceLocator.Current.GetInstance<IRepository<TipologiaCompetenza>>();
            var uow = ServiceLocator.Current.GetInstance<IUnitOfWork>();

            foreach (var elemento in lista)
            {
                reposComp.Add(new Competenza()
                {
                    Titolo = elemento.t,
                    Descrizione = elemento.d,
                    Peso = elemento.p,
                    TipologiaCompetenza = reposTipologie.Single(t => t.Titolo == elemento.tipo && t.MacroGruppo == Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE)
                });
            }

            uow.Commit();
        }

        public static void InsertCompetenzeHrComportamentali()
        {

            var lista = new[] 
            {
                new { t="Integrazione", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.HR_C_MANAGERIALI},
                new { t="TeamWork", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.HR_C_MANAGERIALI},
                new { t="Gestione delle Risorse Umane", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.HR_C_MANAGERIALI},
                new { t="Leadership", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.HR_C_MANAGERIALI},

                new { t="Comunicazione", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.HR_C_RELAZIONALI},
                new { t="Assertività", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.HR_C_RELAZIONALI},
                new { t="Negoziazione", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.HR_C_RELAZIONALI},
                new { t="Networking", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.HR_C_RELAZIONALI},

                new { t="Capacità di Analisi", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.HR_C_COGNITIVE},
                new { t="Problem solving", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.HR_C_COGNITIVE},
                new { t="Visione d'insieme", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.HR_C_COGNITIVE},
                new { t="Orientamento al cliente", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.HR_C_COGNITIVE},

                new { t="Orientamento al risultato", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.HR_C_REALIZZATIVE},
                new { t="Responsabilità", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.HR_C_REALIZZATIVE},
                new { t="Efficienza", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.HR_C_REALIZZATIVE},
                new { t="Proattività", d="", p=1, tipo=Tipologiche.TipologiaCompetenza.HR_C_REALIZZATIVE},
                
            };


            var reposComp = ServiceLocator.Current.GetInstance<IRepository<Competenza>>();
            var reposTipologie = ServiceLocator.Current.GetInstance<IRepository<TipologiaCompetenza>>();
            var uow = ServiceLocator.Current.GetInstance<IUnitOfWork>();

            foreach (var elemento in lista)
            {
                reposComp.Add(new Competenza()
                {
                    Titolo = elemento.t,
                    Descrizione = elemento.d,
                    Peso = elemento.p,
                    TipologiaCompetenza = reposTipologie.Single(t => t.Titolo == elemento.tipo && t.MacroGruppo == Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE)
                });
            }

            uow.Commit();
        }



        public static void InsertAltro()
        {
            //var reposAree = ServiceLocator.Current.GetInstance<IRepository<Area>>();
            var reposLivelli = ServiceLocator.Current.GetInstance<IRepository<LivelloConoscenza>>();
            var uow = ServiceLocator.Current.GetInstance<IUnitOfWork>();


            //reposAree.Add(new Area() { Nome = "Area1" });

            reposLivelli.Add(new LivelloConoscenza()
            {
                Titolo = Tipologiche.Livello.INSUFFICIENTE,
                Valore = 0
            });
            reposLivelli.Add(new LivelloConoscenza()
            {
                Titolo = Tipologiche.Livello.SUFFICIENTE,
                Valore = 1
            });
            reposLivelli.Add(new LivelloConoscenza()
            {
                Titolo = Tipologiche.Livello.DISCRETO,
                Valore = 2
            });
            reposLivelli.Add(new LivelloConoscenza()
            {
                Titolo = Tipologiche.Livello.BUONO,
                Valore = 3
            });
            reposLivelli.Add(new LivelloConoscenza()
            {
                Titolo = Tipologiche.Livello.OTTIMO,
                Valore = 4
            });


            uow.Commit();

        }




    }
}
