using System;
using System.Data;
using System.Data.SqlClient;
using SqlServer.Management.IntegrationServices.Configuration;
using SqlServer.Management.IntegrationServices.LightInject;

namespace SqlServer.Management.IntegrationServices
{
    /// <summary>
    /// The SsisApplication is the entry point into the Integration Services Management API.
    /// </summary>
    internal class SsisApplication : ISsisApplication
    {
        private readonly ISsisCatalogFactory _catalogFactory;
        
        public SsisApplication(ISsisCatalogFactory catalogFactory)
        {
            if (catalogFactory == null) throw new ArgumentNullException("catalogFactory");
            _catalogFactory = catalogFactory;
        }

        public ISsisCatalog GetCatalog(string connectionStringOrName)
        {
            return _catalogFactory.Create(connectionStringOrName);
        }
    }
}