using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.Model;

namespace GeCo.BLL.Services
{
    public interface IAdminServices
    {
        void VerificaDb();
        //void CreaDb();
        //void EliminaDb();
        void InizializzaDb();

        //string GetDataDirectory();
        //void SetupDataDirectory();

        IList<Parametro> GetParametri();
        bool SalvaParametro(Parametro parametro);
    }
}
