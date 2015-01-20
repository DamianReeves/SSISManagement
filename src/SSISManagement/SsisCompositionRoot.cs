using System;
using SqlServer.Management.IntegrationServices.Data;
using SqlServer.Management.IntegrationServices.LightInject;

namespace SqlServer.Management.IntegrationServices
{
    internal class SsisCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IConnectionStringResolver,ConnectionStringResolver>();
            serviceRegistry.Register<ISsisApplication, SsisApplication>();

            serviceRegistry.Register<ICatalogDataServiceFactory, CatalogDataServiceFactory>(new PerContainerLifetime());
            
            serviceRegistry.Register<string, ISsisCatalog>(
                (factory,value)=> new SsisCatalog(value,factory.GetInstance<ICatalogDataServiceFactory>()));

            serviceRegistry.Register<ISsisCatalogFactory, SsisCatalogFactory>(new PerContainerLifetime());
            
            serviceRegistry.Register<ISsisCatalogFactory, SsisCatalogFactory>();

            serviceRegistry.Register<ICatalogDataService, CatalogDataService>();

            serviceRegistry.Register<IFolderRepositoryFactory, FolderRepositoryFactory>(new PerContainerLifetime());
            serviceRegistry.Register<ICatalogRepositoryFactory, CatalogRepositoryFactory>(new PerContainerLifetime());
        }
    }
}