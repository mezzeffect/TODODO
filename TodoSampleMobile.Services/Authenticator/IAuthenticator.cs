using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure.MobileServices;

namespace iTracker.Services.Authentication
{
    public interface IAuthenticator
    {
        bool IsAuthenticated { get; }
        MobileServiceUser AuthenticatedUser { get; set; }
        UserInfoModel CurrentUser { get; }
        Task<bool> AuthenticateAsync();
        void Unauthenticate();
        TokenCacheItem GetCurrentUser();
    }
}