using TodoSampleMobile.Base;
using Xamarin.Forms;

namespace TodoSampleMobile.Menu
{
    public class MasterDetailPageViewModel : BaseViewModel
    {
        #region P R I V A T E

        private bool _isPresented;


        public bool IsPresented
        {
            get { return _isPresented; }
            set
            {
                _isPresented = value;
                OnPropertyChanged(nameof(IsPresented));
            }
        }

        #endregion

        #region C O N S T R U C T O R S

        public MasterDetailPageViewModel()
        {
            MessagingCenter.Subscribe<object>(this, "ToggelMenu", sender => ToggelMenu());
        }

        public void ToggelMenu()
        {
            IsPresented = !IsPresented;
        }

        #endregion

     
    }
}