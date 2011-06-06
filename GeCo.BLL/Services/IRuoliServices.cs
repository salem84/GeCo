using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.Model;
using System.Linq.Expressions;

namespace GeCo.BLL.Services
{
    public interface IRuoliServices
    {
        FiguraProfessionale SalvaRuolo(FiguraProfessionale ruolo);
        void EliminaRuolo(int id);
        FiguraProfessionale CaricaRuolo(int id);

        IList<FiguraProfessionale> GetRuoli(Expression<Func<FiguraProfessionale, bool>> where);
    }
}
