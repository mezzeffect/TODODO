using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TodoSampleMobile.ViewModels;

namespace TodoSampleMobile.Views
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            ((MainPageViewModel)this.BindingContext).AppearingCommand.Execute(null);
        }

        private void MainList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var list = sender as ListView;
            list.SelectedItem = null;
        }
    }
}
