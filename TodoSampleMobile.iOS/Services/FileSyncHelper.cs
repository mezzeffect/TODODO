using System;
using System.IO;
using System.Threading.Tasks;
using TodoSampleMobile.iOS.Services;
using Microsoft.WindowsAzure.MobileServices.Files;
using Microsoft.WindowsAzure.MobileServices.Files.Metadata;
using Microsoft.WindowsAzure.MobileServices.Files.Sync;
using Microsoft.WindowsAzure.MobileServices.Sync;
using TodoSampleMobile.Services.Files;


[assembly: Xamarin.Forms.Dependency(typeof(FileSyncHelper))]

namespace TodoSampleMobile.iOS.Services
{
    public class FileSyncHelper : IFileSyncHelper
    {
        public Task<string> GetFilesPathAsync(string modelName)
        {
            var filesPath = Path.Combine(GetRootDataPath(), modelName + "Files");

            if (!Directory.Exists(filesPath))
            {
                Directory.CreateDirectory(filesPath);
            }

            return Task.FromResult(filesPath);
        }

        public async Task DownloadFileAsync<T>(IMobileServiceSyncTable<T> table, MobileServiceFile file,
            string targetPath)
        {
            await table.DownloadFileAsync(file, targetPath);
        }

        public async Task<PathMobileServiceFileDataSource> GetFileDataSource(MobileServiceFileMetadata metadata)
        {
            var filePath = await FileHelper.GetLocalFilePathAsync(metadata.ParentDataItemType, metadata.ParentDataItemId,
                        metadata.FileName);
            return new PathMobileServiceFileDataSource(filePath);
        }

        private string GetRootDataPath()
        {
            // return a reference to <Application_Home>/Library/Caches, so that the images are not marked for iCloud backup
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return Path.Combine(documents, "..", "Library", "Caches");
        }
    }
}