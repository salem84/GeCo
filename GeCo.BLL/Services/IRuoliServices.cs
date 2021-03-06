﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.Model;
using System.Linq.Expressions;

namespace GeCo.BLL.Services
{
    public interface IRuoliServices
    {
        Ruolo SalvaRuolo(Ruolo ruolo);
        void EliminaRuolo(int id);
        Ruolo CaricaRuolo(int id);
        
        IList<Ruolo> GetRuoli();
        IList<Ruolo> GetRuoli(Expression<Func<Ruolo, bool>> where);

        //Utilizzato nel converter, evita di fare il lazy loading
        Ruolo GetRuolo(int id);

        void SalvaArea(Area area);
    }
}
