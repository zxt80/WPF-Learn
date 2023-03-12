using MyToDo.ViewModels;
using MyToDo.Views;
using Prism.Ioc;
using System.Windows;

namespace MyToDo
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
            containerRegistry.RegisterForNavigation<IndexView,IndexViewModel>();
            containerRegistry.RegisterForNavigation<ToDoView,ToDoViewModel>();
            containerRegistry.RegisterForNavigation<MemoView,MemoViewModel>();
            containerRegistry.RegisterForNavigation<SettingView,SettingViewModel>();

            containerRegistry.RegisterForNavigation<PersonalizeView,PersonalizeViewModel>();
            containerRegistry.RegisterForNavigation<SystemView,SystemViewModel>();
            containerRegistry.RegisterForNavigation<MoreView,MoreViewModel>();
        }
    }
}
