using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.Model;
using System.Linq.Expressions;

namespace GeCo.BLL.Services
{
    public interface IDipendentiServices
    {
        Dipendente SalvaDipendente(Dipendente dipendente);
        void EliminaDipendente(int id);
        Dipendente CaricaDipendente(int id);
        IQueryable GetDipendenti();
        IList<Dipendente> GetDipendenti(Expression<Func<Dipendente, bool>> where);

        
        
        
                
    }
}
