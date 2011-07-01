using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using GeCo.BLL.Services;
using GeCo.DAL;
using GeCo.BLL.AlgoritmoRicerca;
using System.Configuration;
using GeCo.Infrastructure;
using Microsoft.Practices.ServiceLocation;
using System.IO;

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
            this.container.RegisterType<IExcelServices, ExcelServices>();
            this.container.RegisterType<IAdminServices, AdminServices>();

            this.container.RegisterType<IAlgoritmoRicerca, RicercaDipendentiDaRuolo>("RicercaDipendenteDaRuolo");
            this.container.RegisterType<IAlgoritmoRicerca, RicercaRuoliDaDipendente>("RicercaRuoloDaDipendente");

            
            SetupDataDirectory();

            //In ogni caso EF si prende in automatico la connectionstring che ha il nome della classe,
            //se non la trova usa SQLEXPRESS
            var cnxString = ConfigurationManager.ConnectionStrings["PavimentalContext"].ConnectionString;
            //var cnxString = "PavimentalContext";

            container.AddNewExtension<EFRepositoryExtension>();
            container.Configure<IEFRepositoryExtension>()
                .WithConnection(cnxString)
                .WithContextLifetime(new ContainerControlledLifetimeManager());

            //Non ho chiamato IoC perchè altrimenti avrei dovuto aggiungere come reference anche MVVMLight
            var dbAdminService = ServiceLocator.Current.GetInstance<IAdminServices>();
            dbAdminService.VerificaDb();
        }

        public void SetupDataDirectory()
        {
            // This is our connection string: Data Source=|DataDirectory|\Chinook40.sdf
            // Set the data directory to the users %AppData% folder
            // So the Chinook40.sdf file must be placed in:  C:\\Users\\<Username>\\AppData\\Roaming\\

            //string dataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationInfo.Company, ApplicationInfo.ProductName);
            string dataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GeCo");

            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }

            AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory);
        }

    }
}
