﻿using System;
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
    public class SsisCatalog : ISsisCatalog
    {
        private readonly SqlConnectionStringBuilder _connectionStringBuilder;
        private readonly IDbConnection _connection;
        private readonly Lazy<SsisDatabase> _databaseAccessor;

        /// <summary>
        /// Create the catalog using a supplied <see cref="IDbConnection"/> instance.
        /// </summary>
        /// <param name="connection">The connection to the database used by this catalog.</param>
        /// <exception cref="ArgumentNullException">The value of 'connection' cannot be null. </exception>
        public SsisCatalog(IDbConnection connection)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            _connection = connection;
            _databaseAccessor = new Lazy<SsisDatabase>(()=> _connection.AsParallel<SsisDatabase>());
        }

        public SsisCatalog(SqlConnectionStringBuilder connectionStringBuilder)
        {
            if (connectionStringBuilder == null) throw new ArgumentNullException("connectionStringBuilder");
            _connectionStringBuilder = connectionStringBuilder;
            _databaseAccessor = new Lazy<SsisDatabase>(connectionStringBuilder.AsParallel<SsisDatabase>);
        }

        public IDbConnection Connection
        {
            get { return _connection; }
        }

        public SqlConnectionStringBuilder ConnectionStringBuilder
        {
            get { return _connectionStringBuilder; }
        }

        /// <summary>
        /// Creates a folder in the Integration Services catalog.
        /// </summary>
        /// <param name="folderName">The name of the new folder.</param>
        /// <returns>The folder identifier is returned.</returns>
        public long CreateFolder(string folderName)
        {
            return Database.CreateFolder(folderName);
        }

        /// <summary>
        /// Deletes a folder from the Integration Services catalog.
        /// </summary>
        /// <param name="folderName">The name of the folder that is to be deleted.</param>
        public void DeleteFolder(string folderName)
        {
            Database.DeleteFolder(folderName);
        }

        public ISsisDatabase Database
        {
            get { return _databaseAccessor.Value; }
        }        

        public IDeployedProject GetProject(string folderName, string projectName)
        {
            var project = new DeployedProjectFacade(folderName, projectName);
            project.SetConnection(Connection);
            return project;
        }    
    }
}
