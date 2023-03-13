using MyToDo.Common.Models;
using MyToDo.shared.Dtos;
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
    class IndexViewModel:BindableBase
    {
        private ObservableCollection<MemoDto> memoItems;

        public ObservableCollection<MemoDto> MemoItems
        {
            get { return memoItems; }
            set { memoItems = value; }
        }

        private ObservableCollection<ToDoDto> toDos;
        public ObservableCollection<ToDoDto> ToDos 
        { 
            get { return toDos; } 
            set { toDos = value; RaisePropertyChanged(); } 
        }
        public ObservableCollection<TaskBar> TaskBars { set; get; }

        public IndexViewModel()
        {
            TaskBars = new ObservableCollection<TaskBar>();
            ToDos = new ObservableCollection<ToDoDto>();
            MemoItems = new ObservableCollection<MemoDto>();
            CreateTaskBars();
        }

        void CreateTaskBars()
        {
            TaskBars.Add(new TaskBar() { Icon = "ClockFast", Title = "汇总", Contant = "5", Color = "#FF0CA0FF",Target="" });
            TaskBars.Add(new TaskBar() { Icon = "ClockCheck", Title = "已完成", Contant = "5", Color = "#FF1ECA3A", Target = "" });
            TaskBars.Add(new TaskBar() { Icon = "InformationVariantBox", Title = "完成百分比", Contant = "100%", Color = "#FF02C6DC", Target = "" });
            TaskBars.Add(new TaskBar() { Icon = "Memory", Title = "备忘录", Contant = "14", Color = "#FFFFA000", Target = "" });

            
            ToDos.Add(new ToDoDto() { Title = "买菜", Content = "去超市买菜", Status = 0});
            MemoItems.Add(new MemoDto() { Title = "上班", Content = "去公司"});
           
        }

    }
}
