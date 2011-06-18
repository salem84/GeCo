using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using GeCo.BLL.Services;
using GeCo.DAL;
using GeCo.BLL.AlgoritmoRicerca;

namespace GeCo.BLL
{
    public class ModuleInit : IModule
    {
        private readonly IUnityContainer container;

        public ModuleInit(IUnityContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            this.container.RegisterType<IDipendentiServices, DipendentiServices>();
            this.container.RegisterType<IRuoliServices, RuoliServices>();
            this.container.RegisterType<ICompetenzeServices, CompetenzeServices>();
            this.container.RegisterType<IRicercaServices, RicercaServices>();

            this.container.RegisterType<IAlgoritmoRicerca, RicercaDipendentiDaRuolo>("RicercaDipendenteDaRuolo");
            this.container.RegisterType<IAlgoritmoRicerca, RicercaRuoliDaDipendente>("RicercaRuoloDaDipendente");


            var cnxString = @"Data Source=.\SQLEXPRESS;Initial Catalog=GeCo.DAL.PavimentalContext;Integrated Security=True";
            
            container.AddNewExtension<EFRepositoryExtension>();
            container.Configure<IEFRepositoryExtension>()
                .WithConnection(cnxString)
                .WithContextLifetime(new ContainerControlledLifetimeManager());
        }
    }
}
