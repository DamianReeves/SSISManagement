using System.Data.SqlClient;
using Insight.Database;
using SqlServer.Management.IntegrationServices.Data;

namespace SqlServer.Management.IntegrationServices.Core.Services
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