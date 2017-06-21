using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TodoSampleMobile.Domain.AzureModels;
using TodoSampleMobile.Domain.BusinessService.Interfaces;
using TodoSampleMobile.Domain.Infrastructure;

namespace TodoSampleMobile.Domain.BusinessService
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;

        public UserService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<bool> IsAdmin(string email)
        {
            ObservableCollection<User> users = await _userRepository.GetItemsAsync();

            User user = await _userRepository.GetItemAsync(u => u.Email.ToLower() == email.ToLower());

            return (user != null)? true : false;
        }

        public async Task SyncAsync()
        {
            await _userRepository.SyncAsync(Constants.UserQueryName);
        }
    }
}