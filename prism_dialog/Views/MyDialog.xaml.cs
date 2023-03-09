using Prism.Events;
using prism_dialog.Event;
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

namespace prism_dialog.Views
{
    /// <summary>
    /// MyDialog.xaml 的交互逻辑
    /// </summary>
    public partial class MyDialog : UserControl
    {
        public MyDialog(IEventAggregator eventAggregator)
        {
            InitializeComponent();

            // 订阅消息
            eventAggregator.GetEvent<MessageEvent>().Subscribe(SubMessage);

            // 取消订阅
            // eventAggregator.GetEvent<MessageEvent>().Unsubscribe(SubMessage);
        }

        private void SubMessage(string obj)
        {
            MessageBox.Show($"接收到消息：{obj}");
        }
    }
}
