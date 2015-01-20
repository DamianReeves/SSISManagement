using System.Collections.Generic;
using SqlServer.Management.IntegrationServices.Data.Dtos;

namespace SqlServer.Management.IntegrationServices.Data
{
    public interface ICatalogRepository : IRequireDatabase
    {
        IList<CatalogProperty> GetCatalogProperties();
        void Startup();
    }
}