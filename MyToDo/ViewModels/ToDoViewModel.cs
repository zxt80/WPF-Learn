using MyToDo.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    class ToDoViewModel : BindableBase
    {
        private bool isDrawerOpen;
        public bool IsDrawerOpen
        { 
            get { return isDrawerOpen; } 
            set { isDrawerOpen = value;RaisePropertyChanged(); }
        }
        private ObservableCollection<ToDoItem> toDos;

		public ObservableCollection<ToDoItem> ToDos
        {
			get { return toDos; }
			set { toDos = value; RaisePropertyChanged(); }
		}

        public DelegateCommand OpenDrawerCommand { get; }

        public ToDoViewModel()
        {
            IsDrawerOpen = false;
            ToDos = new ObservableCollection<ToDoItem>();
            for(int i=0;i<20;i++)
            {
                ToDos.Add(new ToDoItem() { Title="123"+i,Content="456"});
            }

            OpenDrawerCommand = new DelegateCommand(OpenDrawer);
        }

        private void OpenDrawer()
        {
            IsDrawerOpen = true;
        }
    }
}
