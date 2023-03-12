using MyToDo.Common.Models;
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
    class MemoViewModel : BindableBase
    {
        private bool isDrawerOpen;
        private ObservableCollection<MemoItem> memoItems;
        public DelegateCommand OpenDrawerCommand { get; }

        public ObservableCollection<MemoItem> MemoItems
		{
			get { return memoItems; }
			set { memoItems = value; }
		}
        
        public bool IsDrawerOpen 
        { 
            get { return isDrawerOpen; } 
            set { isDrawerOpen = value;RaisePropertyChanged(); } 
        }

		

        public MemoViewModel()
        {
            IsDrawerOpen = false;
            OpenDrawerCommand = new DelegateCommand(OpenDrawer);

            MemoItems = new ObservableCollection<MemoItem>();

            for(int i=0;i<10;i++)
                MemoItems.Add(new MemoItem() { Title = i.ToString(), Content = (i+i).ToString() });
        }

        private void OpenDrawer()
        {
            IsDrawerOpen = true;
        }
    }
}
