using System;
using System.Data.SqlClient;
using System.Linq;
using Insight.Database;

namespace SqlServer.Management.IntegrationServices.Data
{
    public interface IFolderRepositoryFactory : IRepositoryFactory<IFolderRepository>
    {
    }

    internal class FolderRepositoryFactory : RepositoryFactoryBase<IFolderRepository>, IFolderRepositoryFactory
    {
        public FolderRepositoryFactory(IConnectionStringResolver connectionStringResolver)
            :base(connectionStringResolver)
        {
        }

        protected override IFolderRepository CreateRepositoryFromConnectionString(string connectionString)
        {
            return CreateRepository<FolderRepository>(connectionString);
        }
    }
}