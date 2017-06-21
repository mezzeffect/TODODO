using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TodoSampleMobile.Base;
using TodoSampleMobile.Services.Navigation;
using TodoSampleMobile.ViewModels;
using Xamarin.Forms;
using XLabs.Forms.Services;

namespace TodoSampleMobile.TodoDetails
{
    public class TodoDetailsViewModel:BaseViewModel
    {
        #region P R I V A T E


        #endregion

        #region C O N S T R U C T O R S

        public TodoDetailsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        #endregion

        #region P R O P E R T I E S

        private Services.RSS.News _newsItem;

        public Services.RSS.News NewsItem
        {
            get { return _newsItem; }
            set
            {
                _newsItem = value;
                OnPropertyChanged(nameof(NewsItem));
            }
        }
        #endregion

        #region C O M M A N D S

        private ICommand _goBackCommand;
        private readonly INavigationService _navigationService;
        public ICommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new Command(HandleGoBackCommand));

        private async void HandleGoBackCommand()
        {
            await _navigationService.PushAsync<MainPageViewModel>(null);
        }

        private ICommand _openInBrowserCommand;
        public ICommand OpenInBrowserCommand => _openInBrowserCommand ?? (_openInBrowserCommand = new Command(HandleOpenInBrowserCommand));

        private async void HandleOpenInBrowserCommand()
        {
            Device.OpenUri(new Uri(NewsItem.Url));
        }

        #endregion

        #region D E L E G A T E S

        public override Task Init(object initParam)
        {
            NewsItem = initParam as Services.RSS.News;
            return base.Init(initParam);
        }

        #endregion

    }
}
