using System.Collections.ObjectModel;
using TodoSampleMobile.Domain.AzureModels;

namespace TodoSampleMobile.Domain.BusinessService.Interfaces
{
    public interface IBaseService<T> where T : BaseAzureModel
    {
        System.Threading.Tasks.Task<ObservableCollection<T>> GetItemsAsync(bool syncItems = false);
        System.Threading.Tasks.Task SaveTaskAsync(T item);
        System.Threading.Tasks.Task SyncAsync();
    }
}