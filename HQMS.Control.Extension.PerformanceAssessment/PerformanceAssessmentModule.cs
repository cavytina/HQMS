using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Modularity;
using HQMS.Control.Extension.PerformanceAssessment.Views;

namespace HQMS.Control.Extension.PerformanceAssessment
{
    public class PerformanceAssessmentModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PerformanceAssessmentView>("PerformanceAssessmentModule");

            containerRegistry.RegisterForNavigation<DataMappingView>();
            containerRegistry.RegisterForNavigation<DataQueryingView>();
            containerRegistry.RegisterForNavigation<DataExportingView>();
        }
    }
}
