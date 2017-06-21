using System.IO;
using System.Threading.Tasks;
using PCLStorage;
using Xamarin.Forms;

namespace TodoSampleMobile.Services.Files
{
    public class FileHelper
    {
        public static async Task<string> CopyItemFileAsync(string modelName, string itemId, string filePath)
        {
            IFolder localStorage = FileSystem.Current.LocalStorage;

            var fileName = Path.GetFileName(filePath);
            var targetPath = await GetLocalFilePathAsync(modelName, itemId, fileName);

            var sourceFile = await localStorage.GetFileAsync(filePath);
            var sourceStream = await sourceFile.OpenAsync(FileAccess.Read);

            var targetFile = await localStorage.CreateFileAsync(targetPath, CreationCollisionOption.ReplaceExisting);

            using (var targetStream = await targetFile.OpenAsync(FileAccess.ReadAndWrite))
            {
                await sourceStream.CopyToAsync(targetStream);
            }

            return targetPath;
        }

        public static async Task<string> GetLocalFilePathAsync(string modelName, string itemId, string fileName)
        {
            var fileSyncHelper = DependencyService.Get<IFileSyncHelper>();

            var recordFilesPath = Path.Combine(await fileSyncHelper.GetFilesPathAsync(modelName), itemId);

            var checkExists = await FileSystem.Current.LocalStorage.CheckExistsAsync(recordFilesPath);
            if (checkExists == ExistenceCheckResult.NotFound)
            {
                await FileSystem.Current.LocalStorage.CreateFolderAsync(recordFilesPath,
                    CreationCollisionOption.ReplaceExisting);
            }
            return Path.Combine(recordFilesPath, fileName);
        }

        public static async Task DeleteLocalFileAsync(string modelName,
            Microsoft.WindowsAzure.MobileServices.Files.MobileServiceFile fileName)
        {
            var localPath = await GetLocalFilePathAsync(modelName, fileName.ParentId, fileName.Name);
            var checkExists = await FileSystem.Current.LocalStorage.CheckExistsAsync(localPath);

            if (checkExists == ExistenceCheckResult.FileExists)
            {
                var file = await FileSystem.Current.LocalStorage.GetFileAsync(localPath);
                await file.DeleteAsync();
            }
        }

        public static async Task DeleteLocalPathAsync(string fullPath)
        {
            var checkExists = await FileSystem.Current.LocalStorage.CheckExistsAsync(fullPath);

            if (checkExists == ExistenceCheckResult.FolderExists)
            {
                var folder = await FileSystem.Current.LocalStorage.GetFolderAsync(fullPath);
                await folder.DeleteAsync();
            }
        }
    }
}