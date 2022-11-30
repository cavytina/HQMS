using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HQMS.Frame.Kernel.Infrastructure
{
    public enum LogPart
    {
        [Description("数据库日志")]
        DataBase = 1,

        [Description("服务事件日志")]
        ServicEvent = 2
    }
}
