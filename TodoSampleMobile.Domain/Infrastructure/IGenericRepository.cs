using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.Files;
using TodoSampleMobile.Domain.AzureModels;
using Microsoft.WindowsAzure.MobileServices;

namespace TodoSampleMobile.Domain.Infrastructure
{
    public interface IGenericRepository<T> where T : class 
    {
        Task SyncAsync(string queryName, Expression<Func<T, bool>> criteria = null);
        Task PurgeAsync(string queryName);
        Task UpdateItemAsync(T model);
        Task AddItemAsync(T model);

        Task DeleteItemAsync(T model);
        Task<ObservableCollection<T>> GetItemsAsync(Expression<Func<T, bool>> criteria = null);
        Task<ObservableCollection<T>> GetItemsAsync<TKey>(Enums.OrderTypes orderType, Expression<Func<T, TKey>> orderBy,
            Expression<Func<T, bool>> criteria = null);
        Task<T> GetItemAsync(Expression<Func<T, bool>> criteria = null);
        Task<MobileServiceFile> AddFileAsync(T model, string targetPath);
        Task DeleteFileAsync(MobileServiceFile file);
        Task<IEnumerable<MobileServiceFile>> GetFilesAsync(T model);
    }
}