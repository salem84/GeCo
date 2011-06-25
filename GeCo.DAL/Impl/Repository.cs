using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data;
using System.Linq.Expressions;
using GeCo.Infrastructure;
using System.Data.Objects;

namespace GeCo.DAL
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        DbSet<T> _dbSet;
        IDbContextAdapter _dbContext;

        public BaseRepository(IDbContextAdapter dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.CreateDbSet<T>();
        }

        public IQueryable<T> AsQueryable()
        {
            return _dbSet;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where);
        }

        public T Single(Expression<Func<T, bool>> where)
        {
            return _dbSet.Single(where);
        }

        public T SingleOrDefault(Expression<Func<T, bool>> where)
        {
            return _dbSet.SingleOrDefault(where);
        }

        public T First(Expression<Func<T, bool>> where)
        {
            return _dbSet.First(where);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> where)
        {
            return _dbSet.FirstOrDefault(where);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _dbContext.ChangeObjectState(entity, System.Data.EntityState.Deleted);
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.ChangeObjectState(entity, System.Data.EntityState.Added);
        }
        public void Attach(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.ChangeObjectState(entity, System.Data.EntityState.Modified);
        }

        public IRepository<T> Include(string path)
        {
            _dbSet.Include(path);
            return this;
        }

        public IQueryable<T> Include(Expression<Func<T,object>> path)
        {
            return _dbSet.Include(path);
        }

    }
    public static class Extension
    {
        public static IQueryable<T> Include<T>(this IQueryable<T> obj, Expression<Func<T, object>> path)
        {
            if (obj is ObjectQuery<T>)
                (obj as ObjectQuery<T>).Include(path);

            return obj;
        }
    }

    /*public abstract class RepositoryBase<T> : IRepository<T> where T : BaseIdentityModelCore
    {
        protected abstract DbSet<T> GetDbSet();

        protected abstract DbQuery<T> GetDbQuery();

        public PavimentalContext Context { get; private set; }

        protected RepositoryBase(UnitOfWork workUnit)
        {
            Context = workUnit.Context as PavimentalContext;
        }

        public virtual IQueryable<T> Query(bool includeChildren)
        {
            return includeChildren ? GetDbQuery() : GetDbSet().AsQueryable();
        }

        private void _SetState<TBaseIdentity>(TBaseIdentity model) where TBaseIdentity : BaseIdentityModelCore
        {
            Context.Entry(model).State = model.Id > 0 ? EntityState.Modified : EntityState.Added;
        }

        public virtual T Load(int key, bool includeChildren)
        {
            return (from instance in Query(includeChildren) where instance.Id.Equals(key) select instance).FirstOrDefault();
        }

        public virtual void Save(T instance)
        {
            _SetState(instance);
        }

        public virtual void Delete(int key)
        {
            if (GetDbSet().Where(t => t.Id.Equals(key)).Any())
            {
                return;
            }
            GetDbSet().Remove(Load(key, false));
        }
    }*/

}
