using System;
using System.Data.SqlClient;
using System.Linq;
using Insight.Database;

namespace SqlServer.Management.IntegrationServices.Data
{
    public abstract class RepositoryFactoryBase<TRepository> : IRepositoryFactory<TRepository>
        where TRepository : class, IRepository
    {
        private readonly IConnectionStringResolver _connectionStringResolver;

        protected RepositoryFactoryBase(IConnectionStringResolver connectionStringResolver)
        {
            if (connectionStringResolver == null)
                throw new ArgumentNullException("connectionStringResolver");
            _connectionStringResolver = connectionStringResolver;
        }

        public IConnectionStringResolver ConnectionStringResolver
        {
            get { return _connectionStringResolver; }
        }

        public virtual TRepository Create(string connectionStringOrName)
        {
            var connectionString = ConnectionStringResolver.GetConnectionStringResolved(connectionStringOrName);
            return CreateRepository<TRepository>(connectionString);
        }

        protected virtual TRepository CreateRepositoryFromConnectionString(string connectionString)
        {
            return CreateRepository<TRepository>(connectionString);
        }

        protected virtual T CreateRepository<T>(string connectionString) where T : class, TRepository
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            return builder.AsParallel<T>();
        }
    }
}