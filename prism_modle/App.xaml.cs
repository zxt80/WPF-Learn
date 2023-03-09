using FuncA_Module;
using Prism.Ioc;
using Prism.Modularity;
using prism_modle.Views;
using System.Windows;

namespace prism_modle
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

        }

        //protected override IModuleCatalog CreateModuleCatalog()
        //{
        //    // 加载指定路径下的全部模块
        //    return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        //}

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            // 加载指定模块， 需要配置引用关系
            moduleCatalog.AddModule<FuncA_ModuleModule>();
        }
    }
}
