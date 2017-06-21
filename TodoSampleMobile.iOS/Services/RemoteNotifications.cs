using TodoSampleMobile.iOS.Services;
using UIKit;
using Xamarin.Forms;
using TodoSampleMobile.Services.RemoteNotification;

[assembly: Dependency(typeof(RemoteNotifications))]

namespace TodoSampleMobile.iOS.Services
{
    public class RemoteNotifications : IRemoteNotifications
    {
        public void RegisterForRemoteNotifications()
        {
            
            UIApplication.SharedApplication.RegisterForRemoteNotifications();
        }
    }
}