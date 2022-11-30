using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Commands;
using MaterialDesignThemes.Wpf;
using HQMS.Control.Extension.PerformanceAssessment.Models;

namespace HQMS.Control.Extension.PerformanceAssessment.ViewModels
{
    public class DataQueryingViewModel : BindableBase
    {
        ISnackbarMessageQueue messageQueue;

        DataQueryingModel dataQueryingModel;
        public DataQueryingModel DataQueryingModel
        {
            get => dataQueryingModel;
            set => SetProperty(ref dataQueryingModel, value);
        }

        public DelegateCommand QueryCommand { get; private set; }
        public DelegateCommand ExportCommand { get; private set; }
        public DelegateCommand UpLoadCommand { get; private set; }
        public DelegateCommand NavigateFirstPageCommand { get; private set; }
        public DelegateCommand NavigateBeforePageCommand { get; private set; }
        public DelegateCommand NavigateNextPageCommand { get; private set; }
        public DelegateCommand NavigateLastPageCommand { get; private set; }
        public DelegateCommand NavigateCurrentPageCommand { get; private set; }

        public DataQueryingViewModel(IContainerProvider containerProviderArgs)
        {
            messageQueue = containerProviderArgs.Resolve<ISnackbarMessageQueue>();
            DataQueryingModel = new DataQueryingModel(containerProviderArgs);

            QueryCommand = new DelegateCommand(OnQuery);
            ExportCommand = new DelegateCommand(OnExport);
            UpLoadCommand = new DelegateCommand(OnUpLoad);

            NavigateFirstPageCommand = new DelegateCommand(OnNavigateFirstPage);
            NavigateBeforePageCommand = new DelegateCommand(OnNavigateBeforePage);
            NavigateNextPageCommand = new DelegateCommand(OnNavigateNextPage);
            NavigateLastPageCommand = new DelegateCommand(OnNavigateLastPage);
            NavigateCurrentPageCommand = new DelegateCommand(OnNavigateCurrentPage);
        }

        private void OnQuery()
        {
            DataQueryingModel.LoadData();
        }

        private void OnExport()
        {
            DataQueryingModel.ExportData();
        }

        private void OnUpLoad()
        {
            DataQueryingModel.UpLoadData();
        }

        private void OnNavigateFirstPage()
        {
            DataQueryingModel.CurrentPage = 1;
            DataQueryingModel.DisplayRecordPage();
        }

        private void OnNavigateBeforePage()
        {
            if (DataQueryingModel.CurrentPage == 1)
                messageQueue.Enqueue("当前页已是第一页!");
            else
            {
                DataQueryingModel.CurrentPage -= 1;
                DataQueryingModel.DisplayRecordPage();
            }
        }

        private void OnNavigateNextPage()
        {
            if (DataQueryingModel.CurrentPage == (int)Math.Ceiling((decimal)DataQueryingModel.TotalRecordCount / (decimal)DataQueryingModel.PageRecordCount))
                messageQueue.Enqueue("当前页已是最后页!");
            else
            {
                DataQueryingModel.CurrentPage += 1;
                DataQueryingModel.DisplayRecordPage();
            }
        }

        private void OnNavigateLastPage()
        {
            DataQueryingModel.CurrentPage = (int)Math.Ceiling((decimal)DataQueryingModel.TotalRecordCount / (decimal)DataQueryingModel.PageRecordCount);
            DataQueryingModel.DisplayRecordPage();
        }

        private void OnNavigateCurrentPage()
        {
            DataQueryingModel.DisplayRecordPage();
        }
    }
}
