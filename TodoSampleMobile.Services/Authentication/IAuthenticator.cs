using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure.MobileServices;

namespace TodoSampleMobile.Services.Authentication
{
    public interface IAuthenticator
    {
        bool IsAuthenticated { get; }
        MobileServiceUser AuthenticatedUser { get; set; }
        Task<bool> AuthenticateAsync(LoginObject login);
        TokenCacheItem GetCurrentUser();
        Task Unauthenticate();
    }
}