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
    public class DataExportingViewModel : BindableBase
    {
        ISnackbarMessageQueue messageQueue;

        DataExportingModel dataExportingModel;
        public DataExportingModel DataExportingModel
        {
            get => dataExportingModel;
            set => SetProperty(ref dataExportingModel, value);
        }

        public DelegateCommand MasterCommand { get; private set; }
        public DelegateCommand DetailCommand { get; private set; }
        public DelegateCommand MasterExportCommand { get; private set; }
        public DelegateCommand DetailExportCommand { get; private set; }

        public DataExportingViewModel(IContainerProvider containerProviderArgs)
        {
            messageQueue = containerProviderArgs.Resolve<ISnackbarMessageQueue>();
            DataExportingModel = new DataExportingModel(containerProviderArgs);

            MasterCommand = new DelegateCommand(OnMaster);
            DetailCommand = new DelegateCommand(OnDetail);
            MasterExportCommand = new DelegateCommand(OnMasterExport);
            DetailExportCommand = new DelegateCommand(OnDetailExport);
        }

        private void OnMasterExport()
        {
            DataExportingModel.ExprotMasterData();
        }

        private void OnDetailExport()
        {
            DataExportingModel.ExprotDetailData();
        }

        private void OnMaster()
        {
            DataExportingModel.LoadMasterData();
        }

        private void OnDetail()
        {
            DataExportingModel.LoadDetailData();
        }
    }
}
