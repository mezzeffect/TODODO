
#define OFFLINE_SYNC_ENABLED
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTracker.Domain.AzureModels;
using iTracker.Domain.Infrastructure;

namespace iTracker.Domain.BusinessService
{
    public class UserService :BaseService<User>, IUserService
    {
        #region P R I V A T E

        private readonly IGenericRepository<User> _userRepository;

        #endregion

        #region C O N S T R U C T O R S
        public UserService(IGenericRepository<User> userRepository):base(userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        #region P U B L I C  M E T H O D S
       

        #endregion

        public Task<ObservableCollection<PlanAndTasksModel>> GetPlansWithTasks(string id)
        {
            throw new NotImplementedException();
        }
    }
}
