using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace GeCo.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        /*T Load(int key, bool includeChildren);
        void Save(T instance);
        void Delete(int key);
        IQueryable<T> Query(bool includeChildren);*/

        IQueryable<T> AsQueryable();
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> where);
        T Single(Expression<Func<T, bool>> where);
        T First(Expression<Func<T, bool>> where);
        void Delete(T entity);
        void Add(T entity);
        void Attach(T entity);

        IRepository<T> Include(Expression<Func<T, object>> path);
    }

}
