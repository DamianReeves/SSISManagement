using SqlServer.Management.IntegrationServices.Data.Catalog;

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
        int SetExecutionParameterValue(long executionId, CatalogParameterType parameterType, string parameterName, object parameterValue, int? commandTimeout = null);
        long ExecutePackage(string folderName, string projectName, string packageName, long? referenceId = null, bool use32BitRuntime = false, bool synchrounous = false, int? commandTimeout = null);
    }
}