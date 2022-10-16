using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HQMS.Frame.Kernel.Infrastructure;
using HQMS.Frame.Kernel.Events;
using HQMS.Frame.Kernel.Services;

namespace HQMS.Frame.Control.Login.Models
{
    public class ContentModel : BindableBase
    {
        IEventAggregator eventAggregator;
        IEventServiceController EventServiceController;
        bool ret;
        string requestJsonText;

        string returnMessage;
        public string ReturnMessage
        {
            get => returnMessage;
            set => returnMessage = value;
        }

        string account;
        public string Account
        {
            get => account;
            set => SetProperty(ref account, value);
        }

        string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public ContentModel(IContainerProvider containerProviderArgs)
        {
            eventAggregator = containerProviderArgs.Resolve<IEventAggregator>();
            EventServiceController = containerProviderArgs.Resolve<IEventServiceController>();
        }

        public bool IsLoginSucceed()
        {
            ret = false;

            RequestAccountKind accountInfo = new RequestAccountKind
            {
                Account = Account,
                Password = Password
            };

            requestJsonText = EventServiceController.Request(EventServicePart.AccountAuthenticationService, FrameModulePart.LoginModule, FrameModulePart.ServiceModule, accountInfo);

            eventAggregator.GetEvent<RequestServiceEvent>().Publish(requestJsonText);

            eventAggregator.GetEvent<ResponseServiceEvent>().Subscribe(OnAccountAuthenticationResponseService, ThreadOption.PublisherThread, false, x => x.Contains("AccountAuthenticationService"));

            return ret;
        }

        private void OnAccountAuthenticationResponseService(string responseServiceTextArgs)
        {
            string ret_msg = string.Empty;

            JObject respSvcObj = JObject.Parse(responseServiceTextArgs);

            if (respSvcObj["ret_code"].Value<string>() == "1")
                ret = true;
            else
                ReturnMessage = respSvcObj["ret_msg"].Value<string>();
        }

        public void NavigateToMainWindow()
        {
            ApplicationStatusKind applicationStatusInfo = new ApplicationStatusKind
            {
                ApplicationStatus = ApplicationStatusPart.LoginWindowSucceed
            };

            requestJsonText = EventServiceController.Request(EventServicePart.ApplicationStatusService, FrameModulePart.LoginModule, FrameModulePart.ServiceModule, applicationStatusInfo);

            eventAggregator.GetEvent<RequestServiceEvent>().Publish(requestJsonText);
        }
    }
}
