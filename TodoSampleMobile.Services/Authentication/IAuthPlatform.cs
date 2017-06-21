using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Auth;

namespace TodoSampleMobile.Services.Authentication
{
    public interface IAuthPlatform
    {
        Task<AuthenticationResult> GetAuthenticationResultAsync();
        AccountStore GetAccountStore();
        void Logout();
    }
}
