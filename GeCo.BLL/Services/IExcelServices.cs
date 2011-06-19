using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeCo.BLL.Services
{
    public interface IExcelServices
    {
        void EsportaExcel(string nomeRuolo, List<ConfrontoConoscenzaCompetenza> confronti);
    }
}
