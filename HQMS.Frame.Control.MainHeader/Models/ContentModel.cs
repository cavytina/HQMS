using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Events;
using HQMS.Frame.Kernel.Infrastructure;
using HQMS.Frame.Kernel.Events;
using HQMS.Frame.Kernel.Services;

namespace HQMS.Frame.Control.MainHeader.Models
{
    public class ContentModel : BindableBase
    {
        IEventAggregator eventAggregator;
        IJsonTextController jsonTextController;
        string requestServiceEventJsonText;

        public ContentModel(IContainerProvider containerProviderArgs)
        {
            eventAggregator = containerProviderArgs.Resolve<IEventAggregator>();
            jsonTextController = containerProviderArgs.Resolve<IJsonTextController>();
        }

        public void NavigateToLoginWindow()
        {
            ApplicationStatusKind applicationStatus = new ApplicationStatusKind
            {
                ApplicationStatus = ApplicationStatusPart.LoginWindowReLoad
            };

            requestServiceEventJsonText = jsonTextController.ConvertToText(ServiceEventPart.ApplicationStatusService, applicationStatus);

            eventAggregator.GetEvent<RequestServiceEvent>().Publish(requestServiceEventJsonText);
        }
    }
}
