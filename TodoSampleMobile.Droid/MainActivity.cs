using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.WindowsAzure;

namespace TodoSampleMobile.Droid
{
    //[Activity(Label = "Tododo", Icon = "@drawable/logo", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [Activity(Label = "@string/ApplicationName")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            //Azure Mobile Services
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            var height =Resources.DisplayMetrics.HeightPixels/Resources.DisplayMetrics.Density;
            var width = Resources.DisplayMetrics.WidthPixels/Resources.DisplayMetrics.Density;
            LoadApplication(new App((int)height, (int)width));
        }
    }
}

