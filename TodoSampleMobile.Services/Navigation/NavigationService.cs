using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TodoSampleMobile.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly Lazy<INavigation> _navigation;
        private readonly IViewFactory _viewFactory;

        public NavigationService(Lazy<INavigation> navigation, IViewFactory viewFactory)
        {
            _navigation = navigation;
            _viewFactory = viewFactory;
        }

        private INavigation Navigation => _navigation.Value;

        #region Pop
        public async Task<IViewModel> PopAsync()
        {
            var view = await Navigation.PopAsync();
            return view.BindingContext as IViewModel;
        }

        public async Task<IViewModel> PopModalAsync()
        {
            var view = await Navigation.PopAsync();
            return view.BindingContext as IViewModel;
        }

        public async Task PopToRootAsync()
        {
            await Navigation.PopToRootAsync();
        }
        #endregion

        #region Push
        public async Task<TViewModel> PushAsync<TViewModel>()
          where TViewModel : class, IViewModel
        {
            TViewModel viewModel;
            var view = _viewFactory.Resolve(out viewModel);
            await Navigation.PushAsync(view);
            return viewModel;
        }

        public async Task<TViewModel> PushAsync<TViewModel>(object initParam)
            where TViewModel : class, IViewModel
        {
            TViewModel viewModel;
            var view = _viewFactory.Resolve(out viewModel);
            await Navigation.PushAsync(view);
            await viewModel.Init(initParam);
            return viewModel;
        }

        public async Task<TViewModel> PushAsync<TViewModel>(TViewModel viewModel)
            where TViewModel : class, IViewModel
        {
            var view = _viewFactory.ResolveByInstance(viewModel);
            await Navigation.PushAsync(view);
            return viewModel;
        }

        #endregion

        #region Push Modal

        public async Task<TViewModel> PushModalAsync<TViewModel>()
         where TViewModel : class, IViewModel
        {
            TViewModel viewModel;
            var view = _viewFactory.Resolve(out viewModel);
            await Navigation.PushModalAsync(view);
            return viewModel;
        }

        public async Task<TViewModel> PushModalAsync<TViewModel>(object initParam)
            where TViewModel : class, IViewModel
        {
            TViewModel viewModel;
            var view = _viewFactory.Resolve(out viewModel);
            await Navigation.PushModalAsync(view);
            await viewModel.Init(initParam);
            return viewModel;
        }

        public async Task<TViewModel> PushModalAsync<TViewModel>(TViewModel viewModel)
            where TViewModel : class, IViewModel
        {
            var view = _viewFactory.ResolveByInstance(viewModel);
            await Navigation.PushModalAsync(view);
            return viewModel;
        } 
        #endregion
    }
}
