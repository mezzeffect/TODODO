using System.Diagnostics;
using System.Threading.Tasks;
using TodoSampleMobile.Services;
using TodoSampleMobile.Services.Files;
using Plugin.Connectivity;
using TodoSampleMobile.Domain.AzureModels;
using TodoSampleMobile.Domain.BusinessService.Interfaces;
using TodoSampleMobile.Domain.Infrastructure;
using Xamarin.Forms;

namespace TodoSampleMobile.Domain.BusinessService
{
    public class SyncService : ISyncService
    {
        private readonly IGenericRepository<TodoItem> _todoItemRepository;

        public SyncService(
            IGenericRepository<TodoItem> todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        public bool IsSyncing { get; set; }

        public async Task SyncAll()
        {
            if (!CrossConnectivity.Current.IsConnected)
                return;
            if (IsSyncing)
                return;

            IsSyncing = true;
            try
            {
                await _todoItemRepository.SyncAsync(Constants.TodoQueryName);
                await CheckContentsOfFullTaskView();
            }
            catch (System.Exception exception)
            {
                Debug.WriteLine(exception.Message);
                throw;
            }
            finally
            {
                IsSyncing = false;
            }
        }

        private async Task CheckContentsOfFullTaskView()
        {
            await _todoItemRepository.GetItemsAsync(i => !i.Deleted);
        }

        public async Task PurgeDataAsync()
        {
            await _todoItemRepository.PurgeAsync(Constants.TodoQueryName);
        }
    }
}