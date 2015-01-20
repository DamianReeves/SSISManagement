using System.Data;

namespace SqlServer.Management.IntegrationServices.Data
{
    public interface IRequireDatabase : IRepository
    {
    }

    public interface IRepository
    {
        IDbConnection GetConnection();
    }
}