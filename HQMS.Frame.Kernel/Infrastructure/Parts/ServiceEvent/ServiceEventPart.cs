using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Frame.Kernel.Infrastructure
{
    public enum ServiceEventPart
    {
        /// <summary>
        /// 用户认证服务，包含请求与应答
        /// </summary>
        AccountAuthenticationService = 1,

        /// <summary>
        /// 程序状态服务，包含请求
        /// </summary>
        ApplicationStatusService = 2
    }
}
