using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace MyToDo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.ColorZone.MouseDoubleClick += ColorZone_MouseDoubleClick;
            this.ColorZone.MouseMove += ColorZone_MouseMove;

            MenuToggleButton.Click += (s, e) => {
                DemoItemsSearchBox.Focus();
            };
        }

        private void ColorZone_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void ColorZone_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            btnMax_Click(sender, e);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            if(this.WindowState==WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else 
                this.WindowState = WindowState.Maximized;
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState=WindowState.Minimized;
        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //until we had a StaysOpen flag to Drawer, this will help with scroll bars
            var dependencyObject = Mouse.Captured as DependencyObject;

            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            MenuToggleButton.IsChecked = false;
        }
    }
}
