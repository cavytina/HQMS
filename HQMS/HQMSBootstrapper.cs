using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Unity;
using Prism.Ioc;
using Prism.Modularity;
using MaterialDesignThemes.Wpf;
using HQMS.Views;
using HQMS.Frame.Kernel;

namespace HQMS
{
    public class HQMSBootstrapper : PrismBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<LoginWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistryArgs)
        {
            containerRegistryArgs.RegisterSingleton<ISnackbarMessageQueue, SnackbarMessageQueue>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalogArgs)
        {
            Type kernelModuleType = typeof(KernelModule);

            moduleCatalogArgs.AddModule(new ModuleInfo()
            {
                ModuleName = kernelModuleType.Name,
                ModuleType = kernelModuleType.AssemblyQualifiedName,
                InitializationMode = InitializationMode.WhenAvailable
            });
        }
    }
}
