using System.Collections.Generic;
using System.Data.SqlClient;
using SqlServer.Management.IntegrationServices.Data.Dtos;

namespace SqlServer.Management.IntegrationServices.Data
{
    public interface ICatalogRepository
    {
        IList<CatalogProperty> GetCatalogProperties();
        void Startup();
    }

    public interface ICatalogRepositoryFactory
    {
        ICatalogRepository Create(SqlConnectionStringBuilder connectionStringBuilder);
    }
}