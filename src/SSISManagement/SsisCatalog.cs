using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Insight.Database;
using SqlServer.Management.IntegrationServices.Data;
using SqlServer.Management.IntegrationServices.Data.Catalog.Parameters;

namespace SqlServer.Management.IntegrationServices
{
    /// <summary>
    /// Represents the Integration Services Catalog.
    /// </summary>
    /// <remarks>
    /// The <see cref="ISsisCatalog"/> interface provides an API for working with the Integration Services Catalog in SQL Server 2012 and above.
    /// </remarks>
    internal class SsisCatalog : ISsisCatalog
    {
        private readonly ICatalogDataService _dataService;

        public SsisCatalog(ICatalogDataService dataService)
        {
            if (dataService == null) throw new ArgumentNullException("dataService");
            _dataService = dataService;
        }

        public ICatalogDataService DataService
        {
            get { return _dataService; }
        }

        /// <summary>
        /// Creates a folder in the Integration Services catalog.
        /// </summary>
        /// <param name="folderName">The name of the new folder.</param>
        /// <returns>The folder identifier is returned.</returns>
        public long CreateFolder(string folderName)
        {
            return DataService.CreateFolder(folderName);
        }

        /// <summary>
        /// Deletes a folder from the Integration Services catalog.
        /// </summary>
        /// <param name="folderName">The name of the folder that is to be deleted.</param>
        public void DeleteFolder(string folderName)
        {
            DataService.DeleteFolder(folderName);
        }               

        public IDeployedProject GetProject(string folderName, string projectName)
        {
            throw new NotImplementedException();
        }    
    }
}
