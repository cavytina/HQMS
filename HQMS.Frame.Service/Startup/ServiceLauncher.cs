using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HQMS.Frame.Kernel.Infrastructure;
using HQMS.Frame.Kernel.Events;
using HQMS.Frame.Kernel.Services;
using HQMS.Frame.Kernel.Environment;
using HQMS.Frame.Service.Peripherals;
using HQMS.Frame.Service.Extension;
using System.Windows;

namespace HQMS.Frame.Service
{
    public class ServiceLauncher
    {
        IContainerProvider containerProvider;
        IModuleManager moduleManager;
        IRegionManager regionManager;
        IEventAggregator eventAggregator;
        IEnvironmentMonitor environmentMonitor;
        IEventServiceController eventServiceController;

        string responseJsonText;

        public ServiceLauncher(IContainerProvider containerProviderArgs)
        {
            containerProvider = containerProviderArgs;
            moduleManager = containerProviderArgs.Resolve<IModuleManager>();
            eventAggregator = containerProviderArgs.Resolve<IEventAggregator>();
            regionManager= containerProviderArgs.Resolve<IRegionManager>();

            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
            eventServiceController = containerProviderArgs.Resolve<IEventServiceController>();
            moduleManager.LoadModuleCompleted += ModuleManager_LoadModuleCompleted;
        }

        private void ModuleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs args)
        {
            if (args.ModuleInfo.ModuleName == "LoginModule")
                eventAggregator.GetEvent<RequestServiceEvent>().Subscribe(OnAccountAuthenticationRequestService, ThreadOption.PublisherThread, false, x => x.Contains("AccountAuthenticationService"));

            if (args.ModuleInfo.ModuleName == "MainLeftDrawerModule")
            {
                eventAggregator.GetEvent<RequestServiceEvent>().Subscribe(OnMenuListRequestService, ThreadOption.PublisherThread, false, x => x.Contains("MenuListService"));
                eventAggregator.GetEvent<RequestServiceEvent>().Subscribe(OnMenuItemRequestService, ThreadOption.PublisherThread, false, x => x.Contains("MenuItemService"));
            }
        }

        private void OnMenuItemRequestService(string requestServiceTextArgs)
        {
            JObject requestObj = JObject.Parse(requestServiceTextArgs);
            JObject requestContentObj = requestObj["svc_cont"].Value<JObject>();
            string requestMenuItemCodeText = requestContentObj["menu"].Value<string>();

            MenuModuleCatalogExtension menuModuleCatalogExtension = new MenuModuleCatalogExtension(containerProvider, requestMenuItemCodeText);
            menuModuleCatalogExtension.Load();
            moduleManager.Run();

            regionManager.RequestNavigate("MainContentRegion", environmentMonitor.MenuSettings[requestMenuItemCodeText].Name);
        }

        private void OnMenuListRequestService(string requestServiceTextArgs)
        {
            IMenuListController menuListController= containerProvider.Resolve<IMenuListController>();

            ResponseMenuListKind responseMenuList = new ResponseMenuListKind();
            responseMenuList.MenuList.AddRange(menuListController.Load());

            responseJsonText = eventServiceController.Response(EventServicePart.MenuListService, FrameModulePart.ServiceModule,
                     FrameModulePart.KernelModule,
                     true, "",
                    responseMenuList);

            eventAggregator.GetEvent<ResponseServiceEvent>().Publish(responseJsonText);
        }

        private void OnAccountAuthenticationRequestService(string requestServiceTextArgs)
        {
            string retMsg;
            ResponseAccountKind responseAccountInfo;
            JObject requestObj = JObject.Parse(requestServiceTextArgs);

            JObject requestContentObj = requestObj["svc_cont"].Value<JObject>();

            RequestAccountKind accountInfo = new RequestAccountKind
            {
                Account = requestContentObj["account"].Value<string>(),
                Password = requestContentObj["password"].Value<string>()
            };

            IAccountAuthenticationControler accountAuthenticationControler = containerProvider.Resolve<IAccountAuthenticationControler>();
            if (accountAuthenticationControler.Validate(accountInfo, out retMsg))
            {
                responseAccountInfo = new ResponseAccountKind { Name = retMsg };

                responseJsonText = eventServiceController.Response(EventServicePart.AccountAuthenticationService, FrameModulePart.ServiceModule,
                                     FrameModulePart.LoginModule,
                                     true, "",
                                    responseAccountInfo);
            }
            else
            {
                responseAccountInfo = new ResponseAccountKind { Name = "" };

                responseJsonText = eventServiceController.Response(EventServicePart.AccountAuthenticationService, FrameModulePart.ServiceModule,
                     FrameModulePart.LoginModule,
                     false, retMsg,
                    responseAccountInfo);
            }

            eventAggregator.GetEvent<ResponseServiceEvent>().Publish(responseJsonText);
        }
    }
}
