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

        public DataExportingViewModel(IContainerProvider containerProviderArgs)
        {
            messageQueue = containerProviderArgs.Resolve<ISnackbarMessageQueue>();
            DataExportingModel = new DataExportingModel(containerProviderArgs);

            MasterCommand = new DelegateCommand(OnMaster);
            DetailCommand = new DelegateCommand(OnDetail);
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
