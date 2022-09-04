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
        IPathLauncher pathLauncher;
        ILogController logController;
        IDataBaseLauncher dataBaseLauncher;

        public void OnInitialized(IContainerProvider containerProviderArgs)
        {
            containerProvider = containerProviderArgs;

            InitializeAndValidationServices();

            LoadFrameModuleCatalog();
        }

        public void RegisterTypes(IContainerRegistry containerRegistryArgs)
        {
            containerRegistryArgs.RegisterSingleton<IEnvironmentMonitor, EnvironmentMonitor>();

            containerRegistryArgs.Register<IPathLauncher, PathLauncher>();
            containerRegistryArgs.Register<ILogController, TextLogController>();
            containerRegistryArgs.Register<IDataBaseLauncher, DataBaseLauncher>();
            containerRegistryArgs.Register<ICipherController, SMController>();

            containerRegistryArgs.Register<IDataBaseController, NativeBaseController>("Native");
            containerRegistryArgs.Register<IDataBaseController, BAGLDBController>("BAGLDB");
        }

        private void InitializeAndValidationServices()
        {
            environmentMonitor = containerProvider.Resolve<IEnvironmentMonitor>();

            pathLauncher = containerProvider.Resolve<PathLauncher>();
            pathLauncher.Initialize();

            Validator<PathLauncher> pathLauncherValidator = ValidationFactory.CreateValidator<PathLauncher>();
            environmentMonitor.ValidationResults.AddAllResults(pathLauncherValidator.Validate(pathLauncher));

            if (environmentMonitor.ValidationResults.IsValid)
                pathLauncher.Load();

            logController = containerProvider.Resolve<ILogController>();
            Validator<TextLogController> txtLogControllerValidator = ValidationFactory.CreateValidator<TextLogController>();
            environmentMonitor.ValidationResults.AddAllResults(txtLogControllerValidator.Validate(logController));

            dataBaseLauncher= containerProvider.Resolve<IDataBaseLauncher>();
            dataBaseLauncher.Initialize();

            dataBaseLauncher.Load();
            dataBaseLauncher.Save();
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
