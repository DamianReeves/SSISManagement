using System.Data.SqlClient;

namespace SqlServer.Management.IntegrationServices.Data.Services
{
    public interface IDbAccessorFactory
    {
        TDbAccessor Create<TDbAccessor>(SqlConnectionStringBuilder connectionStringBuilder) where TDbAccessor: class, IDbAccessor;
        TDbAccessor Create<TDbAccessor>(string connectionStringOrName) where TDbAccessor : class, IDbAccessor;
        TDbAccessor CreateFromConnectionString<TDbAccessor>(string connectionString) where TDbAccessor : class, IDbAccessor;
        TDbAccessor CreateFromConnectionName<TDbAccessor>(string connectionName) where TDbAccessor : class, IDbAccessor;
    }    
}