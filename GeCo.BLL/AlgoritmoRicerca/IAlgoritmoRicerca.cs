using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.Model;

namespace GeCo.BLL.AlgoritmoRicerca
{
    public interface IAlgoritmoRicerca
    {
        List<RisultatoRicerca> Cerca(Anagrafica anagrafica);
    }
}
