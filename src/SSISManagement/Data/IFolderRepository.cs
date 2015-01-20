﻿using System;
using System.Data;
using System.Data.SqlClient;
using Insight.Database;

namespace SqlServer.Management.IntegrationServices.Data
{
    public interface IFolderRepository : IRequireDatabase
    {
        /// <summary>
        /// Creates a folder in the Integration Services catalog.
        /// </summary>
        /// <param name="folderName">The name of the new folder.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <returns>The folder identifier is returned.</returns>
        long CreateFolder(string folderName, int? commandTimeout = null);

        /// <summary>
        /// Deletes a folder from the Integration Services catalog.
        /// </summary>
        /// <param name="folderName">The name of the folder that is to be deleted.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        void DeleteFolder(string folderName, int? commandTimeout = null);
    }

    internal abstract class FolderRepository: IFolderRepository
    {
        public abstract IDbConnection GetConnection();

        /// <summary>
        /// Creates a folder in the Integration Services catalog.
        /// </summary>
        /// <param name="folderName">The name of the new folder.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <returns>The folder identifier is returned.</returns>
        /// <exception cref="ArgumentNullException">The value of 'database' cannot be null. </exception>
        /// <exception cref="Exception">A delegate callback throws an exception. </exception>
        public long CreateFolder(string folderName, int? commandTimeout = null)
        {
            return this.WithConnection(conn =>
            {
                dynamic parameters = new FastExpando();
                parameters.folder_name = folderName;
                parameters.folder_id = default(int);
                conn.Execute("catalog.create_folder", (object)parameters, commandTimeout: commandTimeout);
                return parameters.folder_id;
            });
        }

        /// <summary>
        /// Deletes a folder from the Integration Services catalog.
        /// </summary>
        /// <param name="folderName">The name of the folder that is to be deleted.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        public void DeleteFolder(string folderName, int? commandTimeout = null)
        {
            this.WithConnection(conn =>
            {                
                conn.Execute("catalog.create_folder"
                    , new {folder_name = folderName}
                    , commandTimeout: commandTimeout);
            });
        }
    }
}