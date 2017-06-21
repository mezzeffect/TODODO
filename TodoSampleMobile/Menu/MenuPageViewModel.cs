using TodoSampleMobile.Services;
using System.Windows.Input;
using Plugin.Connectivity;
using Xamarin.Forms;
using System;
using TodoSampleMobile.Infrastructure;
using TodoSampleMobile.Base;
using TodoSampleMobile.Domain.BusinessService.Interfaces;
using TodoSampleMobile.Helpers;
using TodoSampleMobile.Services.Authentication;
using TodoSampleMobile.Services.Navigation;

namespace TodoSampleMobile.ViewModels
{
    public class MenuPageViewModel : BaseViewModel
    {
        #region P R I V A T E

        private bool _isSyncing;
       
        private readonly INavigationService _navigationService;

        private string _email;
     
        private string _name;
        private ISyncService _syncService;


        #endregion

        #region C O N S T R U C T O R S
        public MenuPageViewModel(IAuthenticator authenticator, ISyncService syncService, INavigationService navigationService)
        {
            _syncService = syncService;
            _navigationService = navigationService;
            _authenticator = authenticator;
        }


        #endregion

        #region P R O P E R T I E S

        public bool IsSyncing
        {
            get { return _isSyncing; }
            set
            {
                _isSyncing = value;
                OnPropertyChanged(nameof(IsSyncing));
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private bool _isSyncNews;

        public bool IsSyncNews
        {
            get { return _isSyncNews; }
            set
            {
                _isSyncNews = value;
                OnPropertyChanged(nameof(IsSyncNews));
            }
        }

        private int _newCount;

        public int NewsCount
        {
            get { return _newCount; }
            set
            {
                _newCount = value;
                OnPropertyChanged(nameof(NewsCount));
            }
        }


        #endregion

        #region C O M M A N D S

        private ICommand _syncCommand;
        private ICommand _logoutCommand;
        public ICommand LogoutCommand
            => _logoutCommand ?? (_logoutCommand = new Command(ExecuteLogoutCommand));
        public ICommand SyncCommand => _syncCommand ?? (_syncCommand = new Command(ExecuteSyncCommand));

        private ICommand _toggelMenuCommand;
        private IAuthenticator _authenticator;
        public ICommand ToggelMenuCommand => _toggelMenuCommand ?? (_toggelMenuCommand = new Command(HandleToggelMenuCommand));

        


        
        #endregion

        #region D E L E G A T E S
        private async void handleGoToNewsCommand()
        {
            if (!IsSyncNews)
            {
                MessagingCenter.Send<object>(this, "ToggelMenu");
            }
        }
        private async void HandleToggelMenuCommand()
        {
            MessagingCenter.Send<object>(this, "ToggelMenu");
        }
        private async void ExecuteSyncCommand()
        {
            if (IsSyncing)
                return;
            IsSyncing = true;
            try
            {
                await _syncService.SyncAll();
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
            IsSyncing = false;
            NavigationHelper.NavigateToHomePage();
        }

        private async void ExecuteLogoutCommand()
        {
            await _authenticator.Unauthenticate();
            NavigationHelper.NavigateToLoginPage();
        }
        #endregion
    }
}