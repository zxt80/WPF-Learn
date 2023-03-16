using DryIoc;
using MyToDo.Common.Models;
using MyToDo.Service;
using MyToDo.shared.Dtos;
using MyToDo.shared.Parameters;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    class ToDoViewModel : NavigationViewModel
    {
        private string searchText;
        private string btnText;
        private bool isDrawerOpen;
        private ToDoDto currentToDo;
        private ObservableCollection<ToDoDto> toDos;
        private readonly IToDoService todoService;

        private int filterIndex;

        public int FilterIndex
        {
            get { return filterIndex; }
            set { filterIndex = value; RaisePropertyChanged(); }
        }


        public string SearchText
        {
            get { return searchText; }
            set { searchText = value; RaisePropertyChanged(); }
        }



        public DelegateCommand<ToDoDto> EditeCommand { get; }

        public DelegateCommand AddOrUpdateCommand { get; }

        public DelegateCommand AddToDoCommand { get; }

        public DelegateCommand<ToDoDto> DeleteCommand { get; }

        public DelegateCommand SearchCommand { get; }


        public bool IsDrawerOpen
        { 
            get { return isDrawerOpen; } 
            set { isDrawerOpen = value;RaisePropertyChanged(); }
        }        

        public string BtnText
        {
            get { return btnText; }
            set { btnText = value; RaisePropertyChanged(); }
        }


        public ObservableCollection<ToDoDto> ToDos
        {
			get { return toDos; }
			set { toDos = value; RaisePropertyChanged(); }
		}        

        public ToDoDto CurrentTodo
        {
            get { return currentToDo; }
            set { currentToDo = value; RaisePropertyChanged(); }
        }



        public ToDoViewModel(IToDoService todoService,IContainerProvider containerProvider)
            :base(containerProvider)
        {
            this.todoService = todoService;
            IsDrawerOpen = false;
            ToDos = new ObservableCollection<ToDoDto>();
            FilterIndex = 2;

            EditeCommand = new DelegateCommand<ToDoDto>(Edite);
            AddOrUpdateCommand = new DelegateCommand(AddOrUpdate);
            AddToDoCommand = new DelegateCommand(AddToDo);
            DeleteCommand = new DelegateCommand<ToDoDto>(Delete);
            SearchCommand = new DelegateCommand(Search);
        }

        private void Search()
        {
            UpdateItems(new ToDoParameter()
            {
                PageIndex = 0,
                PageSize = 50,
                Search = SearchText,
                Status = FilterIndex == 2 ? null : FilterIndex
            }); 
        }

        private async void Delete(ToDoDto obj)
        {
            var response = await todoService.DeleteAsync(obj.Id);
            if (response.Status)
            {
                ToDos.Remove(obj);
            }
        }

        private void AddToDo()
        {
            BtnText = "新增待办";
            IsDrawerOpen = true;
            CurrentTodo = new ToDoDto();
        }

        private async void AddOrUpdate()
        {
            if (string.IsNullOrEmpty(currentToDo.Title) ||
                string.IsNullOrEmpty(currentToDo.Content))
                return;

            UpdateLoading(true);
            try
            {
                if (CurrentTodo.Id > 0)
                {
                    var todo = ToDos.FirstOrDefault(x => x.Id == CurrentTodo.Id);
                    if (todo != null)
                    {                       
                        var response = await todoService.UpdateAsync(CurrentTodo);
                        if(response!=null && response.Status)
                        {
                            todo.Status = CurrentTodo.Status;
                            todo.Title = CurrentTodo.Title;
                            todo.Content = CurrentTodo.Content;
                        }
                    }
                }
                else
                {
                    var response = await todoService.AddAsync(CurrentTodo);
                    if (response.Status)
                    {
                        ToDos.Add(response.Result);
                    }

                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                IsDrawerOpen = false;
                UpdateLoading(false);
            }
            
        }

        private void Edite(ToDoDto todoDto)
        {
            BtnText = "更新待办";
            IsDrawerOpen = true;
            CurrentTodo = todoDto.Clone() as ToDoDto;
        }


        void GetDataAsync()
        {
            UpdateItems(new ToDoParameter()
            {
                PageIndex = 0,
                PageSize = 100,
            });
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            GetDataAsync();
        }

        async void UpdateItems(ToDoParameter param)
        {
            UpdateLoading(true);
            var result = await todoService.GetAllFilterAsync(param);

            if (result.Status)
            {
                ToDos.Clear();

                foreach (var item in result.Result.Items)
                {
                    ToDos.Add(item);
                }
            }
            UpdateLoading(false);
        }
    }
}
