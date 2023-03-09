using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace prism_modle.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand<string> OpenCommand { get; private set; }
        private IRegionManager _regionManager;
        public MainWindowViewModel(IRegionManager manager)
        {
            _regionManager = manager;
            OpenCommand = new DelegateCommand<string>(Open);
        }

        private void Open(string obj)
        {
            _regionManager.Regions["ContentRegion"].RequestNavigate(obj);
        }



    }
}
