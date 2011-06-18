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
                if (c.LivelloConoscenza.Titolo != Tipologiche.Livello.INSUFFICIENTE)
                {
                    ConoscenzaCompetenza conoscenza = new ConoscenzaCompetenza();

                    conoscenza.LivelloConoscenzaId = c.LivelloConoscenzaId;
                    conoscenza.CompetenzaId = c.CompetenzaId;
                    dipendente.Conoscenze.Add(conoscenza);
                }
            }

            var repos = ServiceLocator.Current.GetInstance<IRepository<Dipendente>>();
            var uow = ServiceLocator.Current.GetInstance<IUnitOfWork>();

            if (d.Id != 0)
            {
                repos.Delete(d);
            }        

            repos.Add(dipendente);
            uow.Commit();

            return dipendente;
        }

        public void EliminaDipendente(int id)
        {
            Dipendente dipToRemove = new Dipendente() { Id = id };
            var repos = ServiceLocator.Current.GetInstance<IRepository<Dipendente>>();
            var uow = ServiceLocator.Current.GetInstance<IUnitOfWork>();

            repos.Attach(dipToRemove);
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
            return lista;
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
