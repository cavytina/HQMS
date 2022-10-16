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
    public class DataMappingViewModel : BindableBase
    {
        ISnackbarMessageQueue messageQueue;

        DataMappingModel dataMappingModel;
        public DataMappingModel DataMappingModel
        {
            get => dataMappingModel;
            set => SetProperty(ref dataMappingModel, value);
        }

        public DelegateCommand WindowLoadedCommand { get; private set; }
        public DelegateCommand CategorySelectionChangedCommand { get; private set; }

        public DelegateCommand MatchedCommand { get; private set; }
        public DelegateCommand CancelMatchedCommand { get; private set; }

        public DataMappingViewModel(IContainerProvider containerProviderArgs)
        {
            DataMappingModel = new DataMappingModel(containerProviderArgs);

            messageQueue = containerProviderArgs.Resolve<ISnackbarMessageQueue>();

            WindowLoadedCommand = new DelegateCommand(OnWindowLoaded);
            CategorySelectionChangedCommand = new DelegateCommand(OnCategorySelectionChanged);
            MatchedCommand = new DelegateCommand(OnMatched);
            CancelMatchedCommand = new DelegateCommand(OnCancelMatched);
        }

        private void OnCancelMatched()
        {
            if (DataMappingModel.CurrentMatched==null)
                messageQueue.Enqueue("请选择已匹配数据!");
            else
            {
                if (DataMappingModel.CancelMatchData())
                    messageQueue.Enqueue("取消匹配数据成功!");
                else
                    messageQueue.Enqueue("取消匹配数据失败!");
            }

            DataMappingModel.RefreshCategoryData();
        }

        private void OnMatched()
        {
            if (DataMappingModel.CurrentLocal == null)
                messageQueue.Enqueue("请选择本地数据!");
            else if (DataMappingModel.CurrentStandard == null)
                messageQueue.Enqueue("请选择标准数据!");
            else
            {
                if (DataMappingModel.MatchData())
                    messageQueue.Enqueue("匹配数据成功!");
                else
                    messageQueue.Enqueue("匹配数据失败!");
            }

            DataMappingModel.RefreshCategoryData();
        }

        private void OnWindowLoaded()
        {
            if (!DataMappingModel.LoadCategoryData())
                messageQueue.Enqueue("加载数据失败!");
        }

        private void OnCategorySelectionChanged()
        {
            if (!DataMappingModel.RefreshCategoryData())
                messageQueue.Enqueue("刷新数据失败!");
        }
    }
}
