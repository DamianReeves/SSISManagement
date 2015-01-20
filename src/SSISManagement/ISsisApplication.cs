using System;
using System.Data;
using System.Data.SqlClient;

namespace SqlServer.Management.IntegrationServices
{
    /// <summary>
    /// Provides an API for accessing the Integration Services catalogs across various servers.
    /// </summary>
    public interface ISsisApplication
    {        
        ISsisCatalog GetCatalog(string connectionStringOrName);
    }
}