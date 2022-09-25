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
using HQMS.Frame.Kernel.Events;
using HQMS.Views;

namespace HQMS.Models
{
    public class MainWindowModel : BindableBase
    {
        IContainerProvider containerProvider;
        IEventAggregator eventAggregator;

        public MainWindowModel(IContainerProvider containerProviderArgs)
        {
            containerProvider = containerProviderArgs;
            eventAggregator = containerProviderArgs.Resolve<IEventAggregator>();

            eventAggregator.GetEvent<RequestServiceEvent>().Subscribe(OnRequestService);
        }

        private void OnRequestService(string requestServiceTextArgs)
        {
            JObject sevrObj = JObject.Parse(requestServiceTextArgs);

            if (sevrObj["svc_code"].Value<string>() == "02")
            {
                JObject requestContentObj = sevrObj["svc_cry"].Value<JObject>();
                if (requestContentObj["App_Stas"].Value<string>() == "LoginWindowReLoad")
                {
                    Window loginWindow = containerProvider.Resolve<LoginWindow>();
                    loginWindow.Show();
                }
            }
        }
    }
}
