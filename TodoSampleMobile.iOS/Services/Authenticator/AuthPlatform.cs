using System;
using System.Threading.Tasks;
using Foundation;
using TodoSampleMobile.iOS.Services.Authenticator;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using TodoSampleMobile.Services.Authentication;
using UIKit;
using Xamarin.Auth;

[assembly: Xamarin.Forms.Dependency(typeof(AuthPlatform))]
namespace TodoSampleMobile.iOS.Services.Authenticator
{

    public class AuthPlatform : IAuthPlatform
    {
        public async Task<AuthenticationResult> GetAuthenticationResultAsync()
        {
           throw new NotImplementedException();
        }

        public AccountStore GetAccountStore()
        {
            return AccountStore.Create();
        }

        public void Logout()
        {
            foreach (var cookie in NSHttpCookieStorage.SharedStorage.Cookies)
            {
                NSHttpCookieStorage.SharedStorage.DeleteCookie(cookie);
            }
        }
    }
}