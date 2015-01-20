using System;
using SqlServer.Management.IntegrationServices.Data;
using SqlServer.Management.IntegrationServices.LightInject;

namespace SqlServer.Management.IntegrationServices
{
    internal class SsisCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IConnectionStringResolver,ConnectionStringResolver>(new PerContainerLifetime());
            serviceRegistry.Register<ISsisApplication, SsisApplication>(new PerRequestLifeTime());

            serviceRegistry.Register<ICatalogDataServiceFactory, CatalogDataServiceFactory>(new PerContainerLifetime());           

            serviceRegistry.Register<ISsisCatalogFactory, SsisCatalogFactory>(new PerContainerLifetime());            

            serviceRegistry.Register<ICatalogDataService, CatalogDataService>();

            serviceRegistry.Register<IFolderRepositoryFactory, FolderRepositoryFactory>(new PerContainerLifetime());
            serviceRegistry.Register<ICatalogRepositoryFactory, CatalogRepositoryFactory>(new PerContainerLifetime());
        }
    }
}