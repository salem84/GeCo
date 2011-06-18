using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.Infrastructure;

namespace GeCo.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _dbContext;
        
        public UnitOfWork(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

    }
}
