using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TodoSampleMobile.TodoDetails
{
    public partial class TodoDetailsPage : ContentPage
    {
        public TodoDetailsPage()

        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            //Task.Run(async () => await MainView.ScaleTo(1, 1000));

        }
    }
}
