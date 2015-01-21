using System.Data;
using SqlServer.Management.IntegrationServices.Data.Catalog;

namespace SqlServer.Management.IntegrationServices.Data
{
    internal abstract class ExecutionRepository : IExecutionRepository
    {
        private readonly ExecutionRepositoryImplementation _implementation;
        protected ExecutionRepository()
        {
            _implementation = new ExecutionRepositoryImplementation(GetConnection);
        }

        public ExecutionRepositoryImplementation Implementation
        {
            get { return _implementation; }
        }

        public abstract IDbConnection GetConnection();

        public virtual long CreateExecution(string folderName, string projectName, string packageName,
            long? referenceId = null,
            bool use32BitRuntime = false, int? commandTimeout = null)
        {
            return Implementation.CreateExecution(folderName, projectName, packageName, referenceId,
                use32BitRuntime, commandTimeout);
        }

        public virtual int StartExecution(long executionId, int? commandTimeout = null)
        {
            return Implementation.StartExecution(executionId, commandTimeout);
        }

        public virtual int SetExecutionParameterValue(long executionId, CatalogParameterType parameterType,
            string parameterName,
            object parameterValue, int? commandTimeout = null)
        {
            return Implementation.SetExecutionParameterValue(executionId, parameterType, parameterName,
                parameterValue, commandTimeout);
        }

        public long ExecutePackage(string folderName, string projectName, string packageName, long? referenceId = null,
            bool use32BitRuntime = false, bool synchrounous = false, int? commandTimeout = null)
        {
            return Implementation.ExecutePackage(folderName, projectName, packageName, referenceId, use32BitRuntime,
                synchrounous, commandTimeout);
        }
    }
}