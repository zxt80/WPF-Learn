using Prism.Commands;
using Prism.Mvvm;
using PrismTest.Views;
using System;
using System.Data.Common;

namespace PrismTest.ViewModels
{
    // ctrl+K+D 格式代码
    public class MainWindowViewModel : BindableBase
    {
        public DelegateCommand<string> OpenCmd { get; private set; }
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private object body;
        public Object Body { get => body; private set { body = value; RaisePropertyChanged(); } }

        public MainWindowViewModel()
        {
            OpenCmd = new DelegateCommand<string>(Open);
            Body = new FuncA();
        }

        private void Open(string obj)
        {
            switch (obj)
            {
                case "FuncA": Body = new FuncA(); break;
                case "FuncB": Body = new FuncB(); break;
                case "FuncC": Body = new FuncC(); break;
            }
        }
    }
}
