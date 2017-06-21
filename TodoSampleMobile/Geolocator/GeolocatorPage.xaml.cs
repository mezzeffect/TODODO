using Xamarin.Forms;

namespace TodoSampleMobile.Geolocator
{
    public partial class GeolocatorPage : ContentPage
    {
        public GeolocatorPage()
        {
            InitializeComponent();
            BindingContext =  new GeolocatorViewModel();
        }
    }
}
