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
        private readonly ICatalogDataServiceFactory _catalogDataServiceFactory;
        private readonly ICatalogDataService _dataService;

        public SsisCatalog(string connectionStringOrName, ICatalogDataServiceFactory catalogDataServiceFactory)
        {            
            if (connectionStringOrName == null) throw new ArgumentNullException("connectionStringOrName");
            if (catalogDataServiceFactory == null) throw new ArgumentNullException("catalogDataServiceFactory");
            _catalogDataServiceFactory = catalogDataServiceFactory;
            _dataService = _catalogDataServiceFactory.Create(connectionStringOrName);
        }

        public ICatalogDataService DataService
        {
            get { return _dataService; }
        }

        internal ICatalogDataServiceFactory DataServiceFactory
        {
            get { return _catalogDataServiceFactory; }
        }

        

        public IDeployedProject GetProject(string folderName, string projectName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a folder in the Integration Services catalog.
        /// </summary>
        /// <param name="folderName">The name of the new folder.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <returns>The folder identifier is returned.</returns>
        public long CreateFolder(string folderName, int? commandTimeout = null)
        {
            return DataService.CreateFolder(folderName, commandTimeout);
        }

        /// <summary>
        /// Deletes a folder from the Integration Services catalog.
        /// </summary>
        /// <param name="folderName">The name of the folder that is to be deleted.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        public void DeleteFolder(string folderName, int? commandTimeout = null)
        {
            DataService.DeleteFolder(folderName, commandTimeout);
        }
    }
}
