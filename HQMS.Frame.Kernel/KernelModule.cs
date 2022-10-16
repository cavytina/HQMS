using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Modularity;
using HQMS.Frame.Kernel.Environment;
using HQMS.Frame.Kernel.Services;

namespace HQMS.Frame.Kernel
{
    public class KernelModule : IModule
    {
        KernelLauncher kernelLauncher;

        public void OnInitialized(IContainerProvider containerProviderArgs)
        {
            kernelLauncher = new KernelLauncher(containerProviderArgs);
            kernelLauncher.Initialize();
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

            containerRegistryArgs.Register<IEventServiceController, EventServiceController>();
        }
    }
}
