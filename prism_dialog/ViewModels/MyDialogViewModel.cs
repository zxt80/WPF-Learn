using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;
using prism_dialog.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prism_dialog.ViewModels
{
    internal class MyDialogViewModel : IDialogAware
    {
        private IEventAggregator _eventAggregator;
        public string Title { get; set; }
        public string Message { get; set; }

        public string MessageText { get; set; } 

        public DelegateCommand  SaveCommand { get; set; }
        public DelegateCommand  CancelCommand { get; set; }

        public DelegateCommand SendCommand { get; set; }

        public event Action<IDialogResult> RequestClose;

        public MyDialogViewModel(IEventAggregator eventAggregator)
        {
            Title = "默认标题";
            Message = "对话框内容";
            MessageText = "消息";

            SendCommand = new DelegateCommand(Send);
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(() =>
            {
                RequestClose?.Invoke(new DialogResult(ButtonResult.No));
            });
            _eventAggregator = eventAggregator;
            
        }

        private void Send()
        {
            // 发布消息
            _eventAggregator.GetEvent<MessageEvent>().Publish(MessageText);

            
        }

        private void Save()
        {
            OnDialogClosed();
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            DialogParameters keys = new DialogParameters();
            keys.Add("Value", "Hello"); //设置对话框返回结果
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, keys));
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Title = parameters.GetValue<string>("Title");
            Message = parameters.GetValue<string>("Message");
        }
    }
}
