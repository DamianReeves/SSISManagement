using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SqlServer.Management.IntegrationServices.Data.Dtos;

namespace SqlServer.Management.IntegrationServices.Data
{
    public interface ICatalogDataService
    {
        IList<CatalogProperty> GetCatalogProperties();
        void Startup();

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
    public class CatalogDataService : ICatalogDataService
    {
        private readonly SqlConnectionStringBuilder _connectionStringBuilder;
        private readonly Lazy<IFolderRepository> _folderRepository;
        private readonly Lazy<ICatalogRepository> _catalogRepository;

        public CatalogDataService(
            SqlConnectionStringBuilder connectionStringBuilder, 
            ICatalogRepositoryFactory catalogRepositoryFactory, 
            IFolderRepositoryFactory folderRepositoryFactory)
        {
            if (connectionStringBuilder == null) throw new ArgumentNullException("connectionStringBuilder");
            if (catalogRepositoryFactory == null) throw new ArgumentNullException("catalogRepositoryFactory");
            if (folderRepositoryFactory == null) throw new ArgumentNullException("folderRepositoryFactory");
            _connectionStringBuilder = connectionStringBuilder;
            _catalogRepository = new Lazy<ICatalogRepository>(()=> catalogRepositoryFactory.Create(_connectionStringBuilder));
            _folderRepository = new Lazy<IFolderRepository>(()=> folderRepositoryFactory.Create(_connectionStringBuilder));
        }

        public ICatalogRepository CatalogRepository
        {
            get { return _catalogRepository.Value; }
        }

        public IFolderRepository FolderRepository
        {
            get { return _folderRepository.Value; }
        }

        public IList<CatalogProperty> GetCatalogProperties()
        {
            return CatalogRepository.GetCatalogProperties();
        }

        public void Startup()
        {
            CatalogRepository.Startup();
        }

        public long CreateFolder(string folderName, int? commandTimeout = null)
        {
            return FolderRepository.CreateFolder(folderName, commandTimeout);
        }

        public void DeleteFolder(string folderName, int? commandTimeout = null)
        {
            FolderRepository.DeleteFolder(folderName);
        }
    }
}