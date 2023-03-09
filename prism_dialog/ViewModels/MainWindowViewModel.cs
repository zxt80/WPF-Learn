using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace prism_dialog.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private IDialogService _dialogService;
        public DelegateCommand<string> OpenCommand { get; set; }
        public MainWindowViewModel(IDialogService dialogService)
        {
            OpenCommand = new DelegateCommand<string>(Open);
            this._dialogService = dialogService;
        }

        private void Open(string obj)
        {
            DialogParameters keys = new DialogParameters();
            keys.Add("Title", "测试弹窗");
            keys.Add("Message", "这是测试弹窗的内容");
            _dialogService.ShowDialog(obj, keys, callback => 
            { 
                if(callback.Result==ButtonResult.OK)
                {
                    var res = callback.Parameters.GetValue<string>("Value");
                }
            });
        }
    }
}
