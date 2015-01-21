using System;
using System.Data;
using Insight.Database;

namespace SqlServer.Management.IntegrationServices.Data
{
    public interface IExecutionRepository : IRequireDatabase
    {
        long CreateExecution(string folderName, string projectName,
            string packageName, long? referenceId = null, bool use32BitRuntime = false, int? commandTimeout = null);
        /// <summary>
        /// Starts an instance of execution in the Integration Services catalog.
        /// </summary>
        /// <remarks>
        /// An execution is used to specify the parameter values that will be used by a package during a single instance of package execution. After an instance of execution has been created, before it has been started, the corresponding project might be redeployed. In this case, the instance of execution will reference a project that is outdated. This will cause the stored procedure to fail.
        /// </remarks>
        /// <param name="executionId">The unique identifier for the instance of execution. </param>
        /// <param name="commandTimeout"></param>
        /// <returns><b>0</b> if successful.</returns>
        int StartExecution(long executionId, int? commandTimeout = null);
        long ExecutePackage(string folderName, string projectName, string packageName, long? referenceId = null, bool use32BitRuntime = false, int? commandTimeout = null);
    }

    internal abstract class ExecutionRepository : IExecutionRepository
    {
        public abstract IDbConnection GetConnection();

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
                }, (conn, parameters) =>
                {
                    return conn.Execute("catalog.create_execution", parameters, commandTimeout: commandTimeout);
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

        public abstract long ExecutePackage(string folderName, string projectName, string packageName, long? referenceId = null,
            bool use32BitRuntime = false, int? commandTimeout = null);
        
    }
}