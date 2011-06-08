using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.BLL.AlgoritmoRicerca;
using GeCo.Model;

namespace GeCo.BLL.Services
{
    public interface IRicercaServices
    {
        List<RisultatoRicerca> CercaRuoloDaDipendente(Dipendente dipendente);
        List<RisultatoRicerca> CercaDipendenteDaRuolo(FiguraProfessionale ruolo);
    }
}
