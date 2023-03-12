using MyToDo.Common.Models;
using MyToDo.Extentions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    class SettingViewModel : BindableBase
    {
        private IRegionManager _regionManager;

        private ObservableCollection<MenuBar> settingMenus;

		public ObservableCollection<MenuBar> SettingMenus
        {
			get { return settingMenus; }
			set { settingMenus = value; RaisePropertyChanged(); }
		}

        public DelegateCommand<MenuBar> NavigateCommand { get; }

        public SettingViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            settingMenus = new ObservableCollection<MenuBar>();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);

            settingMenus.Add(new MenuBar() { Icon = "Palette", Title = "个性化", NameSpace = "PersonalizeView" });
            settingMenus.Add(new MenuBar() { Icon = "Cog", Title = "系统设置", NameSpace = "SystemView" });
            settingMenus.Add(new MenuBar() { Icon = "Information", Title = "关于更多", NameSpace = "MoreView" });
        }

        private void Navigate(MenuBar obj)
        {
            if(obj!=null && !string.IsNullOrEmpty(obj.NameSpace))
                _regionManager.Regions[PrismManager.SettingViewRegionName].RequestNavigate(obj.NameSpace);
        }
    }
}
