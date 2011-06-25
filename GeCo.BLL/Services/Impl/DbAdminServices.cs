using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using GeCo.Infrastructure;
using GeCo.DAL;
using System.IO;

namespace GeCo.BLL.Services
{
    public class DbAdminServices : IDbAdminServices
    {

        private IDbContextAdapter dbContext;

        public DbAdminServices(IDbContextAdapter dbContext)
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
    }
}
