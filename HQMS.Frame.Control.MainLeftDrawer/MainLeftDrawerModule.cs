using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using HQMS.Frame.Control.MainLeftDrawer.Views;

namespace HQMS.Frame.Control.MainLeftDrawer
{
    public class MainLeftDrawerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("MainLeftDrawerRegion", typeof(MainLeftDrawerView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}