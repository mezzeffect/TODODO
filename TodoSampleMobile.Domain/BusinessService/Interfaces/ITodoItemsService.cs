using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TodoSampleMobile.Domain.AzureModels;

namespace TodoSampleMobile.Domain.BusinessService.Interfaces
{
    public interface ITodoItemsService 
    {
        Task SaveTodoItem(TodoItem todoItem);
        Task DeleteTodoItem(TodoItem todoItem);
        Task<ObservableCollection<TodoItem>> GetTodoItems();
    }
}