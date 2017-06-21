using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TodoSampleMobile.Services.Annotations;
using TodoSampleMobile.Services.Navigation;

namespace TodoSampleMobile.Base
{
    public class BaseViewModel:INotifyPropertyChanged, IViewModel
    {
        public string Title { get; set; }

        public void SetState<T>(Action<T> action) where T : class, IViewModel
        {
            action(this as T);
        }

        public virtual async Task Init(object initParam)
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}