using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Frame.Kernel.Services
{
    public interface ILogManager
    {
        /// <summary>
        /// 获取默认日志文件路径，并加载至IEnvironmentMonitor.LogSetting
        /// </summary>
        void Initialize();

        /// <summary>
        /// 持久化IEnvironmentMonitor.LogSetting
        /// </summary>
        void Save();
    }
}
