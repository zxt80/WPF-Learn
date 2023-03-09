﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace 绑定2
{
    class MyCommand : ICommand
    {
        Action excludeAction;
        public event EventHandler CanExecuteChanged;
        public MyCommand(Action ac)
        {
            excludeAction = ac;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            excludeAction();
        }
    }
}
