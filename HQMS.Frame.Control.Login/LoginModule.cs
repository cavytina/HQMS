using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using HQMS.Frame.Control.Login.Views;
using HQMS.Frame.Control.Login.ViewModels;

namespace HQMS.Frame.Control.Login
{
    public class LoginModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("LoginHeaderRegion", typeof(LoginHeaderView));
            regionManager.RegisterViewWithRegion("LoginContentRegion", typeof(LoginContentView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LoginContentView, LoginContentViewModel>();
            containerRegistry.RegisterForNavigation<LoginConfigView, LoginConfigViewModel>();
        }
    }
}
