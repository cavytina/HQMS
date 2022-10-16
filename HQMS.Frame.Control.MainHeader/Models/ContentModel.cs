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
        IEventServiceController eventServiceController;
        string requestServiceEventJsonText;

        bool isLeftDrawerOpen;
        public bool IsLeftDrawerOpen
        {
            get => isLeftDrawerOpen;
            set
            {
                SetProperty(ref isLeftDrawerOpen, value);
                OnLeftDrawerChanged(isLeftDrawerOpen);
            }
        }

        public ContentModel(IContainerProvider containerProviderArgs)
        {
            eventAggregator = containerProviderArgs.Resolve<IEventAggregator>();
            eventServiceController = containerProviderArgs.Resolve<IEventServiceController>();
        }

        public void NavigateToLoginWindow()
        {
            ApplicationStatusKind applicationStatus = new ApplicationStatusKind
            {
                ApplicationStatus = ApplicationStatusPart.LoginWindowReLoad
            };

            requestServiceEventJsonText = eventServiceController.Request(EventServicePart.ApplicationStatusService, FrameModulePart.MainHeaderModule,
                                            FrameModulePart.ApplictionModule, applicationStatus);

            eventAggregator.GetEvent<RequestServiceEvent>().Publish(requestServiceEventJsonText);
        }

        private void OnLeftDrawerChanged(bool isCheckArgs)
        {
            ApplicationStatusKind applicationStatus = new ApplicationStatusKind
            {
                ApplicationStatus = isCheckArgs == false ? ApplicationStatusPart.LeftDrawerClose : ApplicationStatusPart.LeftDrawerOpen
            };

            requestServiceEventJsonText = eventServiceController.Request(EventServicePart.ApplicationStatusService, FrameModulePart.MainHeaderModule,
                                            FrameModulePart.ApplictionModule, applicationStatus);

            eventAggregator.GetEvent<RequestServiceEvent>().Publish(requestServiceEventJsonText);
        }
    }
}
