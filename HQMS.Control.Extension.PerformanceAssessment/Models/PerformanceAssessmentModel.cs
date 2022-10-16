using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using HQMS.Frame.Kernel.Infrastructure;
using HQMS.Frame.Kernel.Environment;
using HQMS.Frame.Kernel.Services;

namespace HQMS.Control.Extension.PerformanceAssessment.Models
{
    public class PerformanceAssessmentModel : BindableBase
    {
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController dataBaseController;
        string sqlStatement;
        List<BaseKind> menuItemHub;

        ObservableCollection<MenuItem> menuItems;
        public ObservableCollection<MenuItem> MenuItems
        {
            get => menuItems;
            set => SetProperty(ref menuItems, value);
        }

        string currentMenuItemName;
        public string CurrentMenuItemName
        {
            get => currentMenuItemName;
            set => SetProperty(ref currentMenuItemName, value);
        }

        public PerformanceAssessmentModel (IContainerProvider containerProviderArgs)
        {
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
            dataBaseController = environmentMonitor.DataBaseSetting.GetDataBaseController("Native");

            LoadMenuData();
        }

        private void LoadMenuData()
        {
            MenuItems = new ObservableCollection<MenuItem>();
            sqlStatement = "SELECT Code,Name,Content,Description,Rank,Flag FROM HQMS_PerformanceAssessment_MenuSetting";
            if (dataBaseController.Query<BaseKind>(sqlStatement,out menuItemHub))
            {
                foreach (BaseKind menuItem in menuItemHub)
                {
                    MenuItems.Add(new MenuItem
                    {
                        MenuItemCode = menuItem.Code,
                        MenuItemName = menuItem.Name,
                        MenuItemContent = menuItem.Content
                    });
                }
            }
        }
    }
}
