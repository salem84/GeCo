using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using GeCo.Infrastructure;
using GeCo.DAL;

namespace GeCo.BLL.Services
{
    public class DbAdmin : IDbAdmin
    {
        
        public void VerificaDb()
        {
            throw new NotImplementedException();
        }

        public void CreaDb()
        {
            
        }

        public void EliminaDb()
        {
            throw new NotImplementedException();
        }


        public void InizializzaDb()
        {
            var context = ServiceLocator.Current.GetInstance<IDbContext>();
            context.DeleteDb();
            context.CreateDb();
            InitializeDB.InitalizeAll();
        }
    }
}
