
using Foundation;
using TodoSampleMobile.iOS.Services;
using TodoSampleMobile.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocalNotificationService))]
namespace TodoSampleMobile.iOS.Services
{
   public  class LocalNotificationService:ILocalNotificationService
    {
       public bool Send(string title, string description, int badge)
       {
            try
            {
                var notification = new UILocalNotification();

                // set the fire date (the date time in which it will fire)
                notification.FireDate = NSDate.FromTimeIntervalSinceNow(1);

                // configure the alert
                notification.AlertAction = title;
                notification.AlertBody = description;

                // modify the badge
                notification.ApplicationIconBadgeNumber = badge;

                // set the sound to be the default sound
                notification.SoundName = UILocalNotification.DefaultSoundName;

                // schedule it
                UIApplication.SharedApplication.ScheduleLocalNotification(notification);
            }
            catch (System.Exception)
            {
                return false;
                throw;
            }
           return true;
       }
    }
}