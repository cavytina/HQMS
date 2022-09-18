﻿using System;
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

namespace HQMS.Frame.Control.Login.Models
{
    public class ContentModel : BindableBase
    {
        IEventAggregator eventAggregator;
        bool ret;

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

            eventAggregator.GetEvent<ResponseServiceEvent>().Subscribe(OnResponseService);
        }

        private void OnResponseService(string responseServiceTextArgs)
        {
            JObject respSvcObj = JObject.Parse(responseServiceTextArgs);

            if (respSvcObj["svc_code"].Value<string>() == "01" && respSvcObj["ret_code"].Value<string>() == "1")
            {
                ret = true;

                JObject responseContentObj = respSvcObj["serv_cont"].Value<JObject>();
                ResponseAccountKind responseAccountInfo = new ResponseAccountKind { Name = responseContentObj["name"].Value<string>() };
            }
        }

        public bool IsLoginSucceed()
        {
            ret = false;

            RequestAccountKind accountInfo = new RequestAccountKind
            {
                Account = Account,
                Password = Password
            };

            RequestServiceKind requestAccountAuthenticationService = new RequestServiceKind
            {
                Code = "01",
                Name = "AccountAuthenticationService",
                Description = "用户认证服务",
                RequestModuleName = "LoginModule",
                ServiceContent = accountInfo
            };

            string requestAccountAuthenticationServiceJsonText = JsonConvert.SerializeObject(requestAccountAuthenticationService);

            eventAggregator.GetEvent<RequestServiceEvent>().Publish(requestAccountAuthenticationServiceJsonText);

            return ret;
        }
    }
}
