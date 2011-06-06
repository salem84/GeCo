using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using GeCo.BLL.Services;
using GeCo.Infrastructure;
using GeCo.Model;
using GeCo.ModuleRuoli.Views;
using GeCo.ModuleRuoli.ViewModels;

namespace GeCo.ModuleRuoli
{
    public class ModuleRuoli : IModule
    {

        public void Initialize()
        {
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            regionManager.RegisterViewWithRegion("TaskButtonRegion", typeof(RuoliTaskButton));

            var container = ServiceLocator.Current.GetInstance<IUnityContainer>();
            container.RegisterType<Object, RuoliRibbonTab>("RuoliRibbonTab");
            container.RegisterType<Object, RuoliWorkspaceContainer>("RuoliWorkspaceContainer");
            container.RegisterType<Object, RuoliWorkspaceContainerVM>(new ContainerControlledLifetimeManager());

            //Registro i servizi
            container.RegisterType<IRuoliServices, RuoliServices>("RuoliServices");


            /*var cnxString = @"Data Source=.\SQLEXPRESS;Initial Catalog=GeCo.DAL.PavimentalContext;Integrated Security=True";
            
            container.AddNewExtension<EFRepositoryExtension>();
            container.Configure<IEFRepositoryExtension>()
                .WithConnection(cnxString)
                .WithContextLifetime(new ContainerControlledLifetimeManager());*/
        }
    }
}
