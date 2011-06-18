using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.Model;
using Microsoft.Practices.ServiceLocation;
using GeCo.DAL;

namespace GeCo.BLL.Services
{
    public class CompetenzeServices : ICompetenzeServices
    {
        public IQueryable<LivelloConoscenza> GetLivelliConoscenza()
        {
            var repos = ServiceLocator.Current.GetInstance<IRepository<LivelloConoscenza>>();
            return repos.AsQueryable();
        }

        public IList<Competenza> GetCompetenze()
        {
            var repos = ServiceLocator.Current.GetInstance<IRepository<Competenza>>();
            var result = repos.Include(c => c.TipologiaCompetenza).ToList();

            return result;
        }


        public bool SalvaCompetenza(Competenza c)
        {
            try
            {
                var repos = ServiceLocator.Current.GetInstance<IRepository<Competenza>>();
                var uow = ServiceLocator.Current.GetInstance<IUnitOfWork>();

                if (c.Id == 0)
                {
                    repos.Add(c);
                }
                else
                {
                    repos.Attach(c);
                    /*var competenza = context.Competenze.Find(CompetenzaSelezionata.Id);
                    competenza.Titolo = CompetenzaSelezionata.Titolo;
                    competenza.Descrizione = CompetenzaSelezionata.Descrizione;
                    competenza.TipologiaCompetenzaId = CompetenzaSelezionata.TipologiaCompetenza.Id;*/
                }

                uow.Commit();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IList<TipologiaCompetenza> GetTipologieCompetenze()
        {
            var repos = ServiceLocator.Current.GetInstance<IRepository<TipologiaCompetenza>>();

            return repos.GetAll().ToList();
        }
    }
}
