using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SqlServer.Management.IntegrationServices.Data.Dtos;

namespace SqlServer.Management.IntegrationServices.Data
{
    public interface ICatalogDataService
    {
        IList<CatalogProperty> GetCatalogProperties();
        void Startup();   
    }
    public class CatalogDataService : ICatalogDataService
    {
        private readonly SqlConnectionStringBuilder _connectionStringBuilder;
        private readonly ICatalogRepositoryFactory _catalogRepositoryFactory;
        private readonly Lazy<ICatalogRepository> _catalogRepository;

        public CatalogDataService(SqlConnectionStringBuilder connectionStringBuilder, ICatalogRepositoryFactory catalogRepositoryFactory)
        {
            if (connectionStringBuilder == null) throw new ArgumentNullException("connectionStringBuilder");
            if (catalogRepositoryFactory == null) throw new ArgumentNullException("catalogRepositoryFactory");
            _connectionStringBuilder = connectionStringBuilder;
            _catalogRepositoryFactory = catalogRepositoryFactory;
            _catalogRepository = new Lazy<ICatalogRepository>(()=>_catalogRepositoryFactory.Create(_connectionStringBuilder));
        }

        public ICatalogRepository CatalogRepository
        {
            get { return _catalogRepository.Value; }
        }

        protected internal ICatalogRepositoryFactory CatalogRepositoryFactory
        {
            get { return _catalogRepositoryFactory; }
        }

        public IList<CatalogProperty> GetCatalogProperties()
        {
            return CatalogRepository.GetCatalogProperties();
        }

        public void Startup()
        {
            CatalogRepository.Startup();
        }
    }
}