using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Events;
using Prism.Modularity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HQMS.Frame.Kernel.Infrastructure;
using HQMS.Frame.Kernel.Events;
using HQMS.Frame.Service.Peripherals;

namespace HQMS.Frame.Service
{
    public class ServiceLauncher
    {
        IContainerProvider containerProvider;
        IModuleManager moduleManager;
        IEventAggregator eventAggregator;

        public ServiceLauncher(IContainerProvider containerProviderArgs)
        {
            containerProvider = containerProviderArgs;
            moduleManager = containerProviderArgs.Resolve<IModuleManager>();
            eventAggregator = containerProviderArgs.Resolve<IEventAggregator>();
            moduleManager.LoadModuleCompleted += ModuleManager_LoadModuleCompleted;
        }

        private void ModuleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs args)
        {
            if (args.ModuleInfo.ModuleName== "LoginModule")
            {
                eventAggregator.GetEvent<RequestServiceEvent>().Subscribe(OnRequestService);
            }
        }

        private void OnRequestService(string requestServiceTextArgs)
        {
            JObject sevrObj  = JObject.Parse(requestServiceTextArgs);

            if (sevrObj["svc_code"].Value<string>()== "01")
            {
                JObject requestContentObj = sevrObj["serv_cont"].Value<JObject>();

                RequestAccountKind requestAccountInfo = new RequestAccountKind
                {
                    Account = requestContentObj["account"].Value<string>(),
                    Password = requestContentObj["password"].Value<string>()
                };

                IAccountAuthenticationControler accountAuthenticationControler = containerProvider.Resolve<IAccountAuthenticationControler>();
                bool ret = accountAuthenticationControler.Validate(requestAccountInfo);

                ResponseAccountKind responseAccountInfo = new ResponseAccountKind { Name = "测试" };

                ResponseServiceKind responseServiceInfo = new ResponseServiceKind
                {
                    Code = "01",
                    Name = "AccountAuthenticationService",
                    Description = "用户认证服务",
                    ResponseModuleName = "ServiceModule",
                    ReturnCode = ret == true ? "1" : "0",
                    ErrorMessage = "",
                    ServiceContent = responseAccountInfo
                };

                string responseAccountAuthenticationServiceJsonText = JsonConvert.SerializeObject(responseServiceInfo);

                eventAggregator.GetEvent<ResponseServiceEvent>().Publish(responseAccountAuthenticationServiceJsonText);
            }
        }
    }
}
