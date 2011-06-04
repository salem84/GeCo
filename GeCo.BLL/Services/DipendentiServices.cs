using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.DAL;
using GeCo.Model;
using GeCo.Infrastructure;

namespace GeCo.BLL.Services
{
    public class DipendentiServices : IDipendentiServices
    {
        /// <summary>
        /// Devo rielaborare l'oggetto per evitare creazione di duplicati di conoscenze su DB
        /// </summary>
        /// <param name="dipendente"></param>
        public void SalvaDipendente(Dipendente dipendente)
        {
            
        }

        public void EliminaDipendente(Dipendente dipendente)
        {
            throw new NotImplementedException();
        }

        public IQueryable GetDipendenti()
        {
            IRepository<Dipendente> repository = new BaseRepository<Dipendente>(null);
            return repository.AsQueryable();
        }
    }
}
