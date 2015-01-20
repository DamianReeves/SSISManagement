using System;
using SqlServer.Management.IntegrationServices.Data;

namespace SqlServer.Management.IntegrationServices.Core.Services
{
    /// <summary>
    /// Provides a collection of the services required by the SsisApplication
    /// </summary>
    public interface IServiceCollection
    {
        IDbAccessorFactory DbAccessorFactory { get; }
        ISqlConnectionStringBuilderFactory SqlConnectionStringBuilderFactory { get; }
    }

    internal class ServiceCollection : IServiceCollection
    {
        private Func<IDbAccessorFactory> _dbAccessorFactoryActivator;
        private Func<ISqlConnectionStringBuilderFactory> _sqlConnectionStringBuilderFactoryActivator;

        public ServiceCollection()
        {
            _dbAccessorFactoryActivator = 
                () => new DbAccessorFactory(_sqlConnectionStringBuilderFactoryActivator());
            _sqlConnectionStringBuilderFactoryActivator =
                () => new SqlConnectionStringBuilderFactory();
        }

        public IDbAccessorFactory DbAccessorFactory
        {
            get { return _dbAccessorFactoryActivator(); }
        }

        public ISqlConnectionStringBuilderFactory SqlConnectionStringBuilderFactory
        {
            get { return _sqlConnectionStringBuilderFactoryActivator(); }
        }

        /// <summary>
        /// Registers a resolver for the <see cref="IDbAccessorFactory"/>
        /// </summary>
        /// <param name="dbAccessorFactoryResolver"></param>
        /// <exception cref="ArgumentNullException">The value of 'dbAccessorFactoryResolver' cannot be null. </exception>
        public void Register(Func<IDbAccessorFactory> dbAccessorFactoryResolver)
        {
            if (dbAccessorFactoryResolver == null) throw new ArgumentNullException("dbAccessorFactoryResolver");
            _dbAccessorFactoryActivator = dbAccessorFactoryResolver;
        }

        /// <summary>
        /// </summary>
        /// <param name="sqlConnectionStringBuilderResolver"></param>
        /// <exception cref="ArgumentNullException">The value of 'sqlConnectionStringBuilderResolver' cannot be null. </exception>
        public void Register(Func<ISqlConnectionStringBuilderFactory> sqlConnectionStringBuilderResolver)
        {
            if (sqlConnectionStringBuilderResolver == null)
                throw new ArgumentNullException("sqlConnectionStringBuilderResolver");
            _sqlConnectionStringBuilderFactoryActivator = sqlConnectionStringBuilderResolver;
        }
    }
}
