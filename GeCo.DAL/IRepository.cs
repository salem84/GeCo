﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace GeCo.DAL
{
    public interface IRepository<T> where T : class
    {
        /*T Load(int key, bool includeChildren);
        void Save(T instance);
        void Delete(int key);
        IQueryable<T> Query(bool includeChildren);*/

        IQueryable<T> AsQueryable();
        IList<T> GetAll();
        IList<T> Find(Expression<Func<T, bool>> where);
        T Single(Expression<Func<T, bool>> where);
        T SingleOrDefault(Expression<Func<T, bool>> where);
        T First(Expression<Func<T, bool>> where);
        T FirstOrDefault(Expression<Func<T, bool>> where);
        void Delete(T entity);
        void Add(T entity);
        void Attach(T entity);

        IRepository<T> Include(string path);
        IQueryable<T> Include(Expression<Func<T, object>> path);
    }

}
