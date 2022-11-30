using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HQMS.Frame.Kernel.Infrastructure
{
    public enum PathPart
    {
        [Description("程序运行目录")]
        ApplictionCatalogue = 1,

        [Description("本地数据库文件路径")]
        NativeDataBaseFilePath = 2,

        [Description("日志文件路径")]
        TextLogFilePath = 3
    }
}
