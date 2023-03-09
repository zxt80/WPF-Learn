using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 绑定
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new TestContext()
            {
                Name = "张三"
            };
        }
    }

    public class TestContext
    {
        public string Name { get; set; }

        public TestContext()
        {
            ShowCmd = new MyCommand(Show);
        }
        public MyCommand ShowCmd { set; get; }
        public void Show()
        {
            MessageBox.Show("点击了按钮");
            return;
        }
    }
}
