using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TodoSampleMobile.Domain.AzureModels;

namespace TodoSampleMobile.Domain.BusinessService.Interfaces
{

    public interface IUserService
    {
        Task<bool> IsAdmin(string email);
        Task SyncAsync();
    }
}