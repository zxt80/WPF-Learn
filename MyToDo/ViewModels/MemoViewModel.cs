using MyToDo.Common.Models;
using MyToDo.Service;
using MyToDo.shared.Dtos;
using MyToDo.shared.Parameters;
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
        private ObservableCollection<MemoDto> memoItems;
        private readonly IMemoService _memoService;

        public DelegateCommand OpenDrawerCommand { get; }

        public ObservableCollection<MemoDto> MemoItems
		{
			get { return memoItems; }
			set { memoItems = value; }
		}
        
        public bool IsDrawerOpen 
        { 
            get { return isDrawerOpen; } 
            set { isDrawerOpen = value;RaisePropertyChanged(); } 
        }

		

        public MemoViewModel(IMemoService memoService)
        {
            IsDrawerOpen = false;
            OpenDrawerCommand = new DelegateCommand(OpenDrawer);
            _memoService = memoService;

            MemoItems = new ObservableCollection<MemoDto>();
            CreateList();
        }

        private void OpenDrawer()
        {
            IsDrawerOpen = true;
        }

        async void CreateList()
        {
            var result = await this._memoService.GetAllAsync(new QueryParameters()
            {
                PageIndex = 0,
                PageSize = 100,
            });

            if (result.Status)
            {
                MemoItems.Clear();

                foreach (var item in result.Result.Items)
                {
                    MemoItems.Add(item);
                }
            }
        }
    }
}
