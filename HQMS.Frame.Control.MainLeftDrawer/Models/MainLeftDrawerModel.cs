using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Events;
using HQMS.Frame.Kernel.Infrastructure;
using HQMS.Frame.Kernel.Environment;
using HQMS.Frame.Kernel.Events;
using HQMS.Frame.Kernel.Services;

namespace HQMS.Frame.Control.MainLeftDrawer.Models
{
    public class MainLeftDrawerModel : BindableBase
    {
        public ObservableCollection<MainLeftDrawerMenuItem> menuItems;
        public ObservableCollection<MainLeftDrawerMenuItem> MenuItems
        {
            get => menuItems;
            set => SetProperty(ref menuItems, value);
        }

        IEventAggregator eventAggregator;
        IEnvironmentMonitor environmentMonitor;
        IEventServiceController eventServiceController;

        string requestJsonText;

        public MainLeftDrawerModel (IContainerProvider containerProviderArgs)
        {
            menuItems = new ObservableCollection<MainLeftDrawerMenuItem>();

            eventAggregator = containerProviderArgs.Resolve<IEventAggregator>();
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
            eventServiceController= containerProviderArgs.Resolve<IEventServiceController>();
        }

        public void RequestMenuItemData()
        {
            requestJsonText = eventServiceController.Request(EventServicePart.MenuListService, FrameModulePart.MainLeftDrawerModule, FrameModulePart.ServiceModule, new EmptyRequestServiceContentKind());
            eventAggregator.GetEvent<RequestServiceEvent>().Publish(requestJsonText);
        }

        public void LoadTopLevelMenuItemData()
        {
            var rootMenuItemInfos = environmentMonitor.MenuSettings.ToList().FindAll(x => x.SuperCode == "");

            foreach (MenuKind rootMenuItemInfo in rootMenuItemInfos)
            {
                MainLeftDrawerMenuItem mainLeftDrawerMenuItem = new MainLeftDrawerMenuItem();
                mainLeftDrawerMenuItem.MenuItemCode = rootMenuItemInfo.Code;
                mainLeftDrawerMenuItem.SuperMenuItemCode = rootMenuItemInfo.SuperCode;
                mainLeftDrawerMenuItem.MenuItemReferName = rootMenuItemInfo.ReferName;
                mainLeftDrawerMenuItem.NextMenuItems = FindNextMenuItemCollection(rootMenuItemInfo.Code);

                MenuItems.Add(mainLeftDrawerMenuItem);
            }
        }

        private ObservableCollection<MainLeftDrawerMenuItem> FindNextMenuItemCollection(string superMenuItemCodeArgs)
        {
            ObservableCollection<MainLeftDrawerMenuItem> mainLeftDrawerMenuItems = new ObservableCollection<MainLeftDrawerMenuItem>();

            var nextMenuItemInfos = environmentMonitor.MenuSettings.ToList().FindAll(x => x.SuperCode == superMenuItemCodeArgs);

            foreach (MenuKind menuInfo in nextMenuItemInfos)
            {
                MainLeftDrawerMenuItem mainLeftDrawerMenuItem = new MainLeftDrawerMenuItem();
                mainLeftDrawerMenuItem.MenuItemCode = menuInfo.Code;
                mainLeftDrawerMenuItem.SuperMenuItemCode = menuInfo.SuperCode;
                mainLeftDrawerMenuItem.MenuItemReferName = menuInfo.ReferName;
                mainLeftDrawerMenuItem.NextMenuItems = FindNextMenuItemCollection(menuInfo.Code);
                mainLeftDrawerMenuItem.MenuItemSelected += MainLeftDrawerMenuItem_MenuItemSelected;

                mainLeftDrawerMenuItems.Add(mainLeftDrawerMenuItem);
            }

            return mainLeftDrawerMenuItems;
        }

        private void MainLeftDrawerMenuItem_MenuItemSelected(object sender, MenuItemSelectedEventArgs e)
        {
            RequestMenuItemKind requestMenuItem = new RequestMenuItemKind
            {
                MenuItemCode = e.MenuItemCode
            };

            requestJsonText = eventServiceController.Request(EventServicePart.MenuItemService, FrameModulePart.MainLeftDrawerModule, FrameModulePart.ServiceModule, requestMenuItem);
            eventAggregator.GetEvent<RequestServiceEvent>().Publish(requestJsonText);
        }
    }
}
