using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeCo.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
        //void Complete();
    }
}
