using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Commands;
using Prism.Regions;
using HQMS.Control.Extension.PerformanceAssessment.Models;

namespace HQMS.Control.Extension.PerformanceAssessment.ViewModels
{
    public class PerformanceAssessmentViewModel : BindableBase
    {
        string menuItemName = "绩效考核";
        public string MenuItemName
        {
            get => menuItemName;
            set => SetProperty(ref menuItemName, value);
        }

        PerformanceAssessmentModel performanceAssessmentModel;
        public PerformanceAssessmentModel PerformanceAssessmentModel
        {
            get => performanceAssessmentModel;
            set => SetProperty(ref performanceAssessmentModel, value);
        }

        IRegionManager regionManager;

        public DelegateCommand MenuItemSelectedChangedCommand { get; private set; }

        public PerformanceAssessmentViewModel (IContainerProvider containerProviderArgs)
        {
            regionManager = containerProviderArgs.Resolve<IRegionManager>();
            PerformanceAssessmentModel = new PerformanceAssessmentModel(containerProviderArgs);

            MenuItemSelectedChangedCommand = new DelegateCommand(OnMenuItemSelectedChanged);
        }

        private void OnMenuItemSelectedChanged()
        {
            switch (PerformanceAssessmentModel.CurrentMenuItemName)
            {
                case "DataMapping": regionManager.RequestNavigate("PerformanceAssessmentContentRegion", "DataMappingView"); break;
                case "DataQuerying": regionManager.RequestNavigate("PerformanceAssessmentContentRegion", "DataQueryingView"); break;
                case "DataExporting": regionManager.RequestNavigate("PerformanceAssessmentContentRegion", "DataExportingView"); break;
            }
        }
    }
}
