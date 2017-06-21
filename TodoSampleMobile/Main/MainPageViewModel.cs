using TodoSampleMobile.Services.GeoLocation;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;
using TodoSampleMobile.Domain;
using TodoSampleMobile.Services;
using TodoSampleMobile.Services.RSS;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Threading.Tasks;
using TodoSampleMobile.TodoDetails;
using TodoSampleMobile.Base;
using TodoSampleMobile.Domain.AzureModels;
using TodoSampleMobile.Domain.BusinessService.Interfaces;
using TodoSampleMobile.Services.Authentication;
using TodoSampleMobile.Services.Navigation;

namespace TodoSampleMobile.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        #region FI E L D S

        private readonly IAuthenticator _authenticator;
        private readonly ITodoItemsService _todoItemsService;
        private readonly INavigationService _navigationService;
        private bool initialized = false;
        private IUserService _userService;

        #endregion

        #region C O N S T R U C T O R S

        public MainPageViewModel(
            IAuthenticator authenticator, 
            INavigationService navigationService, 
            ITodoItemsService todoItemsService,
            IUserService userService, ISyncService syncService)
        {
            _authenticator = authenticator;
            _navigationService = navigationService;
            _todoItemsService = todoItemsService;
            _userService = userService;
            _authenticator = authenticator;
        }

        #endregion

        #region C O M M A N D S



        private ICommand appearingCommand;
        public ICommand AppearingCommand
        {
            get { return appearingCommand = appearingCommand ?? new Command(ExecuteAppearingCommand); }
        }

        private async void ExecuteAppearingCommand()
        {
            #region todos

            TodoItemsList = await _todoItemsService.GetTodoItems();
           
            #endregion
        }

        private ICommand _menuCommand;

        public ICommand MenuCommand => _menuCommand ?? (_menuCommand = new Command(HandleMenuCommand));


        

        private ICommand _doneCheckBoxCheckedCommand;

        public ICommand DoneCheckBoxCheckedCommand => _doneCheckBoxCheckedCommand ?? 
            (_doneCheckBoxCheckedCommand = new Command<TodoItem>(ExecuteDoneCheckBoxCheckedCommand));

        private ICommand _editTodoItemClickedCommand;

        public ICommand EditTodoItemClickedCommand => _editTodoItemClickedCommand ??
                                                      (_editTodoItemClickedCommand = new Command<TodoItem>(ExecuteEditClickedCommand));

        private ICommand _saveTodItemClickedCommand;

        public ICommand SaveTodItemClickedCommand => _saveTodItemClickedCommand ??
                                                     (_saveTodItemClickedCommand = new Command(ExecuteSaveClickedCommand));

        private ICommand _cancelClickedCommand;

        public ICommand CancelClickedCommand => _cancelClickedCommand ??
                                                     (_cancelClickedCommand = new Command(ExecuteCancelClickedCommand));

        private ICommand _confirmTodoItemClickedCommand;

        public ICommand ConfirmDeleteTodItemClickedCommand => _confirmTodoItemClickedCommand ??
                                                       (_confirmTodoItemClickedCommand = new Command(ExecuteConfirmDeleteClickedCommand));

        private ICommand _deleteTodoItemClickedCommand;

        public ICommand DeleteTodoItemClickedCommand => _deleteTodoItemClickedCommand ??
                                                              (_deleteTodoItemClickedCommand = new Command<TodoItem>(ExecuteDeleteTodoItemClickedCommand));

        private ICommand _addTodoItemClickedCommand;

        public ICommand AddTodItemClickedCommand => _addTodoItemClickedCommand ??
                                                              (_addTodoItemClickedCommand = new Command(ExecuteAddClickedCommand));
        #endregion

        #region P R O P E R T I E S
        private TodoItem _selectedTodoItem;

        public TodoItem SelectedTodoItem
        {
            get { return _selectedTodoItem; }
            set
            {
                _selectedTodoItem = value;
                OnPropertyChanged(nameof(SelectedTodoItem));
            }
        }

        private ObservableCollection<TodoItem> _todoItemsList;


        public ObservableCollection<TodoItem> TodoItemsList
        {
            get { return _todoItemsList; }
            set
            {
                _todoItemsList = value;
                OnPropertyChanged(nameof(TodoItemsList));
            }
        }

        private string _title;


        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }


        private string _discription;


        public string Discription
        {
            get { return _discription; }
            set
            {
                _discription = value;
                OnPropertyChanged(nameof(Discription));
            }
        }

        private bool _isDone;


        public bool IsDone
        {
            get { return _isDone; }
            set
            {
                _isDone = value;
                OnPropertyChanged(nameof(IsDone));
            }
        }

        private bool _loading;


        public bool Loading
        {
            get { return _loading; }
            set
            {
                _isDone = value;
                OnPropertyChanged(nameof(Loading));
            }
        }


        private bool _editGridVisibility =false;


        public bool EditGridVisibility
        {
            get { return _editGridVisibility; }
            set
            {
                _editGridVisibility = value;
                OnPropertyChanged(nameof(EditGridVisibility));
            }
        }

        private bool _deleteGridVisibility = false;


        public bool DeleteGridVisibility
        {
            get { return _deleteGridVisibility; }
            set
            {
                _deleteGridVisibility = value;
                OnPropertyChanged(nameof(DeleteGridVisibility));
            }
        }

        private TodoItem _currentTodoItem;


        public TodoItem CurrentTodoItem
        {
            get { return _currentTodoItem; }
            set
            {
                _currentTodoItem = value;
                OnPropertyChanged(nameof(CurrentTodoItem));
            }
        }

        #endregion

        #region M E T H O D S


        private void HandleMenuCommand()
        {
            MessagingCenter.Send<object>(this, "ToggelMenu");
        }

        private void ExecuteDoneCheckBoxCheckedCommand(TodoItem item)
        {
            _todoItemsService.SaveTodoItem(item);
        }

        private void ExecuteEditClickedCommand(TodoItem item)
        {
            CurrentTodoItem = item;
            Title = item.Title;
            Discription = item.Discription;
            IsDone = item.Done;
            EditGridVisibility = true;
        }
        

        private void ExecuteDeleteTodoItemClickedCommand(TodoItem item)
        {
            CurrentTodoItem = item;
            DeleteGridVisibility = true;
        }
        private async  void ExecuteConfirmDeleteClickedCommand()
        {
            Loading = true;
            await _todoItemsService.DeleteTodoItem(CurrentTodoItem);
            TodoItemsList = await _todoItemsService.GetTodoItems();
            DeleteGridVisibility = Loading = false;
        } 

        private void ExecuteAddClickedCommand()
        {
            CurrentTodoItem = new TodoItem();
            Title = Discription = string.Empty;
            IsDone = false;
            EditGridVisibility = true;  
        }
        private async void ExecuteSaveClickedCommand()
        {
            Loading = true;
            CurrentTodoItem.Title = Title;
            CurrentTodoItem.Discription = Discription;
            CurrentTodoItem.Done = IsDone;
            CurrentTodoItem.UserId = App.UserName;
            await _todoItemsService.SaveTodoItem(CurrentTodoItem);
            TodoItemsList = await _todoItemsService.GetTodoItems();
            EditGridVisibility = Loading =  false;
        }
        private void ExecuteCancelClickedCommand()
        {
            EditGridVisibility = DeleteGridVisibility = false;
        }

        



        #endregion
    }
}