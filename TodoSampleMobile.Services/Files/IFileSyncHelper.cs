using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.Files;
using Microsoft.WindowsAzure.MobileServices.Files.Metadata;
using Microsoft.WindowsAzure.MobileServices.Files.Sync;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace TodoSampleMobile.Services.Files
{
    public interface IFileSyncHelper
    {
        Task<string> GetFilesPathAsync(string modelName);
        Task DownloadFileAsync<T>(IMobileServiceSyncTable<T> table, MobileServiceFile file, string filename);
        Task<PathMobileServiceFileDataSource> GetFileDataSource(MobileServiceFileMetadata metadata);
    }
}