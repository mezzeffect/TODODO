using Autofac;
using TodoSampleMobile.Services;
using System;
using TodoSampleMobile.Domain.Infrastructure;
using TodoSampleMobile.Services.Authentication;
using TodoSampleMobile.Services.GeoLocation;
using TodoSampleMobile.Helpers;
using TodoSampleMobile.Login;
using TodoSampleMobile.TodoDetails;
using TodoSampleMobile.ViewModels;
using TodoSampleMobile.Views;

namespace TodoSampleMobile.Infrastructure
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            var builder = new ContainerBuilder();
            ConfigureContainer(builder);
            MapViews();
            ConfigureApplication();
        }

        private static void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<AppModule>();
            AppAutoFac.Container = builder.Build();
        }

        private static void MapViews()
        {
            AppAutoFac.ResolvedIViewFactory.Register<MainPageViewModel, MainPage>();
            AppAutoFac.ResolvedIViewFactory.Register<LoginPageViewModel, LoginPage>();
            AppAutoFac.ResolvedIViewFactory.Register<MenuPageViewModel, MenuPage>();
            AppAutoFac.ResolvedIViewFactory.Register<TodoDetailsViewModel, TodoDetailsPage>();
        }

        private static async void ConfigureApplication()
        {
            try
            { 

                NavigationHelper.NavigateToLoginPage();
            
            }
            catch (Exception e)
                {
                }
            }
    }
}