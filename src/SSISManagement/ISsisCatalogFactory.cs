namespace SqlServer.Management.IntegrationServices
{
    public interface ISsisCatalogFactory
    {
        ISsisCatalog Create(string connectionStringOrName);
    }

    internal class SsisCatalogFactory : ISsisCatalogFactory
    {
        public ISsisCatalog Create(string connectionStringOrName)
        {
            throw new System.NotImplementedException();
        }
    }
}