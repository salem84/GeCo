using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using GeCo.DAL.Dati;

namespace GeCo.DAL
{
    public class PavimentalInitializer : DropCreateDatabaseIfModelChanges<PavimentalContext>
    {
        protected override void Seed(PavimentalContext context)
        {
            base.Seed(context);
            
            DbContextAdapter adapter = new DbContextAdapter(context);
            InitializeDB initializer = new InitializeDB(adapter);
            initializer.InitalizeAll();
            
        }
    }
}
