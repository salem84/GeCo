using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using GeCo.ModuleOpzioni.Views;
using GeCo.ModuleOpzioni.ViewModels;
using GeCo.BLL.Services;
using GeCo.Infrastructure;
using GeCo.Model;

namespace GeCo.ModuleOpzioni
{
    public class ModuleOpzioni : IModule
    {

        public void Initialize()
        {
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            regionManager.RegisterViewWithRegion("TaskButtonRegion", typeof(OpzioniTaskButton));

            var container = ServiceLocator.Current.GetInstance<IUnityContainer>();
            container.RegisterType<Object, OpzioniView>("OpzioniView");
            container.RegisterType<Object, OpzioniVM>("OpzioniVM");
            

            
            
        }
    }
}
