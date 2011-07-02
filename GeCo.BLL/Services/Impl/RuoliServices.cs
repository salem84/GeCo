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
        public Ruolo SalvaRuolo(Ruolo r)
        {
            if (r.Id == 0)
                return CreaRuolo(r);
            else
                return AggiornaRuolo(r);
        }

        private Ruolo CreaRuolo(Ruolo r)
        {
            var reposLivelloConoscenza = ServiceLocator.Current.GetInstance<IRepository<LivelloConoscenza>>();
            int idLivelloInsuff = reposLivelloConoscenza.Single(lc => lc.Titolo == Tipologiche.Livello.INSUFFICIENTE).Id;

            Ruolo ruolo = new Ruolo();
            ruolo.Titolo = r.Titolo;
            ruolo.Descrizione = r.Descrizione;

            ruolo.Conoscenze = new List<ConoscenzaCompetenza>();

            //Mi scorro tutte le conoscenze
            foreach (var c in r.Conoscenze)
            {
                //e salvo solo quelle diverse da 0
                if (c.LivelloConoscenzaId != idLivelloInsuff)
                {
                    ConoscenzaCompetenza conoscenza = new ConoscenzaCompetenza();

                    conoscenza.LivelloConoscenzaId = c.LivelloConoscenzaId;
                    conoscenza.CompetenzaId = c.CompetenzaId;
                    ruolo.Conoscenze.Add(conoscenza);
                }
            }

            var repos = ServiceLocator.Current.GetInstance<IRepository<Ruolo>>();
            var uow = ServiceLocator.Current.GetInstance<IUnitOfWork>();

            
            repos.Add(ruolo);
            uow.Commit();

            return ruolo;
        }


        private Ruolo AggiornaRuolo(Ruolo r)
        {
            var reposLivelloConoscenza = ServiceLocator.Current.GetInstance<IRepository<LivelloConoscenza>>();
            var reposRuoli = ServiceLocator.Current.GetInstance<IRepository<Ruolo>>();
            var reposConoscenze = ServiceLocator.Current.GetInstance<IRepository<ConoscenzaCompetenza>>();
            var uow = ServiceLocator.Current.GetInstance<IUnitOfWork>();

            int idLivelloInsuff = reposLivelloConoscenza.Single(lc => lc.Titolo == Tipologiche.Livello.INSUFFICIENTE).Id;

            Ruolo ruolo = new Ruolo();
            ruolo.Titolo = r.Titolo;
            ruolo.Descrizione = r.Descrizione;

            ruolo.Conoscenze = new List<ConoscenzaCompetenza>();

            //Mi scorro tutte le conoscenze
            for (int i = 0; i < r.Conoscenze.Count; i++)
            {
                ConoscenzaCompetenza c = r.Conoscenze.ToList()[i];

                //e salvo solo quelle diverse da 0
                if (c.LivelloConoscenzaId != idLivelloInsuff)
                {
                    ConoscenzaCompetenza conoscenza;
                    conoscenza = ruolo.Conoscenze.SingleOrDefault(con => con.CompetenzaId == c.CompetenzaId);
                    if (conoscenza == null)
                    {
                        conoscenza = new ConoscenzaCompetenza();
                        conoscenza.CompetenzaId = c.CompetenzaId;
                        ruolo.Conoscenze.Add(conoscenza);
                    }

                    conoscenza.LivelloConoscenzaId = c.LivelloConoscenzaId;
                }
                else
                {
                    //E' una di quelle che erano presenti in precedenza e sono state settate a 0 per essere cancellate
                    var conosc = ruolo.Conoscenze.SingleOrDefault(con => con.CompetenzaId == c.CompetenzaId);
                    if (conosc != null)
                    {
                        ruolo.Conoscenze.Remove(conosc);
                        var cc = reposConoscenze.Single(con => con.Id == conosc.Id);
                        reposConoscenze.Delete(cc);
                    }
                }
            }

            reposRuoli.Add(ruolo);
            uow.Commit();

            return ruolo;
        }

        public void EliminaRuolo(int id)
        {
            var repos = ServiceLocator.Current.GetInstance<IRepository<Ruolo>>();
            var uow = ServiceLocator.Current.GetInstance<IUnitOfWork>();

            //repos.Attach(ruoloToRemove);
            Ruolo ruoloToRemove = repos.Single(r => r.Id == id);
            repos.Delete(ruoloToRemove);

            uow.Commit();
        }

        public Ruolo CaricaRuolo(int id)
        {
            var repos = ServiceLocator.Current.GetInstance<IRepository<Ruolo>>();
            var result = repos.Include(r => r.Conoscenze.Select(c => c.Competenza))
                     .Include(r => r.Conoscenze.Select(c => c.LivelloConoscenza))
                     .Include(r => r.Conoscenze.Select(c => c.Competenza.TipologiaCompetenza))
                     .SingleOrDefault(r => r.Id == id);

            return result;
        }

        public IList<Ruolo> GetRuoli(Expression<Func<Ruolo, bool>> where)
        {
            var repos = ServiceLocator.Current.GetInstance<IRepository<Ruolo>>();
            //var lista = repos.Find(where).ToList();
            var lista = repos.AsQueryable().AsExpandable().Where(where).ToList();
            return lista;
        }

        public void SalvaArea(Area area)
        {
            var reposArea = ServiceLocator.Current.GetInstance<IRepository<Area>>();
            reposArea.Add(area);
        }
    }
}
