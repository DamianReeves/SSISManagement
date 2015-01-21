using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Insight.Database;
using SqlServer.Management.IntegrationServices.Core;
using SqlServer.Management.IntegrationServices.Data.Catalog;
using SqlServer.Management.IntegrationServices.Data.Catalog.Parameters;
using SqlServer.Management.IntegrationServices.Data.Dtos;

namespace SqlServer.Management.IntegrationServices.Data
{
    public abstract class SsisDatabase : ISsisDatabase
    {        
        private static readonly Type ThisType = typeof (SsisDatabase);
        private ExecutionRepositoryImplementation _executionRepositoryImplementation;
        

        internal ExecutionRepositoryImplementation ExecutionRepositoryImplementation
        {
            get
            {
                Interlocked.CompareExchange(ref _executionRepositoryImplementation
                    , new ExecutionRepositoryImplementation(GetConnection)
                    , null);
                return _executionRepositoryImplementation;
            }
        }

        public abstract IDbConnection GetConnection();

        [Sql("startup", Schema = "catalog")]
        public abstract void Startup();

        public virtual long CreateExecution(string folderName, string projectName, string packageName,
            long? referenceId = null,
            bool use32BitRuntime = false, int? commandTimeout = null)
        {
            return ExecutionRepositoryImplementation.CreateExecution(folderName, projectName, packageName, referenceId,
                use32BitRuntime, commandTimeout);
        }

        /// <summary>
        /// Starts an instance of execution in the Integration Services catalog.
        /// </summary>
        /// <remarks>
        /// An execution is used to specify the parameter values that will be used by a package during a single instance of package execution. After an instance of execution has been created, before it has been started, the corresponding project might be redeployed. In this case, the instance of execution will reference a project that is outdated. This will cause the stored procedure to fail.
        /// </remarks>
        /// <param name="executionId">The unique identifier for the instance of execution. </param>
        /// <param name="commandTimeout"></param>
        /// <returns><b>0</b> if successful.</returns>
        /// <exception cref="Exception">A delegate callback throws an exception. </exception>
        public virtual int StartExecution(long executionId, int? commandTimeout = null)
        {
            return ExecutionRepositoryImplementation.StartExecution(executionId, commandTimeout);
        }

        /// <summary>
        /// Sets the value of a parameter for an instance of execution in the Integration Services catalog.
        /// A parameter value cannot be changed after an instance of execution has started.
        /// </summary>
        /// <remarks>
        /// To find out the parameter values that were used for a given execution, query the catalog.execution_parameter_values view.
        /// To specify the scope of information that is logged during a package execution, set parameter_name to LOGGING_LEVEL and set parameter_value to one of the following values.
        /// Set the object_type parameter to 50.
        /// </remarks>
        /// <param name="executionId"></param>
        /// <param name="parameterType"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public int SetExecutionParameterValue(long executionId, CatalogParameterType parameterType, string parameterName,
            object parameterValue, int? commandTimeout = null)
        {
            return ExecutionRepositoryImplementation.SetExecutionParameterValue(executionId, parameterType, parameterName,
                parameterValue, commandTimeout);
        }

        public virtual long ExecutePackage(string folderName, string projectName, string packageName,
            long? referenceId = null,
            bool use32BitRuntime = false,
            bool synchrounous = false,
            int? commandTimeout = null)
        {
            return ExecutionRepositoryImplementation.ExecutePackage(folderName, projectName, packageName, referenceId, use32BitRuntime,
                synchrounous, commandTimeout);
        }

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
                    , new { folder_name = folderName }
                    , commandTimeout: commandTimeout);
            });
        }        

        public virtual IList<CatalogProperty> GetCatalogProperties()
        {
            var sql = GetSqlText("GetCatalogProperties.sql");
            return GetConnection().Query<CatalogProperty>(sql, commandType: CommandType.Text);
        }

        private string GetSqlText(string filename)
        {
            var resourceName = string.Format("{0}.Sql.{1}",ThisType.Namespace, filename);
            return ThisType.Assembly.GetEmbeddedTextResource(resourceName);
        }        
    }    
}
