using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;

namespace HQMS.Frame.Control.MainLeftDrawer.Models
{
    public class MainLeftDrawerMenuItem : BindableBase
    {
        string menuItemCode;
        public string MenuItemCode
        {
            get => menuItemCode;
            set => SetProperty(ref menuItemCode, value);
        }

        string menuItemReferName;
        public string MenuItemReferName
        {
            get => menuItemReferName;
            set => SetProperty(ref menuItemReferName, value);
        }

        string superMenuItemCode;
        public string SuperMenuItemCode
        {
            get => superMenuItemCode;
            set => SetProperty(ref superMenuItemCode, value);
        }

        bool isExpanded;
        public bool IsExpanded
        {
            get => isExpanded;
            set => SetProperty(ref isExpanded, value);
        }

        bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                SetProperty(ref isSelected, value);

                if (isSelected)
                    OnMenuItemSelected(this.menuItemCode);
            }
        }

        public ObservableCollection<MainLeftDrawerMenuItem> nextMenuItems;
        public ObservableCollection<MainLeftDrawerMenuItem> NextMenuItems
        {
            get => nextMenuItems;
            set => SetProperty(ref nextMenuItems, value);
        }

        public event EventHandler<MenuItemSelectedEventArgs> MenuItemSelected;

        public void OnMenuItemSelected(string menuItemCodeArgs)
        {
            if (MenuItemSelected != null)
            {
                MenuItemSelected(this, new MenuItemSelectedEventArgs(menuItemCodeArgs));
            }
        }
    }
}
