using System;
using System.Collections.Generic;
using Autofac;
using Xamarin.Forms;

namespace TodoSampleMobile.Services.Navigation
{
    public class ViewFactory : IViewFactory
    {
        private readonly IDictionary<Type, Type> _map = new Dictionary<Type, Type>();
        private readonly IComponentContext _componentContext;

        public ViewFactory(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public void Register<TViewModel, TView>()
            where TViewModel : class, IViewModel
            where TView : Page
        {
            _map[typeof(TViewModel)] = typeof(TView);
        }

        public Page Resolve<TViewModel>() where TViewModel : class, IViewModel
        {
            TViewModel viewModel;
            return Resolve(out viewModel);
        }

        public Page Resolve<TViewModel>(out TViewModel viewModel)
            where TViewModel : class, IViewModel
        {
            viewModel = _componentContext.Resolve<TViewModel>();
            return ResolveByInstance(viewModel);
        }

        public Page ResolveByInstance<TViewModel>(TViewModel viewModel)
            where TViewModel : class, IViewModel
        {
            var viewType = _map[typeof(TViewModel)];
            var view = _componentContext.Resolve(viewType) as Page;

            if (view != null)
                view.BindingContext = viewModel;
            return view;
        }
    }
}