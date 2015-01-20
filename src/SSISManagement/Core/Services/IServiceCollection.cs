using System;
using SqlServer.Management.IntegrationServices.Data;

namespace SqlServer.Management.IntegrationServices.Core.Services
{
    /// <summary>
    /// Provides a collection of the services required by the SsisApplication
    /// </summary>
    public interface IServiceCollection
    {
        Func<IDbAccessorFactory> DbAccessorFactoryActivator { get; }
        Func<ISqlConnectionStringBuilderFactory> SqlConnectionStringBuilderFactoryActivator { get; }
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

        public Func<IDbAccessorFactory> DbAccessorFactoryActivator
        {
            get { return _dbAccessorFactoryActivator; }
        }

        public Func<ISqlConnectionStringBuilderFactory> SqlConnectionStringBuilderFactoryActivator
        {
            get { return _sqlConnectionStringBuilderFactoryActivator; }
        }

        /// <summary>
        /// Registers a resolver for the <see cref="IDbAccessorFactory"/>
        /// </summary>
        /// <param name="dbAccessorFactoryActivator"></param>
        /// <exception cref="ArgumentNullException">The value of 'dbAccessorFactoryActivator' cannot be null. </exception>
        public void Register(Func<IDbAccessorFactory> dbAccessorFactoryActivator)
        {
            if (dbAccessorFactoryActivator == null) throw new ArgumentNullException("dbAccessorFactoryActivator");
            _dbAccessorFactoryActivator = dbAccessorFactoryActivator;
        }

        /// <summary>
        /// </summary>
        /// <param name="sqlConnectionStringBuilderActivator"></param>
        /// <exception cref="ArgumentNullException">The value of 'sqlConnectionStringBuilderActivator' cannot be null. </exception>
        public void Register(Func<ISqlConnectionStringBuilderFactory> sqlConnectionStringBuilderActivator)
        {
            if (sqlConnectionStringBuilderActivator == null)
                throw new ArgumentNullException("sqlConnectionStringBuilderActivator");
            _sqlConnectionStringBuilderFactoryActivator = sqlConnectionStringBuilderActivator;
        }
    }
}
