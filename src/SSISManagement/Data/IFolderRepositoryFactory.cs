using System;
using System.Data.SqlClient;
using System.Linq;
using Insight.Database;

namespace SqlServer.Management.IntegrationServices.Data
{
    public interface IFolderRepositoryFactory : IRepositoryFactory<IFolderRepository>
    {
    }

    internal class FolderRepositoryFactory : IFolderRepositoryFactory
    {
        private readonly ISqlConnectionStringBuilderFactory _sqlConnectionStringBuilderFactory;

        public FolderRepositoryFactory(ISqlConnectionStringBuilderFactory sqlConnectionStringBuilderFactory)
        {
            if (sqlConnectionStringBuilderFactory == null)
                throw new ArgumentNullException("sqlConnectionStringBuilderFactory");
            _sqlConnectionStringBuilderFactory = sqlConnectionStringBuilderFactory;
        }

        public ISqlConnectionStringBuilderFactory ConnectionStringBuilderFactory
        {
            get { return _sqlConnectionStringBuilderFactory; }
        }

        public IFolderRepository Create(string connectionStringOrName)
        {
            var builder = ConnectionStringBuilderFactory.Create(connectionStringOrName);
            return Create(builder);
        }

        public IFolderRepository Create(SqlConnectionStringBuilder connectionStringBuilder)
        {
            return connectionStringBuilder.AsParallel<FolderRepository>();
        }
    }
}