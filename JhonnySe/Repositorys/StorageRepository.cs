using BlobStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlobStorage.ExtensionMethods;

namespace JhonnySe.Repositorys
{
    public class StorageRepository : IStorageRepository
    {
        private readonly BlobStorageClient _client;
        
        public StorageRepository(ISecretsRepository secrets)
        {
            _client = new BlobStorageClient(secrets.GetSecret("JhonnySeStorageConnectionString"));
        }

        public Task SaveData<T>(T model)
        {
            _client.CreateContainer("DataContainer");
            return null;
        }
    }
}
