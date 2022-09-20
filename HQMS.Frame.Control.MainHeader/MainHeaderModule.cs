using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Modularity;
using Prism.Ioc;
using Prism.Regions;
using HQMS.Frame.Control.MainHeader.Views;

namespace HQMS.Frame.Control.MainHeader
{
    public class MainHeaderModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("MainHeaderRegion", typeof(ContentView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
