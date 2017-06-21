using System;
using System.Threading.Tasks;

namespace TodoSampleMobile.Services.Navigation
{
    public interface IViewModel
    {
        string Title { get; set; }
        void SetState<T>(Action<T> action) where T : class, IViewModel;
        Task Init(object initParam);
    }
}
