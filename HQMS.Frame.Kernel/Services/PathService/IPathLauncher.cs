using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Frame.Kernel.Services
{
    /// <summary>
    /// 提供对IEnvironmentMonitor.AppPathSetting初始化、持久化的支持.
    /// </summary>
    public interface IPathLauncher
    {
        /// <summary>
        /// 获取默认程序运行目录、默认本地数据库文件路径、默认日志文件路径
        /// </summary>
        void Initialize();

        /// <summary>
        /// 对IEnvironmentMonitor.AppPathSetting初始化默认值
        /// </summary>
        void Load();

        /// <summary>
        /// 持久化程序运行目录、日志路径信息.
        /// </summary>
        void Save();
    }
}
