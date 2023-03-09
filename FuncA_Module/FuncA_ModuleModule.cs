using FuncA_Module.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace FuncA_Module
{
    public class FuncA_ModuleModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA>();
            containerRegistry.RegisterForNavigation<ViewB>();
        }
    }
}