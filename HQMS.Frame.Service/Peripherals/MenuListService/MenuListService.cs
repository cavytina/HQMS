using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using HQMS.Frame.Kernel.Infrastructure;
using HQMS.Frame.Kernel.Environment;
using HQMS.Frame.Kernel.Services;

namespace HQMS.Frame.Service.Peripherals
{
    public class MenuListController : IMenuListController
    {
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeController;
        string queryText;

        public MenuListController (IContainerProvider containerProviderArgs)
        {
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
            nativeController = environmentMonitor.DataBaseSetting.GetContent("Native");
        }

        public List<MenuKind> Load()
        {
            List<MenuKind> retHub = new List<MenuKind>();

            queryText = "SELECT Code,Name, ReferName,Content,Description,SuperCode,SuperName,Rank,Flag FROM System_MenuSetting";
            if (nativeController.Query<MenuKind>(queryText,out retHub))
            {
                var ret = from retMenuHub in retHub
                          where retMenuHub.Flag
                          orderby retMenuHub.Code
                          select retMenuHub;

                retHub = ret.ToList();
            }

            return retHub;
        }
    }
}
