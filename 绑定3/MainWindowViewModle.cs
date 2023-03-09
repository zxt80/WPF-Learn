using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace 绑定3
{
    class MainWindowViewModle:BaseViewModle
    {
        private string name;

        public string Name
        {
            get { return name; }
            set {
                name = value;
                OnPropertyChanged();    // 通知属性更改
            }
        }

        public MyCommand Cmd { get; set; }

        public MainWindowViewModle()
        {
            Name = "张三";
            Cmd = new MyCommand(Show);
        }

        private void Show()
        {
            Name = "李四";
            MessageBox.Show($"Hello,{Name}");
        }
    }
}
