using System;

namespace SqlServer.Management.IntegrationServices.Data
{
    public interface ICatalogDataServiceFactory
    {
        ICatalogDataService Create(string connectionStringOrName);
    }

    internal class CatalogDataServiceFactory : ICatalogDataServiceFactory
    {
        private readonly ICatalogRepositoryFactory _catalogRepositoryFactory;
        private readonly IFolderRepositoryFactory _folderRepositoryFactory;

        public CatalogDataServiceFactory(ICatalogRepositoryFactory catalogRepositoryFactory, 
            IFolderRepositoryFactory folderRepositoryFactory )
        {
            if (catalogRepositoryFactory == null) throw new ArgumentNullException("catalogRepositoryFactory");
            if (folderRepositoryFactory == null) throw new ArgumentNullException("folderRepositoryFactory");
            _catalogRepositoryFactory = catalogRepositoryFactory;
            _folderRepositoryFactory = folderRepositoryFactory;
        }

        public ICatalogRepositoryFactory CatalogRepositoryFactory
        {
            get { return _catalogRepositoryFactory; }
        }

        public IFolderRepositoryFactory FolderRepositoryFactory
        {
            get { return _folderRepositoryFactory; }
        }

        public ICatalogDataService Create(string connectionStringOrName)
        {
            return new CatalogDataService(connectionStringOrName
                , CatalogRepositoryFactory
                , FolderRepositoryFactory);
        }
    }
}