using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace 绑定2
{
    class MainWindowViewModle:INotifyPropertyChanged
    {
        private string name;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { 
            get {
                return name;
            } 
            set {
                name = value;
                // 通知更新
                this.PropertyChanged?.Invoke(this,new PropertyChangedEventArgs("Name"));
            } }

        public MainWindowViewModle()
        {
            Cmd = new MyCommand(Show);
            Name = "张三";
        }

        // 命令
        public MyCommand Cmd { set; get; }

        private void Show()
        {
            Name = "李四";
            MessageBox.Show($"Hello, {Name}");
        }
    }
}
