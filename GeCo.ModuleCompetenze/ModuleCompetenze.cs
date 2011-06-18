using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using GeCo.ModuleCompetenze.Views;
using GeCo.ModuleCompetenze.ViewModels;
using GeCo.BLL.Services;
using GeCo.Infrastructure;
using GeCo.Model;

namespace GeCo.ModuleCompetenze
{
    public class ModuleCompetenze : IModule
    {

        public void Initialize()
        {
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            regionManager.RegisterViewWithRegion("TaskButtonRegion", typeof(CompetenzeTaskButton));

            var container = ServiceLocator.Current.GetInstance<IUnityContainer>();
            container.RegisterType<Object, CompetenzeView>("CompetenzeView");
            container.RegisterType<Object, CompetenzeVM>("CompetenzeVM");
            

            //Registro i servizi
            container.RegisterType<IDipendentiServices, DipendentiServices>("Services");
        }
    }
}
