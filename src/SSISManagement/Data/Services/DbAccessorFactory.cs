using System.Data.SqlClient;
using Insight.Database;

namespace SqlServer.Management.IntegrationServices.Data.Services
{
    internal class DbAccessorFactory : DbAccessorFactoryBase

    {
        public DbAccessorFactory(ISqlConnectionStringBuilderFactory connectionStringBuilderFactory) 
            : base(connectionStringBuilderFactory)
        {
        }

        public override TDbAccessor Create<TDbAccessor>(SqlConnectionStringBuilder connectionStringBuilder) 
        {
            return connectionStringBuilder.AsParallel<TDbAccessor>();
        }
    }
}