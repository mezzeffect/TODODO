using Foundation;
using TodoSampleMobile.CustomViews;
using TodoSampleMobile.iOS.CustomRenders;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(DisabledButton), typeof(DisabledButtonRenderer))]
namespace TodoSampleMobile.iOS.CustomRenders
{
    public class DisabledButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (Control == null) return;
            Control.Enabled = false;
            Control.SetTitleColor(UIColor.White, UIControlState.Disabled);
        }
    }
}