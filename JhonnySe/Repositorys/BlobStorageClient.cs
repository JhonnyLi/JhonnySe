using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JhonnySe.Repositorys
{
    public class BlobStorageClient : IBlobStorageClient
    {
        private readonly BlobServiceClient _client;
        private readonly BlobBaseClient _blobClient;
        public BlobStorageClient(ISecretsRepository secrets)
        {
            var connString = secrets.GetSecret("JhonnySeStorageConnectionString");
            _client = new BlobServiceClient(connString);
            _blobClient = new BlobBaseClient(connString, "jhonnysedatacontainer", "Test");
        }

        public async Task<Response<BlobContainerClient>> CreateContainer(string containerName)
        {
            var response = await _client.CreateBlobContainerAsync(containerName);
            return response;
        }

        public async Task<Response> DeleteContainer(string containerName)
        {
            return await _client.DeleteBlobContainerAsync(containerName);
        }

        public BlobContainerItem GetContainer()
        {
            return _client.GetBlobContainers().AsPages().First().Values.FirstOrDefault();
        }

        public async Task<Response<BlobContentInfo>> UploadStream(string blobFileName, MemoryStream stream)
        {
            var containerClient = _client.GetBlobContainerClient("jhonnysedatacontainer");
            await containerClient.DeleteBlobIfExistsAsync(blobFileName);
            var response = await containerClient.UploadBlobAsync(blobFileName, stream);
            return response;
        }

        public static MemoryStream CreateMemoryStreamFromObject<T>(T model)
        {
            var serializedString = JsonConvert.SerializeObject(model);
            byte[] bytes = Encoding.UTF8.GetBytes(serializedString);
            return new MemoryStream(bytes);
        }

        public T GetBlob<T>()
        {
            BlobDownloadInfo download = _blobClient.Download();
            var stream = new MemoryStream();
            download.Content.CopyTo(stream);
            var bytes = stream.ToArray();
            var stringObject = Encoding.UTF8.GetString(bytes);
            return JsonConvert.DeserializeObject<T>(stringObject);
        }
    }
}
