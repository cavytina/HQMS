using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Modularity;
using Prism.Ioc;
using HQMS.Frame.Kernel.Core;
using HQMS.Frame.Kernel.Environment;
using HQMS.Frame.Kernel.Services;

namespace HQMS.Frame.Kernel.Extension
{
    public class FrameModuleCatalogExtension : ModuleCatalog
    {
        IDataBaseController nativeBaseController;
        IEnvironmentMonitor environmentMonitor;
        IModuleCatalog moduleCatalog;

        string sqlString;
        List<BaseKind> frameModuleCatalogHub;

        public FrameModuleCatalogExtension(IContainerProvider containerProviderArgs)
        {
            environmentMonitor= containerProviderArgs.Resolve<IEnvironmentMonitor>();
            nativeBaseController = environmentMonitor.DataBaseSetting.GetDataBaseController("Native");
            moduleCatalog = containerProviderArgs.Resolve<IModuleCatalog>();
        }

        protected override void InnerLoad()
        {
            sqlString = "SELECT Code,Name,Content,Description,Rank,Flag FROM System_ModuleSetting";
            nativeBaseController.Query<BaseKind>(sqlString, out frameModuleCatalogHub);

            var moduleCatalogInfos = from moduleCatalogInfoHub in frameModuleCatalogHub
                                 where moduleCatalogInfoHub.Flag
                                 orderby moduleCatalogInfoHub.Rank
                                 select moduleCatalogInfoHub;

            foreach (BaseKind moduleCatalogInfo in moduleCatalogInfos)
            {
                ModuleInfo moduleInfo = new ModuleInfo
                {
                    ModuleName = moduleCatalogInfo.Name,
                    ModuleType = moduleCatalogInfo.Description,
                    Ref = GetFileAbsoluteUri(moduleCatalogInfo.Content),
                    InitializationMode = InitializationMode.WhenAvailable
                };

                moduleCatalog.AddModule(moduleInfo);
            }
        }
    }
}
