using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeCo.BLL.Services
{
    public interface IDbAdmin
    {
        void VerificaDb();
        void CreaDb();
        void EliminaDb();
        void InizializzaDb();
    }
}
