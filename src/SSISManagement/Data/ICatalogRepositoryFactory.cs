using System.Data.SqlClient;

namespace SqlServer.Management.IntegrationServices.Data
{
    public interface ICatalogRepositoryFactory:IRepositoryFactory<ICatalogRepository>
    {
    }

    internal class CatalogRepositoryFactory : RepositoryFactoryBase<ICatalogRepository>, ICatalogRepositoryFactory
    {
        public CatalogRepositoryFactory(IConnectionStringResolver connectionStringResolver)
            :base(connectionStringResolver)
        {            
        }

    }
}