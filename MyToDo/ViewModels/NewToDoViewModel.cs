﻿using MyToDo.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    class NewToDoViewModel:BindableBase
    {
        

        public DelegateCommand Add { get; }

        public NewToDoViewModel()
        {
            
        }

        private void AddToDo()
        {
            
        }
    }
}
