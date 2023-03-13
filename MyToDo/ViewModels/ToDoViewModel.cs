using DryIoc;
using MyToDo.Common.Models;
using MyToDo.Service;
using MyToDo.shared;
using MyToDo.shared.Dtos;
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
        private bool isDrawerOpen;
        public bool IsDrawerOpen
        { 
            get { return isDrawerOpen; } 
            set { isDrawerOpen = value;RaisePropertyChanged(); }
        }
        private ObservableCollection<ToDoDto> toDos;
        private readonly IToDoService todoService;

        public ObservableCollection<ToDoDto> ToDos
        {
			get { return toDos; }
			set { toDos = value; RaisePropertyChanged(); }
		}

        public DelegateCommand OpenDrawerCommand { get; }

        public ToDoViewModel(IToDoService todoService,IContainerProvider containerProvider)
            :base(containerProvider)
        {
            this.todoService = todoService;
            IsDrawerOpen = false;
            ToDos = new ObservableCollection<ToDoDto>();
            //CreateList();
            
            OpenDrawerCommand = new DelegateCommand(OpenDrawer);
        }

        private void OpenDrawer()
        {
            IsDrawerOpen = true;
        }

        async void GetDataAsync()
        {
            UpdateLoading(true);
            var result = await this.todoService.GetAllAsync(new shared.QueryParameters()
            {
                PageIndex = 0,
                PageSize = 100,
            });

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

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            GetDataAsync();
        }
    }
}
