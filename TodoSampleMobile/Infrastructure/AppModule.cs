using System;
using Autofac;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using TodoSampleMobile.Domain.BusinessService;
using TodoSampleMobile.Domain.BusinessService.Interfaces;
using TodoSampleMobile.Domain.Infrastructure;
using TodoSampleMobile.Helpers;
using TodoSampleMobile.Login;
using TodoSampleMobile.Menu;
using TodoSampleMobile.Services;
using TodoSampleMobile.Services.Authentication;
using TodoSampleMobile.Services.GeoLocation;
using TodoSampleMobile.Services.Media;
using TodoSampleMobile.Services.Navigation;
using TodoSampleMobile.Services.RemoteNotification;
using TodoSampleMobile.Services.Reports;
using TodoSampleMobile.Services.RSS;
using TodoSampleMobile.TodoDetails;
using TodoSampleMobile.ViewModels;
using TodoSampleMobile.Views;

namespace TodoSampleMobile.Infrastructure
{
    public class AppModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterRepositories(builder);
            RegisterServices(builder);
            RegisterViewModels(builder);
            RegisterViews(builder);
            // RegisterSqLiteConnection(builder);
        }

        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>))
                .As(typeof(IGenericRepository<>))
                .InstancePerLifetimeScope();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.Register<IMobileServiceClient>(
                context => new MobileServiceClient(AppConstants.ApplicationUrl, new AuthHandler())).SingleInstance();
            builder.Register<INavigation>(
                context => ((Application.Current.MainPage) as MasterDetailPage).Detail.Navigation).SingleInstance();
            builder.Register<IRemoteNotifications>(context => DependencyService.Get<IRemoteNotifications>()).SingleInstance();
            builder.Register<IAuthenticator>(context => DependencyService.Get<IAuthenticator>()).SingleInstance();
            builder.Register<IGeolocator>(context => DependencyService.Get<IGeolocator>()).SingleInstance();
            builder.Register<IReportRenderer>(context => DependencyService.Get<IReportRenderer>()).SingleInstance();
            builder.Register<IMediaPicker>(context => DependencyService.Get<IMediaPicker>()).SingleInstance();
            builder.RegisterType<AzureInitializer>().As<IAzureInitializer>().SingleInstance();
            builder.RegisterType<ViewFactory>().As<IViewFactory>().SingleInstance();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<TodoItemsService>().As<ITodoItemsService>().SingleInstance();
            builder.RegisterType<UserService>().As<IUserService>().SingleInstance(); 
            builder.RegisterType<SyncService>().As<ISyncService>().SingleInstance();
            
            builder.RegisterGeneric(typeof(RssManger<>)).As(typeof(IRssManger<>)).InstancePerLifetimeScope();
            builder.Register(c => DependencyService.Get<ILocalNotificationService>()).As<ILocalNotificationService>();
        }

        private static void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(LoginPageViewModel));
            builder.RegisterType(typeof(MainPageViewModel));
            builder.RegisterType(typeof(MenuPageViewModel));
            builder.RegisterType(typeof(TodoDetailsViewModel));
            builder.RegisterType(typeof(MasterDetailPageViewModel));
        }

        private static void RegisterViews(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(LoginPage));
            builder.RegisterType(typeof(TodoDetailsPage));
            builder.RegisterType(typeof(MainPage));
            builder.RegisterType(typeof(MenuPage)); 
        }

        private static void RegisterSqLiteConnection(ContainerBuilder builder)
        {
            var appRuntimeSqLiteConnector = GetAppSqLiteImplementor();
            builder.RegisterInstance(appRuntimeSqLiteConnector).AsImplementedInterfaces().SingleInstance();
            var sqlLiteConnection = appRuntimeSqLiteConnector.GetConnection();
            builder.RegisterInstance(sqlLiteConnection).AsSelf().SingleInstance();
        }

        private static ISqLiteConnector GetAppSqLiteImplementor()
        {
            var platformSpecificSettings = DependencyService.Get<ISqLiteConnector>();
            if (platformSpecificSettings == null)
            {
                throw new InvalidOperationException(
                    $"Missing '{typeof(ISqLiteConnector).FullName}' implementation! Implementation is required.");
            }
            return platformSpecificSettings;
        }
    }
}