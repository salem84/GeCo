using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data;

namespace GeCo.DAL
{
    public interface IDbContext : IDisposable
    {
        DbSet<T> CreateDbSet<T>() where T : class;
        void SaveChanges();
        void ChangeObjectState<T>(T entity, EntityState entityState) where T : class;

    }
}
