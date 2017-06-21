using Xamarin.Forms;

namespace TodoSampleMobile.Services.Navigation
{
    public interface IViewFactory
    {
        void Register<TViewModel, TView>()
            where TViewModel : class, IViewModel
            where TView : Page;

        Page Resolve<TViewModel>()
            where TViewModel : class, IViewModel;

        Page Resolve<TViewModel>(out TViewModel viewModel)
            where TViewModel : class, IViewModel;

        Page ResolveByInstance<TViewModel>(TViewModel viewModel)
            where TViewModel : class, IViewModel;
    }
}