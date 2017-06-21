using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Files.Eventing;
using Microsoft.WindowsAzure.MobileServices.Files.Sync;
using Microsoft.WindowsAzure.MobileServices.Files.Sync.Triggers;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;

namespace TodoSampleMobile.Domain.Infrastructure
{
    internal class FileSyncTriggerFactory : IFileSyncTriggerFactory
    {
        private readonly bool _autoUpdateRecords;
        private readonly IMobileServiceClient _mobileServiceClient;

        public FileSyncTriggerFactory(IMobileServiceClient mobileServiceClient, bool autoUpdateParentRecords)
        {
            if (mobileServiceClient == null)
            {
                throw new ArgumentNullException(nameof(mobileServiceClient));
            }

            this._mobileServiceClient = mobileServiceClient;
            this._autoUpdateRecords = autoUpdateParentRecords;
        }

        public IList<IFileSyncTrigger> CreateTriggers(IFileSyncContext fileSyncContext)
        {
            return new List<IFileSyncTrigger> { new CustomFileSyncTrigger(fileSyncContext, this._mobileServiceClient, this._autoUpdateRecords) };
        }
    }

    public sealed class CustomFileSyncTrigger : IFileSyncTrigger, IDisposable
    {
        private readonly IDisposable _dataChangeNotificationSubscription;
        private readonly IDisposable _fileChangeNotificationSubscription;
        private readonly IFileSyncContext _fileSyncContext;
        private readonly IMobileServiceClient _mobileServiceClient;

        public CustomFileSyncTrigger(IFileSyncContext fileSyncContext, IMobileServiceClient mobileServiceClient, bool autoUpdateParentRecords)
        {
            if (fileSyncContext == null)
            {
                throw new ArgumentNullException(nameof(fileSyncContext));
            }

            if (mobileServiceClient == null)
            {
                throw new ArgumentNullException(nameof(mobileServiceClient));
            }

            this._fileSyncContext = fileSyncContext;
            this._mobileServiceClient = mobileServiceClient;

            this._dataChangeNotificationSubscription = mobileServiceClient.EventManager.Subscribe<StoreOperationCompletedEvent>(OnStoreOperationCompleted);

            if (autoUpdateParentRecords)
            {
                this._fileChangeNotificationSubscription = mobileServiceClient.EventManager.Subscribe<FileOperationCompletedEvent>(OnFileOperationCompleted);
            }
        }

        ~CustomFileSyncTrigger()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;

            this._dataChangeNotificationSubscription?.Dispose();
            this._fileChangeNotificationSubscription?.Dispose();
        }

        private async void OnFileOperationCompleted(FileOperationCompletedEvent obj)
        {
            if (obj.Source == FileOperationSource.Local)
            {
                IMobileServiceSyncTable table = this._mobileServiceClient.GetSyncTable(obj.File.TableName);
                JObject item = await table.LookupAsync(obj.File.ParentId);

                if (item != null)
                {
                    await table.UpdateAsync(item);
                }
            }
        }

        private async void OnStoreOperationCompleted(StoreOperationCompletedEvent storeOperationEvent)
        {
            if (storeOperationEvent.Operation.TableName != "PlanTask")
                return;

            try
            {
                switch (storeOperationEvent.Operation.Kind)
                {
                    case LocalStoreOperationKind.Insert:
                    case LocalStoreOperationKind.Update:
                    case LocalStoreOperationKind.Upsert:
                        if (storeOperationEvent.Operation.Source == StoreOperationSource.ServerPull
                            || storeOperationEvent.Operation.Source == StoreOperationSource.ServerPush)
                        {
                            await this._fileSyncContext.PullFilesAsync(storeOperationEvent.Operation.TableName, storeOperationEvent.Operation.RecordId);
                        }
                        break;
                    case LocalStoreOperationKind.Delete:
                        await this._fileSyncContext.MetadataStore.PurgeAsync(storeOperationEvent.Operation.TableName, storeOperationEvent.Operation.RecordId);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception exception)
            {

            }
        }
    }
}
