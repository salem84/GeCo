using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.Model;

namespace GeCo.BLL.Services
{
    public interface IDipendentiServices
    {
        void SalvaDipendente(Dipendente dipendente);
        void EliminaDipendente(Dipendente dipendente);
        IQueryable GetDipendenti();
    }
}
