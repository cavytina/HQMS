using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HQMS.Frame.Kernel.Infrastructure;
using HQMS.Frame.Kernel.Events;
using HQMS.Frame.Kernel.Services;
using HQMS.Views;

namespace HQMS.Models
{
    public class MainWindowModel : BindableBase
    {
        IContainerProvider containerProvider;
        IEventAggregator eventAggregator;
        IEventServiceController eventServiceController;

        string requestServiceEventJsonText;

        bool isLeftDrawerOpen;
        public bool IsLeftDrawerOpen
        {
            get => isLeftDrawerOpen;
            set => SetProperty(ref isLeftDrawerOpen, value);
        }

        public MainWindowModel(IContainerProvider containerProviderArgs)
        {
            containerProvider = containerProviderArgs;
            eventAggregator = containerProviderArgs.Resolve<IEventAggregator>();
            eventServiceController = containerProviderArgs.Resolve<IEventServiceController>();

            eventAggregator.GetEvent<RequestServiceEvent>().Subscribe(OnRequestApplicationStatusService, ThreadOption.PublisherThread, false, x => x.Contains("ApplicationStatusService"));
        }

        private void OnRequestApplicationStatusService(string requestServiceTextArgs)
        {
            JObject requestObj = JObject.Parse(requestServiceTextArgs);

            JObject requestContentObj = requestObj["svc_cont"].Value<JObject>();

            if (requestContentObj["App_Stas"].Value<string>() == "LoginWindowReLoad")
            {
                Window loginWindow = containerProvider.Resolve<LoginWindow>();
                loginWindow.Show();
            }
            else if (requestContentObj["App_Stas"].Value<string>() == "LeftDrawerOpen")
                IsLeftDrawerOpen = true;
            else if (requestContentObj["App_Stas"].Value<string>() == "LeftDrawerClose")
                IsLeftDrawerOpen = false;
        }
    }
}
