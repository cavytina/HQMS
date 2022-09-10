using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Modularity;
using Prism.Ioc;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using HQMS.Frame.Kernel.Environment;
using HQMS.Frame.Kernel.Services;
using HQMS.Frame.Kernel.Extension;
using System.Windows;

namespace HQMS.Frame.Kernel
{
    public class KernelModule : IModule
    {
        IContainerProvider containerProvider;
        IModuleManager moduleManager;
        IEnvironmentMonitor environmentMonitor;
        IPathManager pathManager;
        ILogManager logManager;
        IDataBaseManager dataBaseManager;

        public void OnInitialized(IContainerProvider containerProviderArgs)
        {
            containerProvider = containerProviderArgs;

            InitializeAndValidationServices();

            LoadFrameModuleCatalog();
        }

        public void RegisterTypes(IContainerRegistry containerRegistryArgs)
        {
            containerRegistryArgs.RegisterSingleton<IEnvironmentMonitor, EnvironmentMonitor>();

            containerRegistryArgs.Register<ILogController, TextLogController>();
            containerRegistryArgs.Register<IDataBaseController, NativeBaseController>("Native");
            containerRegistryArgs.Register<IDataBaseController, BAGLDBController>("BAGLDB");
            containerRegistryArgs.Register<ICipherController, SMController>();

            containerRegistryArgs.Register<IPathManager, PathManager>();
            containerRegistryArgs.Register<ILogManager, LogManager>();
            containerRegistryArgs.Register<IDataBaseManager, DataBaseManager>();
        }

        private void InitializeAndValidationServices()
        {
            environmentMonitor = containerProvider.Resolve<IEnvironmentMonitor>();

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

            pathManager.Save();
            logManager.Save();
            dataBaseManager.Save();
        }

        private void LoadFrameModuleCatalog()
        {
            moduleManager = containerProvider.Resolve<IModuleManager>();
            FrameModuleCatalogExtension frameModuleCatalogExtension = new FrameModuleCatalogExtension(containerProvider);
            frameModuleCatalogExtension.Load();

            moduleManager.Run();
        }
    }
}
