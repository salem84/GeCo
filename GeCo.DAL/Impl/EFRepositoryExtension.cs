using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.Infrastructure;
using Microsoft.Practices.Unity;
using System.Data.Entity.ModelConfiguration;
using Microsoft.Practices.Unity.StaticFactory;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;

namespace GeCo.DAL
{
    public class EFRepositoryExtension : UnityContainerExtension, IEFRepositoryExtension
    {
        private DbModelBuilder _builder;
        private SqlConnection _connection;
        
        protected override void Initialize()
        {
            Container.RegisterType(typeof(IRepository<>), typeof(BaseRepository<>));
            Container.RegisterType<IUnitOfWork, UnitOfWork>();
            //Container.RegisterType<IGlobalUnitOfWork, GlobalUnitOfWork>();
            //Container.RegisterType(typeof(IGlobalRepository<>), typeof(GlobalBaseRepository<>));
        }

        public IEFRepositoryExtension WithConnection(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            return this;
        }

        public IEFRepositoryExtension ConfigureEntity<T>(EntityTypeConfiguration<T> config) where T : class,new()
        {
            ConfigureEntity(config, typeof(T).Name);
            return this;
        }

        public IEFRepositoryExtension ConfigureEntity<T>(EntityTypeConfiguration<T> config, string setName) where T : class,new()
        {
            if (_builder == null)
            {
                _builder = new DbModelBuilder();
                Container.RegisterInstance("ModelBuilder", _builder, new ContainerControlledLifetimeManager());
                //builder.IncludeMetadataInDatabase = false;
                //modelbuilder singleton for all application lifecyle
            }
            //add the configuration      
            _builder.Configurations.Add<T>(config);
            return this;
        }

        public IEFRepositoryExtension WithContextLifetime(LifetimeManager lifetimeManager)
        {
            Container.AddNewExtension<StaticFactoryExtension>();
            //Container.RegisterType<IStaticFactoryConfiguration,StaticFactoryExtension>();
            Container.Configure<IStaticFactoryConfiguration>()
                .RegisterFactory<IDbContextAdapter>(x => ContextResolver(x, lifetimeManager, _connection));
            return this;
        }

        //factory func to build context with given lifetime & connection  
        private static readonly Func<IUnityContainer, LifetimeManager, SqlConnection, object>
        ContextResolver = (c, l, s) =>
        {
            var context = l.GetValue();
            if (context == null)
            {
                //DbModelBuilder builder = c.Resolve<DbModelBuilder>("ModelBuilder"); 
                //DbModel model = builder.Build(s);
                //DbContext newDbContext = new DbContext(s, model.Compile(), true);
                //DbContext newDbContext = new PavimentalContext(s.ConnectionString);
                DbContext newDbContext = new PavimentalContext();
                

                context = new DbContextAdapter(newDbContext);
                l.SetValue(context);
            }
            return context;
        };

    }
}
