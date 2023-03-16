using MaterialDesignColors;
using MyToDo.Common.Models;
using MyToDo.Service;
using MyToDo.shared.Dtos;
using MyToDo.shared.Parameters;
using Prism.Commands;
using Prism.Ioc;
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
    class MemoViewModel : NavigationViewModel
    {
        private bool isDrawerOpen;
        private ObservableCollection<MemoDto> memoItems;
        private readonly IMemoService _memoService;
        private readonly IContainerProvider containerProvider;

        private string searchText;
        private string btnText;

        public DelegateCommand OpenDrawerCommand { get; }

        public DelegateCommand<MemoDto> EditeCommand { get; }

        public DelegateCommand AddOrUpdateCommand { get; }

        public DelegateCommand AddMemoCommand { get; }

        public DelegateCommand<MemoDto> DeleteCommand { get; }

        public DelegateCommand SearchCommand { get; }

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

        public string SearchText
        {
            get { return searchText; }
            set { searchText = value; RaisePropertyChanged(); }
        }

        public string BtnText
        {
            get { return btnText; }
            set { btnText = value; RaisePropertyChanged(); }
        }

        private MemoDto currentMemo;

        public MemoDto CurrentMemo
        {
            get { return currentMemo; }
            set { currentMemo = value; RaisePropertyChanged(); }
        }




        public MemoViewModel(IMemoService memoService, IContainerProvider containerProvider)
            : base(containerProvider)
        {
            IsDrawerOpen = false;
            OpenDrawerCommand = new DelegateCommand(OpenDrawer);
            _memoService = memoService;
            MemoItems = new ObservableCollection<MemoDto>();

            EditeCommand = new DelegateCommand<MemoDto>(Edite);
            AddOrUpdateCommand = new DelegateCommand(AddOrUpdate);
            AddMemoCommand = new DelegateCommand(AddMemo);
            DeleteCommand = new DelegateCommand<MemoDto>(Delete);
            SearchCommand = new DelegateCommand(Search);

            //CreateList();
        }

        private void Search()
        {
            UpdateItems(new QueryParameters()
            {
                PageIndex = 0,
                PageSize = 50,
                Search = SearchText
            });
        }

        private async void Delete(MemoDto obj)
        {
            var response = await _memoService.DeleteAsync(obj.Id);
            if (response.Status)
            {
                MemoItems.Remove(obj);
            }
        }

        private void AddMemo()
        {
            BtnText = "新增备忘录";
            IsDrawerOpen = true;
            CurrentMemo = new MemoDto();
        }

        private async void AddOrUpdate()
        {
            if (string.IsNullOrEmpty(CurrentMemo.Title) ||
                string.IsNullOrEmpty(CurrentMemo.Content))
                return;

            UpdateLoading(true);
            try
            {
                if (CurrentMemo.Id > 0)
                {
                    var memo = MemoItems.FirstOrDefault(x => x.Id == CurrentMemo.Id);
                    if (memo != null)
                    {
                        var response = await _memoService.UpdateAsync(CurrentMemo);
                        if (response != null && response.Status)
                        {
                            memo.Title = CurrentMemo.Title;
                            memo.Content = CurrentMemo.Content;
                        }
                    }
                }
                else
                {
                    var response = await _memoService.AddAsync(CurrentMemo);
                    if (response.Status)
                    {
                        MemoItems.Add(response.Result);
                    }

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsDrawerOpen = false;
                UpdateLoading(false);
            }
        }

        private void Edite(MemoDto obj)
        {
            BtnText = "更新备忘录";
            IsDrawerOpen = true;
            CurrentMemo = obj.Clone() as MemoDto;
        }

        private void OpenDrawer()
        {
            IsDrawerOpen = true;
        }

        async void GetDataAsync()
        {
            UpdateItems(new QueryParameters()
            {
                PageIndex = 0,
                PageSize = 100,
            });
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            GetDataAsync();
        }

        async void UpdateItems(QueryParameters param)
        {
            UpdateLoading(true);
            var result = await _memoService.GetAllAsync(param);

            if (result.Status)
            {
                MemoItems.Clear();

                foreach (var item in result.Result.Items)
                {
                    MemoItems.Add(item);
                }
            }
            UpdateLoading(false);
        }
    }
}
