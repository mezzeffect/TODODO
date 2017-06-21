using System;
using TodoSampleMobile.Infrastructure;
using TodoSampleMobile.Login;
using TodoSampleMobile.Menu;
using TodoSampleMobile.ViewModels;
using Xamarin.Forms;

namespace TodoSampleMobile.Helpers
{
    public static class NavigationHelper
    {
        public static void NavigateToHomePage()
        {
            try { 
            var detailPage = AppAutoFac.ResolvedIViewFactory.Resolve<MainPageViewModel>();
            
            var masterDetails = new MasterDetailPage
            {
                Master = AppAutoFac.ResolvedIViewFactory.Resolve<MenuPageViewModel>(),
                Detail = new NavigationPage(detailPage) { BarTextColor = Color.Black }
            };

            masterDetails.SetBinding(MasterDetailPage.IsPresentedProperty, "IsPresented", BindingMode.TwoWay);
            masterDetails.BindingContext = AppAutoFac.Resolve<MasterDetailPageViewModel>();
            Application.Current.MainPage = masterDetails;
}
            catch(Exception e)
            {

            }

        }

        public static void NavigateToLoginPage()
        {
            try
            {
                Application.Current.MainPage = AppAutoFac.ResolvedIViewFactory.Resolve<LoginPageViewModel>();
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }
    }
}