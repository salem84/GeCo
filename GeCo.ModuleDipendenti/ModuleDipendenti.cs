using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace GeCo.ModuleDipendenti
{
    public class ModuleDipendenti : IModule
    {

        public void Initialize()
        {
            var container = ServiceLocator.Current.GetInstance<IUnityContainer>();
            container.RegisterType<Object, ModuleDipendenti>("ModuleDipendentiRibbonTab");

        }
    }
}
