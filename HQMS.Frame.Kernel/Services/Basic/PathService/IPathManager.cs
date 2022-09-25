using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Frame.Kernel.Services
{
    /// <summary>
    /// 提供对IEnvironmentMonitor.AppPathSetting初始化、持久化的支持
    /// 程序运行目录、本地数据库文件路径自动获取为默认值,且不支持自定义配置
    /// </summary>
    public interface IPathManager
    {
        /// <summary>
        /// 获取默认程序运行目录、默认本地数据库文件路径
        /// </summary>
        void Initialize();

        /// <summary>
        /// 程序退出时持久化IEnvironmentMonitor.AppPathSetting设置值.
        /// </summary>
        void Save();
    }
}
