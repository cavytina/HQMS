using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Frame.Kernel.Infrastructure
{
    public enum EventServicePart
    {
        /// <summary>
        /// 用户认证服务
        /// </summary>
        AccountAuthenticationService = 1,

        /// <summary>
        /// 程序状态服务
        /// </summary>
        ApplicationStatusService = 2,

        /// <summary>
        /// 菜单清单服务
        /// </summary>
        MenuListService = 3,

        /// <summary>
        /// 菜单项目服务
        /// </summary>
        MenuItemService = 4,
    }
}
