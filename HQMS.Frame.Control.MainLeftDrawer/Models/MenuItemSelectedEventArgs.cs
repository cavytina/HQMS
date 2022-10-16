using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Frame.Control.MainLeftDrawer.Models
{
    public class MenuItemSelectedEventArgs : EventArgs
    {
        string menuItemCode;
        public string MenuItemCode
        {
            get => menuItemCode;
            set => menuItemCode = value;
        }

        public MenuItemSelectedEventArgs()
        {

        }

        public MenuItemSelectedEventArgs(string menuItemCodeArgs)
        {
            this.MenuItemCode = menuItemCodeArgs;
        }
    }
}
