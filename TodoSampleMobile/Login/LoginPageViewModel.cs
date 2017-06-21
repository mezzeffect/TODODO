using System;
using System.Windows.Input;
using TodoSampleMobile.Services;
using Xamarin.Forms;
using TodoSampleMobile.Domain.AzureModels;
using TodoSampleMobile.Services.GeoLocation;
using TodoSampleMobile.Services.RemoteNotification;
using TodoSampleMobile.Base;
using TodoSampleMobile.Domain.BusinessService.Interfaces;
using TodoSampleMobile.Domain.Infrastructure;
using TodoSampleMobile.Helpers;
using TodoSampleMobile.Services.Authentication;

namespace TodoSampleMobile.Login
{
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly IAuthenticator _authenticator;
        private IUserService _userService;

        private bool _isEnabled;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        private bool _isIndicatorRunning;
        public bool IsIndicatorRunning
        {
            get { return _isIndicatorRunning; }
            set
            {
                _isIndicatorRunning = value;
                OnPropertyChanged(nameof(IsIndicatorRunning));
            }
        }


        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public LoginPageViewModel(IAuthenticator authenticator, IAzureInitializer azureInitializer,
            IUserService userService)
        {
            _authenticator = authenticator;
            _userService = userService;

            IsEnabled = true;
            LoginCommand = new Command(async () =>
            {
                try
                {
                    IsEnabled = false;
                    IsIndicatorRunning = true;
                    var isAuth = false;
                    if (!string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(UserName))
                        isAuth = await authenticator.AuthenticateAsync(new LoginObject() { Password = this.Password, UserName = this.UserName });

                    if (!isAuth)
                    {
                        IsEnabled = true;
                        IsIndicatorRunning = false;
                        return;
                    }

                    await azureInitializer.SyncClientAndStore();

                    App.UserName = UserName;

                    NavigationHelper.NavigateToHomePage();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    throw;
                }
            });
        }

        public ICommand LoginCommand { get; }

    }
}