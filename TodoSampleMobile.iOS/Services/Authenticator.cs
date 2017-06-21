using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using iTracker.iOS.Services;
using iTracker.Services;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using UIKit;
using Xamarin.Forms;
using iTracker.Infrastructure;
using Microsoft.WindowsAzure.MobileServices;
using iTracker.Domain.BusinessService.Interfaces;
using iTracker.Services.Authentication;

[assembly: Dependency(typeof(Authenticator))]

namespace iTracker.iOS.Services
{
    public class Authenticator : IAuthenticator
    {
        private UserInfoModel _currentUser;
        private readonly IMobileServiceClient _client;
        private IUserService _userService;

        public Authenticator()
        {
            _userService = AppAutoFac.Resolve<IUserService>();
            _client = AppAutoFac.Resolve<IMobileServiceClient>();
        }

        public UserInfoModel CurrentUser
        {
            get
            {
                if (_currentUser != null) return _currentUser;
                var token = GetCurrentUser();
                if (token != null)
                    _currentUser = new UserInfoModel
                    {
                        UserId = token.UniqueId,
                        AccessToken = token.AccessToken,
                        Email = token.DisplayableId,
                        Name = token.GivenName + " " + token.FamilyName
                    };
                return _currentUser;
            }
        }


        public async Task<bool> AuthenticateAsync()
        {
            try
            {
                var controller = UIApplication.SharedApplication.KeyWindow.RootViewController;
                var authContext = new AuthenticationContext(AppConstants.Authority);
                var uri = new Uri(AppConstants.ReturnUri);
                var platformParams = new PlatformParameters(controller);
                var authResult =
                    await
                        authContext.AcquireTokenAsync(AppConstants.Resource, AppConstants.ClientId, uri, platformParams);
               // await _client.LoginAsync(controller, MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory);
                if (authResult == null) return false;

                /*var client = new HttpClient();
                /* OAuth2 is required to access this API. For more information visit:
                   https://msdn.microsoft.com/en-us/office/office365/howto/common-app-authentication-tasks #1#
                // Specify values for path parameters (shown as {...})
                var url = "https://graph.windows.net/integrant.com/users/" + _currentUser.UserId + "/image/$value?" + "api-version=beta";
                var token = await authContext.AcquireTokenSilentAsync(AppConstants.Resource, AppConstants.ClientId);
                var header = token.CreateAuthorizationHeader();
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", header);
                var response = await client.GetAsync(url);
                if (response.Content != null)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseString);
                }*/

                await _userService.SyncAsync();

                _currentUser = new UserInfoModel
                {
                    UserId = authResult.UserInfo.UniqueId,
                    AccessToken = authResult.AccessToken,
                    Email = authResult.UserInfo.DisplayableId,
                    Name = authResult.UserInfo.GivenName + " " + authResult.UserInfo.FamilyName,
                    IsAdmin = await _userService.IsAdmin(authResult.UserInfo.DisplayableId)
            };
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(@"ERROR - AUTHENTICATION FAILED {0}", ex.Message);
                return false;
            }
        }

        public async void Unauthenticate()
        {
            var authContext = new AuthenticationContext(AppConstants.Authority);
            if (authContext.TokenCache.ReadItems().Any())
                authContext.TokenCache.Clear();
            await _client.LogoutAsync();
        }
        

        public TokenCacheItem GetCurrentUser()
        {
            try
            {
                var authContext = new AuthenticationContext(AppConstants.Authority);
                if (authContext.TokenCache.ReadItems().Any())
                    return authContext.TokenCache.ReadItems().FirstOrDefault();
            }
            catch (Exception e)
            {
            }
            return null;
        }
    }
}