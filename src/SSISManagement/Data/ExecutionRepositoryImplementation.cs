using System;
using System.Data;
using System.Data.SqlClient;
using Insight.Database;
using SqlServer.Management.IntegrationServices.Core;
using SqlServer.Management.IntegrationServices.Data.Catalog;

namespace SqlServer.Management.IntegrationServices.Data
{
    internal class ExecutionRepositoryImplementation : IExecutionRepository
    {
        private readonly Func<IDbConnection> _connectionProvider;

        public ExecutionRepositoryImplementation(Func<IDbConnection> connectionProvider)
        {
            if (connectionProvider == null) throw new ArgumentNullException("connectionProvider");
            _connectionProvider = connectionProvider;
        }

        public Func<IDbConnection> ConnectionProvider
        {
            get { return _connectionProvider; }
        }

        public virtual IDbConnection GetConnection()
        {
            return ConnectionProvider();
        }

        public virtual long CreateExecution(string folderName, string projectName, string packageName,
            long? referenceId = null,
            bool use32BitRuntime = false, int? commandTimeout = null)
        {
            return this.WithConnectionAndParameters(
                p =>
                {
                    p.folder_name = folderName;
                    p.project_name = projectName;
                    p.package_name = packageName;
                    p.reference_id = referenceId;
                    p.use32bitruntime = use32BitRuntime;
                    p.execution_id = default(long?);
                }, (conn, dParameters, parameters) =>
                {
                    conn.Execute("catalog.create_execution", parameters, commandTimeout: commandTimeout);
                    return dParameters.execution_id;
                });
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
            return this.WithConnection(conn =>
            {
                dynamic parameters = new FastExpando();
                parameters.execution_id = executionId;
                return conn.Execute("catalog.start_execution", (FastExpando)parameters, commandTimeout: commandTimeout);
            });
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
            return this.WithConnectionAndParameters(
                p =>
                {
                    p.execution_id = executionId;
                }, (conn, parameters) =>
                {
                    return conn.Execute("catalog.set_execution_parameter_value", parameters, commandTimeout: commandTimeout);
                });
        }

        public virtual long ExecutePackage(string folderName, string projectName, string packageName,
            long? referenceId = null,
            bool use32BitRuntime = false, 
            bool synchrounous = false,
            int? commandTimeout = null)
        {
            try
            {
                var executionId = CreateExecution(folderName, projectName, packageName, referenceId, use32BitRuntime);
                StartExecution(executionId, commandTimeout);
                if (synchrounous)
                {
                    SetExecutionParameterValue(executionId, CatalogParameterType.SystemParameter, "SYNCHRONIZED", 1);
                }
                return executionId;
            }
            catch (SqlException ex)
            {
                throw ex.WrapSqlException();
            }
        }
        
    }
}