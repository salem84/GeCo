using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data;
using System.Data.Objects;
using System.Data.Entity.Infrastructure;

namespace GeCo.DAL
{
    public class DbContextAdapter : IDbContext
    {
        readonly DbContext _dbContext;

        public DbContextAdapter(DbContext context)
        {
            _dbContext = context;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public DbSet<T> CreateDbSet<T>() where T : class
        {
            return _dbContext.Set<T>();
        }
        
        public void ChangeObjectState<T>(T entity, EntityState entityState) where T : class
        {
            objectContext.ObjectStateManager.ChangeObjectState(entity, entityState);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        protected ObjectContext objectContext
        {
            get { return ((IObjectContextAdapter)_dbContext).ObjectContext; }
        }
    }
}
