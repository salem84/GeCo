using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using GeCo.Infrastructure;
using GeCo.DAL;
using System.IO;
using GeCo.Model;

namespace GeCo.BLL.Services
{
    public class AdminServices : IAdminServices
    {

        private IDbContextAdapter dbContext;

        public AdminServices(IDbContextAdapter dbContext)
        {
            this.dbContext = dbContext;
        }

        public void VerificaDb()
        {
            //string dataDirectory = AppDomain.CurrentDomain.GetData("DataDirectory") as string;

            //Se esiste verifico che sia compatibile
            if (dbContext.Database.Exists())
            {
                bool compatibile = dbContext.Database.CompatibleWithModel(false);
                if (!compatibile)
                {
                    dbContext.Database.Delete();
                    dbContext.Database.Create();
                    InitializeDB.InitalizeAll();
                }
            }
            else
            {
                dbContext.Database.Create();
                InitializeDB.InitalizeAll();
            }

            //Faccio questa query per ottimizzare le successive
            //var temp = dbContext.ConoscenzaCompetenze.ToList();
        }  
       

        public void InizializzaDb()
        {
            dbContext.Database.Delete();
            dbContext.Database.Create();
            InitializeDB.InitalizeAll();
        }

        public IList<Parametro> GetParametri()
        {
            var reposParametri = ServiceLocator.Current.GetInstance<IRepository<Parametro>>();

            return reposParametri.GetAll().ToList();
        }

        public bool SalvaParametro(Parametro parametro)
        {
            try
            {
                var reposParametri = ServiceLocator.Current.GetInstance<IRepository<Parametro>>();
                var uow = ServiceLocator.Current.GetInstance<IUnitOfWork>();

                reposParametri.Attach(parametro);

                uow.Commit();

                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
