using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using HQMS.Frame.Kernel.Environment;
using HQMS.Frame.Kernel.Services;
using HQMS.Frame.Kernel.Extension;
using HQMS.Frame.Kernel.Infrastructure;
using HQMS.Frame.Kernel.Events;

namespace HQMS.Frame.Kernel
{
    public class KernelLauncher
    {
        IContainerProvider containerProvider;
        IModuleManager moduleManager;
        IEventAggregator eventAggregator;

        IEnvironmentMonitor environmentMonitor;
        IPathManager pathManager;
        ILogManager logManager;
        IDataBaseManager dataBaseManager;

        string relayResponseJsonText;

        public KernelLauncher(IContainerProvider containerProviderArgs)
        {
            containerProvider = containerProviderArgs;
            moduleManager = containerProviderArgs.Resolve<IModuleManager>();
            eventAggregator = containerProviderArgs.Resolve<IEventAggregator>();
            moduleManager.LoadModuleCompleted += ModuleManager_LoadModuleCompleted;

            environmentMonitor = containerProvider.Resolve<IEnvironmentMonitor>();
        }

        private void ModuleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs args)
        {
            if (args.ModuleInfo.ModuleName == "ServiceModule")
                eventAggregator.GetEvent<ResponseServiceEvent>().Subscribe(OnMenuListResponseService, ThreadOption.PublisherThread, false, x => x.Contains("MenuListService"));
        }

        private void OnMenuListResponseService(string responseServiceTextArgs)
        {
            JObject responseObj = JObject.Parse(responseServiceTextArgs);

            JObject responseContentObj = responseObj["svc_cont"].Value<JObject>();

            JArray responseContentText = responseContentObj["menus"].Value<JArray>();

            foreach (JObject menuItemObj in responseContentText)
            {
                environmentMonitor.MenuSettings.Add(new MenuKind
                {
                    Code = menuItemObj["svc_code"].Value<string>(),
                    Name = menuItemObj["svc_name"].Value<string>(),
                    Content = menuItemObj["svc_exp"].Value<string>(),
                    Description = menuItemObj["svc_desc"].Value<string>(),
                    ReferName = menuItemObj["refer_name"].Value<string>(),
                    SuperCode = menuItemObj["super_code"].Value<string>(),
                    SuperName = menuItemObj["super_name"].Value<string>()
                });
            }
        }

        public void Initialize()
        {
            InitializeWithValidateServices();
            LoadFrameModuleCatalog();
        }

        private void InitializeWithValidateServices()
        {
            pathManager = containerProvider.Resolve<IPathManager>();
            pathManager.Initialize();

            Validator<PathManager> pathManagerValidator = ValidationFactory.CreateValidator<PathManager>();
            environmentMonitor.ValidationResults.AddAllResults(pathManagerValidator.Validate(pathManager));

            logManager = containerProvider.Resolve<ILogManager>();
            logManager.Initialize();

            Validator<LogManager> logManagerValidator = ValidationFactory.CreateValidator<LogManager>();
            environmentMonitor.ValidationResults.AddAllResults(logManagerValidator.Validate(logManager));
            if (environmentMonitor.ValidationResults.IsValid)
                logManager.Load();

            dataBaseManager = containerProvider.Resolve<IDataBaseManager>();
            dataBaseManager.Initialize();

            dataBaseManager.Load();
        }

        private void LoadFrameModuleCatalog()
        {
            FrameModuleCatalogExtension frameModuleCatalogExtension = new FrameModuleCatalogExtension(containerProvider);
            frameModuleCatalogExtension.Load();

            moduleManager.Run();
        }
    }
}
