using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mvvm微软框架
{
    class MainWindowViewModle: ObservableObject
    {
        private string name;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public RelayCommand Cmd { get; }

        public MainWindowViewModle()
        {
            Name = "张三";
            Cmd = new RelayCommand(Show);
        }

        private void Show()
        {
            Name = "李四";
            MessageBox.Show($"Hello，{Name}");
        }


    }
}
