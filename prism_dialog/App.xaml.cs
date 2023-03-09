using Prism.Ioc;
using prism_dialog.ViewModels;
using prism_dialog.Views;
using System.Windows;

namespace prism_dialog
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<MyDialog, MyDialogViewModel>();
        }
    }
}
