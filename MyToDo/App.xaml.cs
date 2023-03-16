using DryIoc;
using MyToDo.Service;
using MyToDo.ViewModels;
using MyToDo.Views;
using Prism.DryIoc;
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
            containerRegistry.GetContainer().
                Register<HttpRestClient>(made:Parameters.Of.Type<string>(serviceKey:"webUrl"));
            //containerRegistry.GetContainer().RegisterInstance(@"http://localhost:3389",serviceKey:"webUrl");
            containerRegistry.GetContainer().RegisterInstance(@"http://localhost:5014", serviceKey:"webUrl");

            containerRegistry.Register<IToDoService,ToDoService>();
            containerRegistry.Register<IMemoService,MemoService>();

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
