using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTracker.Domain.Infrastructure;
using Task = iTracker.Models.Task;

namespace iTracker.Domain.BusinessService
{
    public class BaseService<T> where T : BaseAzureModel
    {
        #region P R I V A T E

        private readonly IGenericRepository<T> _planRepository;

        #endregion

        #region C O N S T R U C T O R S
        public BaseService(IGenericRepository<T> planRepository)
        {
            _planRepository = planRepository;
        }
        #endregion

        #region P U B L I C  M E T H O D S
        public async Task<ObservableCollection<T>> GetItemsAsync(bool syncItems = false)
        {
            return await _planRepository.GetItemsAsync();
        }

        public async System.Threading.Tasks.Task SaveTaskAsync(T item)
        {
            await _planRepository.SaveTaskAsync(item);
        }

        public async System.Threading.Tasks.Task SyncAsync()
        {
            await _planRepository.SyncAsync(Constants.TaskQueryName);
        }


        #endregion
    }
}
