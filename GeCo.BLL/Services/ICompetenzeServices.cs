using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.Model;

namespace GeCo.BLL.Services
{
    public interface ICompetenzeServices
    {
        //Gestione competenze
        IQueryable<LivelloConoscenza> GetLivelliConoscenza();
        //LivelloConoscenza GetLivelloConoscenza(string titolo);
        IList<Competenza> GetCompetenze();
        bool SalvaCompetenza(Competenza c);

        IList<TipologiaCompetenza> GetTipologieCompetenze();

        
    }
}
