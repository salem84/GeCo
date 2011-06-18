﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.BLL.AlgoritmoRicerca;
using GeCo.Model;
using Microsoft.Practices.Unity;

namespace GeCo.BLL.Services
{
    public class RicercaServices : IRicercaServices
    {
        private IUnityContainer _container;
        public RicercaServices(IUnityContainer container)
        {
            _container = container;
        }

        public List<RisultatoRicerca> CercaRuoloDaDipendente(Dipendente dipendente)
        {
            var algoritmo = _container.Resolve<IAlgoritmoRicerca>("RicercaRuoloDaDipendente");
            var result = algoritmo.Cerca(dipendente);

            return result;
        }

        public List<RisultatoRicerca> CercaDipendenteDaRuolo(Ruolo ruolo)
        {
            var algoritmo = _container.Resolve<IAlgoritmoRicerca>("RicercaDipendenteDaRuolo");
            var result = algoritmo.Cerca(ruolo);

            return result;
        }
    }
}
