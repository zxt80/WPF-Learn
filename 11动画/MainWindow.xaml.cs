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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _11动画
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            //创建双精度的动画
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = btn.Width;                     // 初始位置

            animation.To = btn.Width - 30;

            
            //animation.By = -30; //等同于From to的用法

            animation.AutoReverse = true; //动画自动还原
            //animation.RepeatBehavior = RepeatBehavior.Forever; //动画永远持续执行
            animation.RepeatBehavior = new RepeatBehavior(3); //动画执行3次

            animation.Duration = TimeSpan.FromSeconds(1);   // 持续时间

            animation.Completed += Animation_Completed;

            // 启动动画
            this.btn.BeginAnimation(Button.WidthProperty, animation);
        }

        private void Animation_Completed(object sender, EventArgs e)
        {
            btn.Content = "动画已经完成";
        }
    }
}
