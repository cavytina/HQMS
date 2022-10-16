using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Modularity;
using Prism.Ioc;
using HQMS.Frame.Service.Peripherals;

namespace HQMS.Frame.Service
{
    public class ServiceModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProviderArgs)
        {
            ServiceLauncher serviceLauncher = new ServiceLauncher(containerProviderArgs);
        }

        public void RegisterTypes(IContainerRegistry containerRegistryArgs)
        {
            containerRegistryArgs.Register<IAccountAuthenticationControler, AccountAuthenticationControler>();
            containerRegistryArgs.Register<IOneWayCipherController, MD5Controller>();
            containerRegistryArgs.Register<IMenuListController, MenuListController>();
        }
    }
}
