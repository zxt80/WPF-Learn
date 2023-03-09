using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace 绑定
{
    class MainWindowViewModle
    {
        public MainWindowViewModle()
        {
            ShowCmd = new MyCommand(Show) ;
        }
        public MyCommand ShowCmd { set; get; }
        public void Show()
        {
            MessageBox.Show("点击了按钮");
            return;
        }
    }

    
}
