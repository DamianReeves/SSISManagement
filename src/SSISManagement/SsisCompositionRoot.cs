using System;
using SqlServer.Management.IntegrationServices.Data;
using SqlServer.Management.IntegrationServices.LightInject;

namespace SqlServer.Management.IntegrationServices
{
    internal class SsisCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ISqlConnectionStringBuilderFactory,SqlConnectionStringBuilderFactory>();
            serviceRegistry.Register<ISsisApplication, SsisApplication>();
            serviceRegistry.Register<ISsisCatalogFactory, SsisCatalogFactory>(new PerContainerLifetime());

            serviceRegistry.Register<ISsisCatalog,SsisCatalog>();
            serviceRegistry.Register<ISsisCatalogFactory, SsisCatalogFactory>();

            serviceRegistry.Register<ICatalogDataService, CatalogDataService>();
        }
    }
}