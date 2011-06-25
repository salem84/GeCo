using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeCo.BLL.Services
{
    public interface IDbAdminServices
    {
        void VerificaDb();
        //void CreaDb();
        //void EliminaDb();
        void InizializzaDb();

        //string GetDataDirectory();
        //void SetupDataDirectory();
    }
}
