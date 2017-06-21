using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TodoSampleMobile.Helpers;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using TodoSampleMobile.Domain.Infrastructure;
using TodoSampleMobile.Domain.BusinessService.Interfaces;
using TodoSampleMobile.Infrastructure;
using TodoSampleMobile.Services.Authentication;

[assembly: Dependency(typeof(Authenticator))]

namespace TodoSampleMobile.Helpers
{
    public class Authenticator : IAuthenticator
    {
        private UserInfoModel _currentUser;
        private readonly IMobileServiceClient _client;
        private IUserService _userService;

        public Authenticator()
        {
            try
            {
                _userService = AppAutoFac.Resolve<IUserService>();
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
            _client = AppAutoFac.Resolve<IMobileServiceClient>();
        }

        public bool IsAuthenticated => GetCachedUser() != null;

        public MobileServiceUser AuthenticatedUser { get; set; }

      
        public async Task<bool> AuthenticateAsync(LoginObject login)
        {
            try
            {
                AuthenticatedUser =  await _client.LoginAsync("custom", JObject.FromObject(login));
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR - AUTHENTICATION FAILED {0}", ex.Message);
                return false;
            }
        }

        private async Task<MobileServiceUser> DoLoginAsync()
        {
            var authHandler = DependencyService.Get<IAuthPlatform>();
            var authResult = await authHandler.GetAuthenticationResultAsync();
            
            _currentUser = new UserInfoModel
            {
                UserId = authResult.UserInfo.UniqueId,
                AccessToken = authResult.AccessToken,
                Email = authResult.UserInfo.DisplayableId,
                Name = authResult.UserInfo.GivenName + " " + authResult.UserInfo.FamilyName

                //,
                //IsAdmin = await _userService.IsAdmin(authResult.UserInfo.DisplayableId)
            };
            var payload = new JObject {["access_token"] = authResult.AccessToken};
            return await _client.LoginAsync(MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory, payload);
        }

        private MobileServiceUser GetCachedUser()
        {
            
            return AuthenticatedUser;
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

        public async Task Unauthenticate()
        {
            await _client.LogoutAsync();
            await AppAutoFac.Resolve<ISyncService>().PurgeDataAsync();
        }

       
    }
}