using Autofac;
using TodoSampleMobile.Services.Navigation;

namespace TodoSampleMobile.Infrastructure
{
    public class AppAutoFac
    {
        public static IContainer Container { get; set; }

        public static IViewFactory ResolvedIViewFactory => Container.Resolve<IViewFactory>();

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}