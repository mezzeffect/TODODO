using System.Collections.ObjectModel;
using TodoSampleMobile.Domain.AzureModels;
using TodoSampleMobile.Infrastructure;
using Xamarin.Forms;

namespace TodoSampleMobile
{
    public class App : Application
    {

        #region L O C A L S

        public static string UserName;
        public static int Width { get; set; }

        public static int Height { get; set; }

        public static ObservableCollection<TodoItem> TodoTasks { get; set; }
        #endregion
        public App(int height, int width)
        {
            
            SetScreenDimensions(height, width);
            /*todo: resolve IGeolocator
             start listening
             changelocation+= method
             */

            //MainPage =  new CameraPage();
            Bootstrapper.Run();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void SetScreenDimensions(int height, int width)
        {
            Height = height;
            Width = width;
        }


       
    }
}
