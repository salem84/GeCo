using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using System.Data.Entity.ModelConfiguration;

namespace GeCo.DAL
{
    public interface IEFRepositoryExtension : IUnityContainerExtensionConfigurator
    {
        IEFRepositoryExtension WithConnection(string connectionString);
        IEFRepositoryExtension WithContextLifetime(LifetimeManager lifetimeManager);
        IEFRepositoryExtension ConfigureEntity<T>(EntityTypeConfiguration<T> config) where T : class,new();
        IEFRepositoryExtension ConfigureEntity<T>(EntityTypeConfiguration<T> config, string setName) where T : class,new();
    }
}
