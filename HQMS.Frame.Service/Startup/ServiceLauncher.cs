using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Events;
using Prism.Modularity;
using Newtonsoft.Json;
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

        private void OnRequestService(string requestServiceText)
        {
            ServiceKind requestService = JsonConvert.DeserializeObject<ServiceKind>(requestServiceText);

            if (requestService.name== "AccountAuthenticationService")
            {
                AccountInfoKind accountInfo= JsonConvert.DeserializeObject<AccountInfoKind>(requestService.content);

                IAccountAuthenticationControler accountAuthenticationControler = containerProvider.Resolve<IAccountAuthenticationControler>();
                bool ret = accountAuthenticationControler.Validate(accountInfo);

                ResultKind resultKind = new ResultKind { result = ret.ToString() };
                ServiceKind responseService = new ServiceKind
                {
                    code = "01",
                    name = "AccountAuthenticationService",
                    content = JsonConvert.SerializeObject(resultKind)
                };

                string responseAccountAuthenticationServiceJsonText = JsonConvert.SerializeObject(responseService);

                eventAggregator.GetEvent<ResponseServiceEvent>().Publish(responseAccountAuthenticationServiceJsonText);
            }
        }
    }
}
