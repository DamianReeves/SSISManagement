using System;

namespace SqlServer.Management.IntegrationServices
{
    public interface ISsisCatalogFactory
    {
        ISsisCatalog Create(string connectionStringOrName);
    }

    internal class SsisCatalogFactory : ISsisCatalogFactory
    {
        private readonly Func<string,ISsisCatalog> _factory;

        public SsisCatalogFactory(Func<string, ISsisCatalog> factory)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            _factory = factory;
        }

        public ISsisCatalog Create(string connectionStringOrName)
        {
            return _factory(connectionStringOrName);
        }
    }
}