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

namespace 数据模板
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<ColorItem> test = new List<ColorItem>();
            test.Add(new ColorItem() { Code= new SolidColorBrush(Colors.Red), Name= "Red" });
            test.Add(new ColorItem() { Code= new SolidColorBrush(Colors.Green), Name = "Green" });
            test.Add(new ColorItem() { Code= new SolidColorBrush(Colors.Blue), Name = "Blue" });
            test.Add(new ColorItem() { Code= new SolidColorBrush(Colors.Yellow), Name = "Yellow" });
            test.Add(new ColorItem() { Code= new SolidColorBrush(Colors.Peru), Name = "Peru" });
            test.Add(new ColorItem() { Code = new SolidColorBrush(Colors.Aquamarine), Name = "Aquamarine" });

            //list.ItemsSource = test;
            grid.ItemsSource = test;
        }
    }

    public class ColorItem
    {
        public Brush Code { get; set; }

        public string Name  { get; set; }
    }
}
