using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.IO;
using System.Threading.Tasks;

namespace JhonnySe.Repositorys
{
    public interface IBlobStorageClient
    {
        Task<Response<BlobContainerClient>> CreateContainer(string containerName);
        Task<Response> DeleteContainer(string containerName);
        BlobContainerItem GetContainer();
        Task<Response<BlobContentInfo>> UploadStream(string blobFileName, MemoryStream stream);
        T GetBlob<T>();
    }
}