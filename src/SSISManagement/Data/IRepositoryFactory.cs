using System.Data.SqlClient;

namespace SqlServer.Management.IntegrationServices.Data
{
    public interface IRepositoryFactory<out T> where T: class, IRepository
    {
        T Create(string connectionStringOrName);
    }
}