using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Controls;

namespace MyToDo.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public string SearchKeyword { get; set; }

        public int SelectedIndex { get; set; }

        public object SelectedItem { get; set; }

        public ItemCollection DemoItems{get;set;}

        public DelegateCommand MoveNextCommand { get; }
        public DelegateCommand MovePrevCommand { get; }
        public MainWindowViewModel()
        {
            MoveNextCommand = new DelegateCommand(MoveNext);
            MovePrevCommand = new DelegateCommand(MovePrev);
        }

        private void MovePrev()
        {
            
        }

        private void MoveNext()
        {
            
        }
    }
}
