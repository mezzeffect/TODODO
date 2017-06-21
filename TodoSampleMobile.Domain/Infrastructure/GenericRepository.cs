using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Linq;
using System.Threading;
using Microsoft.WindowsAzure.MobileServices.Files;

namespace TodoSampleMobile.Domain.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class /* where T : BaseAzureModel*/
    {
        private readonly IMobileServiceClient _client;

        public GenericRepository(IMobileServiceClient client)
        {
            _client = client;
        }

        private IMobileServiceSyncTable<T> AzureTable => _client.GetSyncTable<T>();

        public async Task SyncAsync(string queryName, Expression<Func<T, bool>> criteria = null)
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;
            try
            {
                await AzureTable.PullAsync(
                    queryName,
                    criteria == null
                        ? AzureTable.CreateQuery()
                        : AzureTable.CreateQuery().Where(criteria));
                Debug.WriteLine("SyncAsync :" + queryName);
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine($@"error{exception.Message}");
            }
            // Simple error/conflict handling. A real application would handle the various errors like network conditions,
            // server conflicts and others via the IMobileServiceSyncHandler.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change.
                        await error.CancelAndDiscardItemAsync();
                    }

                    Debug.WriteLine(
                        $@"Error executing sync operation. Item: {error.TableName} ({error.Item["id"]}). Operation discarded.");
                }
            }
        }

        public async Task PurgeAsync(string queryName)
        {
            await AzureTable.PurgeAsync(queryName, null, true, CancellationToken.None);
        }

        public async Task UpdateItemAsync(T model)
        {
            try
            {
                await AzureTable.UpdateAsync(model);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }

        public async Task AddItemAsync(T model)
        {
            try
            {
                await AzureTable.InsertAsync(model);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }

        public async Task DeleteItemAsync(T model)
        {
            try
            {
                await AzureTable.DeleteAsync(model);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }

        public async Task<ObservableCollection<T>> GetItemsAsync(Expression<Func<T, bool>> criteria = null)
        {
            try
            {
                var items = (criteria != null)
                    ? await AzureTable.Where(criteria).ToEnumerableAsync()
                    : await AzureTable.ToEnumerableAsync();

                return new ObservableCollection<T>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine($@"Invalid sync operation: {msioe.Message}");
            }
            catch (Exception e)
            {
                Debug.WriteLine($@"Sync error: {e.Message}");
            }
            return null;
        }

        public async Task<ObservableCollection<T>> GetItemsAsync<TKey>(Enums.OrderTypes orderType,
            Expression<Func<T, TKey>> orderBy,
            Expression<Func<T, bool>> criteria = null)
        {
            try
            {
                var items = await (orderType == Enums.OrderTypes.Ascending
                    ? AzureTable.Where(criteria).OrderBy(orderBy).ToEnumerableAsync()
                    : AzureTable.Where(criteria).OrderByDescending(orderBy).ToEnumerableAsync());
                return new ObservableCollection<T>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine($@"Invalid sync operation: {msioe.Message}");
            }
            catch (Exception e)
            {
                Debug.WriteLine($@"Sync error: {e.Message}");
            }
            return null;
        }

        public async Task<T> GetItemAsync(Expression<Func<T, bool>> criteria = null)
        {
            try
            {
                var items = (criteria != null)
                    ? await AzureTable.Take(1).Where(criteria).ToEnumerableAsync()
                    : await AzureTable.Take(1).ToEnumerableAsync();

                return items.FirstOrDefault();
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine($@"Invalid sync operation: {msioe.Message}");
            }
            catch (Exception e)
            {
                Debug.WriteLine($@"Sync error: {e.Message}");
            }
            return null;
        }

        public async Task<MobileServiceFile> AddFileAsync(T model, string targetPath)
        {
            try
            {
                return await AzureTable.AddFileAsync(model, targetPath);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                return null;
            }
        }

        public async Task DeleteFileAsync(MobileServiceFile file)
        {
            try
            {
                await AzureTable.DeleteFileAsync(file);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }

        public async Task<IEnumerable<MobileServiceFile>> GetFilesAsync(T model)
        {
            try
            {
                return await AzureTable.GetFilesAsync(model);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                return null;
            }
        }
    }
}