using System;
using Xamarin.Forms;

namespace TodoSampleMobile.CustomViews
{
    public class VideoView : View
    {
        public Action StopAction;
        public Action StartAction;
        public Action SetSourceAction;

        public static readonly BindableProperty FileSourceProperty =
            BindableProperty.Create("FileSource", typeof(string), typeof(VideoView),
                string.Empty,BindingMode.Default,null, propertyChanged: OnFileSourceChanged);

        private static void OnFileSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var videoView = bindable as VideoView;
            videoView?.SetSource();
        }

        public string FileSource
        {
            get { return (string)GetValue(FileSourceProperty); }
            set { SetValue(FileSourceProperty, value); }
        }

        public void SetSource()
        {
            SetSourceAction?.Invoke();
        }

        public void Stop()
        {
            StopAction?.Invoke();
        }

        public void Start()
        {
            StartAction?.Invoke();
        }
    }
}