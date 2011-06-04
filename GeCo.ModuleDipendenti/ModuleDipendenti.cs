using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using GeCo.ModuleDipendenti.Views;

namespace GeCo.ModuleDipendenti
{
    public class ModuleDipendenti : IModule
    {

        public void Initialize()
        {
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            regionManager.RegisterViewWithRegion("TaskButtonRegion", typeof(DipendentiTaskButton));

            var container = ServiceLocator.Current.GetInstance<IUnityContainer>();
            container.RegisterType<Object, DipendentiRibbonTab>("DipendentiRibbonTab");
            container.RegisterType<Object, DipendentiWorkspace>("DipendentiWorkspace");

        }
    }
}
