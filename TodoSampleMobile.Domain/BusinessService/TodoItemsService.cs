using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TodoSampleMobile.Domain.AzureModels;
using TodoSampleMobile.Domain.BusinessService.Interfaces;
using TodoSampleMobile.Domain.Infrastructure;

namespace TodoSampleMobile.Domain.BusinessService
{
    public class TodoItemsService : ITodoItemsService
    {
        private readonly IGenericRepository<TodoItem> _todoRepository;

        public TodoItemsService(IGenericRepository<TodoItem> todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task SaveTodoItem(TodoItem todoItem)
        {
            if (todoItem.Id != null)
                await _todoRepository.UpdateItemAsync(todoItem);
            else
                await _todoRepository.AddItemAsync(todoItem);
        }

        public async Task DeleteTodoItem(TodoItem todoItem)
        {
            await _todoRepository.DeleteItemAsync(todoItem);
        }

        public async Task<ObservableCollection<TodoItem>> GetTodoItems()
        {
            return await _todoRepository.GetItemsAsync();
        }
    }
}