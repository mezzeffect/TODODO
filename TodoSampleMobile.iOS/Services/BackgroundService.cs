using System;
using TodoSampleMobile.iOS.Services;
using TodoSampleMobile.Services;
using UIKit;
[assembly: Xamarin.Forms.Dependency(typeof(BackgroundService))]
namespace TodoSampleMobile.iOS.Services
{
    public class BackgroundService: IBackgroundService
    {
        public int RegisterForBackground()
        {
            nint taskId = UIApplication.BackgroundTaskInvalid;

            taskId = UIApplication.SharedApplication.BeginBackgroundTask(() =>
            {
                UIApplication.SharedApplication.EndBackgroundTask(taskId);
            });

            return (int)taskId;
        }
        public void EndBackgroundTask(int taskId)
        {
            UIApplication.SharedApplication.EndBackgroundTask((nint)taskId);
        }
    }
}