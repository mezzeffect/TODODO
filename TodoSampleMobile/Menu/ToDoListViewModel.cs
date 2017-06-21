using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using iTracker.Base;
using iTracker.Domain;
using iTracker.Domain.AzureModels;
using iTracker.Domain.BusinessService;
using iTracker.Models;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using User = iTracker.Domain.AzureModels.User;

namespace iTracker.ViewModels
{
    public class ToDoListViewModel : BaseViewModel
    {
        #region P R I V A T E

        private readonly IToDoService _service;
        private readonly IUserService _userservice;
        private ICommand _todoLstOnRefreshCommand;
        private ICommand _todoLstItemSelectedCommand;
        private ICommand _addClickedCommand;
        private ICommand _menuItemClickedCommand;
        private ICommand _appearingCommand;
        private ObservableCollection<TodoItem> _items;

        #endregion

        #region C O N S T R U C T O R S
        public ToDoListViewModel(IToDoService service, IUserService userService)
        {
            _service = service;
            _userservice = userService;
        }

        #endregion

        #region P R O P E R T I E S

        public ObservableCollection<TodoItem> Items
        {
            get { return _items; }
            // ReSharper disable once ExplicitCallerInfoArgument
            set { _items = value; OnPropertyChanged(nameof(Items)); }
        }


        #endregion

        #region C O M M A N D S
        public ICommand AppearingCommand
            => _appearingCommand ?? (_appearingCommand = new Command(HndlAppearing));
        public ICommand AddClickedCommand
            => _addClickedCommand ?? (_addClickedCommand = new Command<ToDoItem>(AddItem));

        public ICommand TodoLstItemSelectedCommand
            => _todoLstItemSelectedCommand ?? (_todoLstOnRefreshCommand = new Command(HndlTodoSelection));

        public ICommand TodoLstOnRefreshCommand
            => _todoLstOnRefreshCommand ?? (_todoLstOnRefreshCommand = new Command(SyncLst));

        public ICommand MenuItemClickedCommand 
            => _menuItemClickedCommand ?? (_menuItemClickedCommand = new Command<ToDoItem>(HndlMenueClicked));

        #endregion

        #region D E L E G A T E S

        private async void HndlTodoSelection()
        {
            //throw new NotImplementedException();
        }

        private async void AddItem(ToDoItem obj)
        {
            try
            {
                await _userservice.SaveTaskAsync(new User() { Name = "kkk" });
                await _userservice.SyncAsync();
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message+ex.InnerException.Message);
            }
           
        }

        private void HndlMenueClicked(ToDoItem obj)
        {
            // throw new NotImplementedException();
        }

        private async void SyncLst()
        {
            Items = await _service.GetItemsAsync();
        }

        private async void HndlAppearing(object obj)
        {

            Items = await _service.GetItemsAsync();
        }
        #endregion
    }
}