using System;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using Insight.Database;
using SqlServer.Management.IntegrationServices.Core.Services;
using SqlServer.Management.IntegrationServices.Data;
using SqlServer.Management.IntegrationServices.Data.Catalog.Parameters;

namespace SqlServer.Management.IntegrationServices
{    
    public class SsisConfiguration
    {
        private static bool _hasInsightBeenInitialized;
        private readonly IServiceContainer _container;
        static SsisConfiguration()
        {
            EnsureInsightIsInitialized();
        }
        
        public SsisConfiguration()
        {
            _container = new ServiceContainer();
        }        

        internal static void EnsureInsightIsInitialized()
        {
            if (!_hasInsightBeenInitialized)
            {
                // Ensure we register the SqlInsightDbProvider
                // TODO: Consider moving this call closer to where the Insight.Database dependency is actually used
                SqlInsightDbProvider.RegisterProvider();
                ColumnMapping.Parameters.AddMapper(new SsisParameterMapper());
                _hasInsightBeenInitialized = true;
            }
        }
    }
}