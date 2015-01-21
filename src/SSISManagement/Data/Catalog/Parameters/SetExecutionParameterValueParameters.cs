using Insight.Database;

namespace SqlServer.Management.IntegrationServices.Data.Catalog.Parameters
{
    public class SetExecutionParameterValueParameters
    {
        private readonly long _executionId;
        private readonly CatalogParameterType _objectType;
        private readonly string _parameterName;
        private readonly object _parameterValue;

        public SetExecutionParameterValueParameters(long executionId, CatalogParameterType objectType, string parameterName, object parameterValue)
        {
            _executionId = executionId;
            _objectType = objectType;
            _parameterName = parameterName;
            _parameterValue = parameterValue;
        }

        public long ExecutionId
        {
            get { return _executionId; }
        }

        public CatalogParameterType ObjectType
        {
            get { return _objectType; }
        }

        public string ParameterName
        {
            get { return _parameterName; }
        }

        public object ParameterValue
        {
            get { return _parameterValue; }
        }
    }
}