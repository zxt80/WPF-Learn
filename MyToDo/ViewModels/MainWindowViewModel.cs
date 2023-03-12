using MyToDo.Common.Models;
using MyToDo.Extentions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace MyToDo.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public string SearchKeyword { get; set; }

        public int SelectedIndex { get; set; }

        public object SelectedItem { get; set; }

        private ObservableCollection<MenuBar> menuBars;

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }

        private IRegionManager _regionManager;
        private IRegionNavigationJournal _navigationJournal;

        public DelegateCommand GoBackCommand { get; }
        public DelegateCommand GoForwardCommand { get; }

        public DelegateCommand<MenuBar> NavigateCommand { get;  }
        public MainWindowViewModel( IRegionManager regionManager)
        {
            _regionManager = regionManager;
            GoBackCommand = new DelegateCommand(GoBack);
            GoForwardCommand = new DelegateCommand(GoFoward);
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);

            MenuBars = new ObservableCollection<MenuBar>();

            MenuBars.Add(new MenuBar() { Icon = "Home", Title = "首页", NameSpace = "IndexView" });
            MenuBars.Add(new MenuBar() { Icon = "Notebook", Title = "待办事项", NameSpace = "ToDoView" });
            MenuBars.Add(new MenuBar() { Icon = "LeadPencil", Title = "备忘录", NameSpace = "MemoView" });
            MenuBars.Add(new MenuBar() { Icon = "Cog", Title = "设置", NameSpace = "SettingView" });
        }

        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj.NameSpace))
                return;

            _regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.NameSpace, back =>
            {
                _navigationJournal = back.Context.NavigationService.Journal;
            });
        }

        private void GoBack()
        {
            if(_navigationJournal!=null && _navigationJournal.CanGoBack)
            {
                _navigationJournal.GoBack();
            }
        }

        private void GoFoward()
        {
            if(_navigationJournal != null && _navigationJournal.CanGoForward)
            {
                _navigationJournal.GoForward();
            }
        }
    }
}
