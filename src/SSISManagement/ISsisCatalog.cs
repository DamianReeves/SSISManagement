using System.Data;
using System.Data.SqlClient;
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
    public interface ISsisCatalog
    {
        ICatalogDataService DataService { get; }
        IDeployedProject GetProject(string folderName, string projectName);

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
}