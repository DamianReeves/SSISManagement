using System;

namespace SqlServer.Management.IntegrationServices
{
    public interface ISsisCatalogFactory
    {
        ISsisCatalog Create(string connectionStringOrName);
    }

    internal class SsisCatalogFactory : ISsisCatalogFactory
    {
        private readonly Func<ISsisCatalog> _factory;

        public SsisCatalogFactory(Func<ISsisCatalog> factory)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            _factory = factory;
        }

        public ISsisCatalog Create(string connectionStringOrName)
        {
            return _factory();
        }
    }
}