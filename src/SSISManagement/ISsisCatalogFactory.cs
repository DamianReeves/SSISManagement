using System;
using SqlServer.Management.IntegrationServices.Data;

namespace SqlServer.Management.IntegrationServices
{
    public interface ISsisCatalogFactory
    {
        ISsisCatalog Create(string connectionStringOrName);
    }

    internal class SsisCatalogFactory : ISsisCatalogFactory
    {
        private readonly ICatalogDataServiceFactory _catalogDataServiceFactory;

        public SsisCatalogFactory(ICatalogDataServiceFactory catalogDataServiceFactory)
        {
            if (catalogDataServiceFactory == null) throw new ArgumentNullException("catalogDataServiceFactory");
            _catalogDataServiceFactory = catalogDataServiceFactory;
        }

        public ICatalogDataServiceFactory DataServiceFactory
        {
            get { return _catalogDataServiceFactory; }
        }

        public ISsisCatalog Create(string connectionStringOrName)
        {
            return new SsisCatalog(connectionStringOrName, DataServiceFactory);
        }
    }
}