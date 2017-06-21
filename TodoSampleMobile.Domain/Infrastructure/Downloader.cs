using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.Files;
using Microsoft.WindowsAzure.MobileServices.Sync;
using PCLStorage;
using TodoSampleMobile.Services.Files;
using Xamarin.Forms;

namespace TodoSampleMobile.Domain.Infrastructure
{
    public static class Downloader
    {
        private static Task _currentDownloadTask = Task.FromResult(0);
        private static readonly object CurrentDownloadTaskLock = new object();
        internal static Task DownloadFileAsync<T>(IMobileServiceSyncTable<T> table, MobileServiceFile file, string path)
        {
            // should only download one file at a time, since it's possible to get duplicate notifications for the same file
            // ContinueWith is used along with Wait() so that only one thread downloads at a time
            lock (CurrentDownloadTaskLock)
            {
                return _currentDownloadTask =
                    _currentDownloadTask.ContinueWith(x => DoFileDownloadAsync(table,file,path)).Unwrap();
            }
        }

        private static async Task DoFileDownloadAsync<T>(IMobileServiceSyncTable<T> table ,MobileServiceFile file, string path)
        {
            Debug.WriteLine("Starting file download - " + file.Name);
            var fileSyncHelper = DependencyService.Get<IFileSyncHelper>();
            var tempPath = Path.ChangeExtension(path, ".temp");

            await fileSyncHelper.DownloadFileAsync(table, file, tempPath);

            var fileRef = await FileSystem.Current.LocalStorage.GetFileAsync(tempPath);
            await fileRef.RenameAsync(path, NameCollisionOption.ReplaceExisting);
            Debug.WriteLine("Renamed file to - " + path);

            //await MobileService.EventManager.PublishAsync(new ImageDownloadEvent(file.ParentId));
        }
    }
}