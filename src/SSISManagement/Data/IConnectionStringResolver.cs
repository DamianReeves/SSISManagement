using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace SqlServer.Management.IntegrationServices.Data
{
    public interface IConnectionStringResolver
    {
        /// <summary>
        /// Gets a connection string given a connection string or connection name.
        /// A connection name can be specified in the format of name=ConnectionName.
        /// </summary>
        /// <param name="connectionStringOrName">The connection string or connection name</param>
        /// <returns>A connection string.</returns>
        string GetConnectionStringResolved(string connectionStringOrName);
        
        /// <summary>
        /// Creates a connection string from a connection string name. This name is usually the name of a connection string in
        /// in the app.config or web.config but can come from another source if implemented differently.
        /// </summary>
        /// <param name="connectionStringName"></param>
        /// <returns>A connection string</returns>
        string CreateFromConnectionName(string connectionStringName);
    }

    public abstract class ConnectionStringResolverBase : IConnectionStringResolver
    {
        /// <summary>
        /// A regular expression used to detect connection string names.
        /// </summary>
        public static readonly Regex ConnectionStringNameSpecificationRegex
            = new Regex(@"^\s*(?<key>name)\s*=\s*(?<value>.*)", RegexOptions.Compiled);

        
        public string GetConnectionStringResolved(string connectionStringOrName)
        {
            string connectionName;
            // If the following check returns true it is a connection name and we can use that to call the implementation
            if (TryGetConnectionName(connectionStringOrName, out connectionName))
            {
                return CreateFromConnectionName(connectionName);
            }

            // We have a connection string
            return connectionStringOrName;
        }


        /// <summary>
        /// Creates a connection string from a connection string name. This name is usually the name of a connection string in
        /// in the app.config or web.config but can come from another source if implemented differently.
        /// </summary>
        /// <param name="connectionStringName"></param>
        /// <returns>A connection string</returns>
        public string CreateFromConnectionName(string connectionStringName)
        {
            var connectionString = GetConnectionStringForConnectionName(connectionStringName);            
            return connectionString;
        }

        /// <summary>
        /// Try and get a connection name from the <see cref="connectionStringOrName"/> parameter.
        /// </summary>
        /// <param name="connectionStringOrName">A string representing a connection string or connection name</param>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        protected virtual bool TryGetConnectionName(string connectionStringOrName, out string connectionStringName)
        {
            var match = ConnectionStringNameSpecificationRegex.Match(connectionStringOrName);
            if (match.Success)
            {
                connectionStringName = match.Groups["value"].Value;
                return true;
            }
            connectionStringName = null;
            return false;
        }

        /// <summary>
        /// Get a connection string for the supplied connection name.
        /// </summary>
        /// <param name="connectionStringName">The connection name.</param>
        /// <returns>A connection string.</returns>
        protected abstract string GetConnectionStringForConnectionName(string connectionStringName);
    }

    internal class ConnectionStringResolver : ConnectionStringResolverBase
    {
        /// <summary>
        /// Get a connection string for the supplied connection name.
        /// </summary>
        /// <param name="connectionStringName">The connection name.</param>
        /// <returns>A connection string.</returns>
        /// <exception cref="KeyNotFoundException">If a connection string by the specified name cannot be found in the application's configuration.. </exception>
        protected override string GetConnectionStringForConnectionName(string connectionStringName)
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];
            if (connectionStringSettings == null)
            {
                throw new KeyNotFoundException("Could not locate a connection string by the name of '"
                                               + connectionStringName
                                               + "' in the application's configuration. "
                                               + "Make sure the connection name exists in app.config or web.config.");
            }
            return connectionStringSettings.ConnectionString;
        }
    }
}
