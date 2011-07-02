using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.DAL;
using GeCo.Model;
using GeCo.Infrastructure;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System.Linq.Expressions;
using LinqKit;

namespace GeCo.BLL.Services
{
    public class DipendentiServices : IDipendentiServices
    {
        /// <summary>
        /// Devo rielaborare l'oggetto per evitare creazione di duplicati di conoscenze su DB
        /// </summary>
        /// <param name="dipendente"></param>
        public Dipendente SalvaDipendente(Dipendente d)
        {
            if (d.Id == 0)
                return CreaDipendente(d);
            else
                return AggiornaDipendente(d);
        }


        private Dipendente AggiornaDipendente(Dipendente d)
        {
            var reposLivelloConoscenza = ServiceLocator.Current.GetInstance<IRepository<LivelloConoscenza>>();
            int idLivelloInsuff = reposLivelloConoscenza.Single(lc => lc.Titolo == Tipologiche.Livello.INSUFFICIENTE).Id;

            var reposDipendente = ServiceLocator.Current.GetInstance<IRepository<Dipendente>>();
            var reposConoscenze = ServiceLocator.Current.GetInstance<IRepository<ConoscenzaCompetenza>>();
            var uow = ServiceLocator.Current.GetInstance<IUnitOfWork>();

            Dipendente dipendente = reposDipendente.Single(dip => dip.Id == d.Id);

            dipendente.Matricola = d.Matricola;
            dipendente.Cognome = d.Cognome;
            dipendente.Nome = d.Nome;
            dipendente.DataNascita = d.DataNascita;

            for (int i = 0; i < d.Conoscenze.Count; i++ )
            {
                ConoscenzaCompetenza c = d.Conoscenze.ToList()[i];

                //e salvo solo quelle diverse da 0
                if (c.LivelloConoscenzaId != idLivelloInsuff)
                {
                    ConoscenzaCompetenza conoscenza;
                    conoscenza = dipendente.Conoscenze.SingleOrDefault(con => con.CompetenzaId == c.CompetenzaId);
                    if (conoscenza == null)
                    {
                        conoscenza = new ConoscenzaCompetenza();
                        conoscenza.CompetenzaId = c.CompetenzaId;
                        dipendente.Conoscenze.Add(conoscenza);
                    }

                    conoscenza.LivelloConoscenzaId = c.LivelloConoscenzaId;
                }
                else
                {
                    //E' una di quelle che erano presenti in precedenza e sono state settate a 0 per essere cancellate
                    var conosc = dipendente.Conoscenze.SingleOrDefault(con => con.CompetenzaId == c.CompetenzaId);
                    if (conosc != null)
                    {
                        dipendente.Conoscenze.Remove(conosc);
                        var cc = reposConoscenze.Single(con => con.Id == conosc.Id);
                        reposConoscenze.Delete(cc);
                    }
                }
            }

            uow.Commit();

            return dipendente;
        }

        private Dipendente CreaDipendente(Dipendente d)
        {
            var reposLivelloConoscenza = ServiceLocator.Current.GetInstance<IRepository<LivelloConoscenza>>();
            int idLivelloInsuff = reposLivelloConoscenza.Single(lc => lc.Titolo == Tipologiche.Livello.INSUFFICIENTE).Id;

            //Controllo se l'id è uguale a 0
            //Ricreo l'oggetto
                        
            Dipendente dipendente = new Dipendente();
            dipendente.Matricola = d.Matricola;
            dipendente.Cognome = d.Cognome;
            dipendente.Nome = d.Nome;
            dipendente.DataNascita = d.DataNascita;
            
            dipendente.Conoscenze = new List<ConoscenzaCompetenza>();

            //Mi scorro tutte le conoscenze
            foreach (var c in d.Conoscenze)
            {
                //e salvo solo quelle diverse da 0
                //if (c.LivelloConoscenza.Titolo != Tipologiche.Livello.INSUFFICIENTE)
                if (c.LivelloConoscenzaId != idLivelloInsuff)
                {
                    ConoscenzaCompetenza conoscenza = new ConoscenzaCompetenza();

                    conoscenza.LivelloConoscenzaId = c.LivelloConoscenzaId;
                    conoscenza.CompetenzaId = c.CompetenzaId;
                    dipendente.Conoscenze.Add(conoscenza);
                }
            }

            var repos = ServiceLocator.Current.GetInstance<IRepository<Dipendente>>();
            var uow = ServiceLocator.Current.GetInstance<IUnitOfWork>();
            
            repos.Add(dipendente);
            uow.Commit();

            return dipendente;
        }

        public void EliminaDipendente(int id)
        {
            var repos = ServiceLocator.Current.GetInstance<IRepository<Dipendente>>();
            var uow = ServiceLocator.Current.GetInstance<IUnitOfWork>();

            //repos.Attach(dipToRemove);
            Dipendente dipToRemove = repos.Single(d => d.Id == id);
            repos.Delete(dipToRemove);

            uow.Commit();
        }

        public IQueryable GetDipendenti()
        {
            IRepository<Dipendente> repository = new BaseRepository<Dipendente>(null);
            return repository.AsQueryable();
        }

        public IList<Dipendente> GetDipendenti(Expression<Func<Dipendente, bool>> where)
        {
            var repos = ServiceLocator.Current.GetInstance<IRepository<Dipendente>>();
            var lista = repos.AsQueryable().AsExpandable().Where(where).ToList();
            //var lista = repos.GetAll().Where(where.Compile());
            
            return lista.ToList();
        }


        public Dipendente CaricaDipendente(int id)
        {
            //TODO attenzione ritorna proxy
            var repos = ServiceLocator.Current.GetInstance<IRepository<Dipendente>>();
            /*var result = repos.Include(a => a.Conoscenze.Select(c => c.Competenza))
                    .Include(a => a.Conoscenze.Select(c => c.LivelloConoscenza))
                    .Include(a => a.Conoscenze.Select(c => c.Competenza.TipologiaCompetenza))
                    .SingleOrDefault(a => a.Id == id);*/
            var result = repos.SingleOrDefault(a => a.Id == id);

            return result;
        }

        
    }
}
