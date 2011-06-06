using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.Model;
using GeCo.DAL;
using Microsoft.Practices.ServiceLocation;
using GeCo.Infrastructure;
using System.Linq.Expressions;
using LinqKit;

namespace GeCo.BLL.Services
{
    public class RuoliServices : IRuoliServices
    {
        public FiguraProfessionale SalvaRuolo(FiguraProfessionale r)
        {
            FiguraProfessionale ruolo = new FiguraProfessionale();
            ruolo.Titolo = r.Titolo;
            ruolo.Descrizione = r.Descrizione;

            ruolo.Conoscenze = new List<ConoscenzaCompetenza>();

            //Mi scorro tutte le conoscenze
            foreach (var c in r.Conoscenze)
            {
                //e salvo solo quelle diverse da 0
                if (c.LivelloConoscenza.Titolo != Tipologiche.Livello.INSUFFICIENTE)
                {
                    ConoscenzaCompetenza conoscenza = new ConoscenzaCompetenza();

                    conoscenza.LivelloConoscenzaId = c.LivelloConoscenzaId;
                    conoscenza.CompetenzaId = c.CompetenzaId;
                    ruolo.Conoscenze.Add(conoscenza);
                }
            }

            var repos = ServiceLocator.Current.GetInstance<IRepository<FiguraProfessionale>>();
            var uow = ServiceLocator.Current.GetInstance<IUnitOfWork>();

            if (r.Id != 0)
            {
                repos.Delete(r);
            }

            repos.Add(ruolo);
            uow.Commit();

            return ruolo;
        }

        public void EliminaRuolo(int id)
        {
            FiguraProfessionale ruoloToRemove = new FiguraProfessionale() { Id = id };
            var repos = ServiceLocator.Current.GetInstance<IRepository<FiguraProfessionale>>();
            var uow = ServiceLocator.Current.GetInstance<IUnitOfWork>();

            repos.Attach(ruoloToRemove);
            repos.Delete(ruoloToRemove);

            uow.Commit();
        }

        public FiguraProfessionale CaricaRuolo(int id)
        {
            var repos = ServiceLocator.Current.GetInstance<IRepository<FiguraProfessionale>>();
            var result = repos.Include(r => r.Conoscenze.Select(c => c.Competenza))
                    .Include(r => r.Conoscenze.Select(c => c.LivelloConoscenza))
                    .Include(r => r.Conoscenze.Select(c => c.Competenza.TipologiaCompetenza))
                    .Find(r => r.Id == id);

            if (result.Count() == 1)
                return result.Single();
            else
                return null;
        }

        public IList<FiguraProfessionale> GetRuoli(Expression<Func<FiguraProfessionale, bool>> where)
        {
            var repos = ServiceLocator.Current.GetInstance<IRepository<FiguraProfessionale>>();
            var lista = repos.AsQueryable().AsExpandable().Where(where).ToList();
            return lista;
        }
    }
}
