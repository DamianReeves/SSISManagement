using System;
using System.Data;
using System.Data.SqlClient;

namespace SqlServer.Management.IntegrationServices
{
    public interface ISsisApplication
    {        
        ISsisCatalog GetCatalog(SqlConnectionStringBuilder connectionStringBuilder);
    }

    public interface ISsisApplicationAdvanced : ISsisApplication
    {
        SsisConfiguration Configuration { get; }
    }
}