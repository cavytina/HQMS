using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Modularity;
using Prism.Ioc;
using HQMS.Frame.Kernel.Infrastructure;
using HQMS.Frame.Kernel.Environment;
using HQMS.Frame.Kernel.Services;

namespace HQMS.Frame.Service.Extension
{
    public class MenuModuleCatalogExtension : ModuleCatalog
    {
        string menuItemCode;
        IEnvironmentMonitor environmentMonitor;
        IModuleCatalog moduleCatalog;

        public MenuModuleCatalogExtension(IContainerProvider containerProviderArgs,string menuItemCodeArgs)
        {
            menuItemCode = menuItemCodeArgs;
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
            moduleCatalog = containerProviderArgs.Resolve<IModuleCatalog>();
        }

        protected override void InnerLoad()
        {
            if (menuItemCode != null)
            {
                ModuleInfo moduleInfo = new ModuleInfo
                {
                    ModuleName = environmentMonitor.MenuSettings[menuItemCode].Name,
                    ModuleType = environmentMonitor.MenuSettings[menuItemCode].Description,
                    Ref = GetFileAbsoluteUri(environmentMonitor.MenuSettings[menuItemCode].Content),
                    InitializationMode = InitializationMode.WhenAvailable
                };

                moduleCatalog.AddModule(moduleInfo);
            }
        }
    }
}
