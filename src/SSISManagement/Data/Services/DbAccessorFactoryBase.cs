using System.Data.SqlClient;

namespace SqlServer.Management.IntegrationServices.Data.Services
{
    public abstract class DbAccessorFactoryBase : IDbAccessorFactory
    {
        private readonly ISqlConnectionStringBuilderFactory _connectionStringBuilderFactory;

        protected DbAccessorFactoryBase(ISqlConnectionStringBuilderFactory connectionStringBuilderFactory)
        {
            _connectionStringBuilderFactory = connectionStringBuilderFactory;
        }

        public ISqlConnectionStringBuilderFactory ConnectionStringBuilderFactory
        {
            get { return _connectionStringBuilderFactory; }
        }

        public abstract TDbAccessor Create<TDbAccessor>(SqlConnectionStringBuilder connectionStringBuilder) where TDbAccessor : class, IDbAccessor;

        public virtual TDbAccessor Create<TDbAccessor>(string connectionStringOrName)
            where TDbAccessor : class, IDbAccessor
        {
            return Create<TDbAccessor>(ConnectionStringBuilderFactory.Create(connectionStringOrName));
        }

        public virtual TDbAccessor CreateFromConnectionString<TDbAccessor>(string connectionString)
            where TDbAccessor : class, IDbAccessor
        {
            return Create<TDbAccessor>(ConnectionStringBuilderFactory.CreateFromConnectionString(connectionString));
        }

        public virtual TDbAccessor CreateFromConnectionName<TDbAccessor>(string connectionName)
            where TDbAccessor : class, IDbAccessor
        {
            return Create<TDbAccessor>(ConnectionStringBuilderFactory.CreateFromConnectionName(connectionName));
        }
    }
}