using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Files;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using TodoSampleMobile.Domain.AzureModels;


namespace TodoSampleMobile.Domain.Infrastructure
{
    public class AzureInitializer : IAzureInitializer
    {
        private readonly IMobileServiceClient _client;

        public AzureInitializer(IMobileServiceClient client)
        {
            _client = client;
        }

        /*should be called once after login*/

        public async Task SyncClientAndStore()
        {
            try
            {
                if (!_client.SyncContext.IsInitialized)
                {
                    var store = new MobileServiceSQLiteStore(Constants.OfflineDbName);
                    DefineTables(store);

                    // Initialize the SyncContext using the default IMobileServiceSyncHandler
                    await _client.SyncContext.InitializeAsync(store, StoreTrackingOptions.NotifyLocalAndServerOperations);
                }
            }
            catch (System.Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }

        private static void DefineTables(MobileServiceSQLiteStore store)
        {
            store.DefineTable<TodoItem>();
        }
    }
}