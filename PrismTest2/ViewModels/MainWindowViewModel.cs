using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace PrismTest2.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        public DelegateCommand<string> OpenCommand { get; private set; }
        public MainWindowViewModel(IRegionManager regionManager)
        {
            OpenCommand = new DelegateCommand<string>(Open);
            this._regionManager = regionManager;
        }


        private void Open(string obj)
        {
            // 首先通过IRegionManager接口获取可用的区域
            // 往这个区域动态的设置内容
            // 设置内容的方式是通过依赖注入的方式
            _regionManager.Regions["ContentRegion"].RequestNavigate(obj);
        }
    }
}
