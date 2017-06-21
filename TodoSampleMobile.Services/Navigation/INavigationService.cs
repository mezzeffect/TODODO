using System.Threading.Tasks;

namespace TodoSampleMobile.Services.Navigation
{
    public interface INavigationService
    {
        Task<IViewModel> PopAsync();

        Task<IViewModel> PopModalAsync();

        Task PopToRootAsync();

        Task<TViewModel> PushAsync<TViewModel>()
            where TViewModel : class, IViewModel;

        Task<TViewModel> PushAsync<TViewModel>(object initParam)
            where TViewModel : class, IViewModel;

        Task<TViewModel> PushAsync<TViewModel>(TViewModel viewModel)
            where TViewModel : class, IViewModel;

        Task<TViewModel> PushModalAsync<TViewModel>()
            where TViewModel : class, IViewModel;

        Task<TViewModel> PushModalAsync<TViewModel>(object initParam)
            where TViewModel : class, IViewModel;

        Task<TViewModel> PushModalAsync<TViewModel>(TViewModel viewModel)
            where TViewModel : class, IViewModel;
    }
}
