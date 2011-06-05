using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeCo.DAL;
using GeCo.Model;
using System.Data.Entity;
using GeCo.BLL.Services;
using GeCo.Infrastructure;
using Microsoft.Practices.Unity;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.UnityExtensions;

namespace TestProject
{
    [TestClass]
    public class TestDAL
    {
        [TestMethod]
        public void RepositoryTest()
        {
            
            

            var context = new PavimentalContext();
            var contextAdapter = new DbContextAdapter(context);
            var unitOfWork = new UnitOfWork(contextAdapter);

            var teamRepository = new BaseRepository<Area>(contextAdapter);
            var newArea = new Area { Titolo = "Da Bears" };

            teamRepository.Add(newArea);
            unitOfWork.Commit();

            

            context.Dispose();

        }

        [TestMethod]
        public void TestExtension()
        {
            
            var container = ServiceLocator.Current.GetInstance<IUnityContainer>();
            
            //Registro i servizi
            container.RegisterType<IDipendentiServices, DipendentiServices>("Services");


            var cnxString = @"Data Source=.\SQLEXPRESS;Initial Catalog=GeCo.DAL.PavimentalContext;Integrated Security=True";

            container.AddNewExtension<EFRepositoryExtension>();
            container.Configure<IEFRepositoryExtension>()
                .WithConnection(cnxString)
                .WithContextLifetime(new ContainerControlledLifetimeManager());

            Dipendente d = new Dipendente();
            d.Nome = "ciccio";

            var services = container.Resolve<IDipendentiServices>();
            var result = services.SalvaDipendente(d);
        }
    }
}

