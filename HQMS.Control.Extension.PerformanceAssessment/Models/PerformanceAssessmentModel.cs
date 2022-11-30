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
        IDataBaseController nativeController;
        string sqlStatement;
        List<BaseKind> menuHub;

        ObservableCollection<MenuKind> menus;
        public ObservableCollection<MenuKind> Menus
        {
            get => menus;
            set => SetProperty(ref menus, value);
        }

        string currentMenuName;
        public string CurrentMenuName
        {
            get => currentMenuName;
            set => SetProperty(ref currentMenuName, value);
        }

        public PerformanceAssessmentModel (IContainerProvider containerProviderArgs)
        {
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
            nativeController = environmentMonitor.DataBaseSetting.GetContent("Native");

            Menus = new ObservableCollection<MenuKind>();

            LoadMenuData();
        }

        private void LoadMenuData()
        {
            sqlStatement = "SELECT Code,Name,Content,Description,Rank,Flag FROM HQMS_PerformanceAssessment_DictionarySetting WHERE CategoryCode = '01'";

            if (nativeController.Query<BaseKind>(sqlStatement, out menuHub))
            {
                foreach (BaseKind menu in menuHub)
                {
                    Menus.Add(new MenuKind
                    {
                        MenuCode = menu.Code,
                        MenuName = menu.Name,
                        MenuContent = menu.Content
                    });
                }
            }
        }
    }
}
