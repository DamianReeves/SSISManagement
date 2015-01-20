using System.Collections.Generic;
using System.Data.SqlClient;
using SqlServer.Management.IntegrationServices.Data.Dtos;

namespace SqlServer.Management.IntegrationServices.Data
{
    public interface ICatalogRepository : IRequireDatabase
    {
        IList<CatalogProperty> GetCatalogProperties();
        void Startup();
    }

    public interface ICatalogRepositoryFactory:IRepositoryFactory<ICatalogRepository>
    {
    }

    public interface IRepositoryFactory<out T> where T: IDbAccessor
    {
        T Create(string connectionStringOrName);
        T Create(SqlConnectionStringBuilder connectionStringBuilder);
    }
}