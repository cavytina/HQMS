using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Frame.Kernel.Services
{
    /// <summary>
    /// 提供对IEnvironmentMonitor.DataBaseSetting初始化、持久化的支持.
    /// </summary>
    public interface IDataBaseLauncher
    {
        /// <summary>
        /// 初始化本地数据库、网络数据库连接字符串参数
        /// </summary>
        void Initialize();

        /// <summary>
        /// 对IEnvironmentMonitor.DataBaseSetting加载数据库
        /// </summary>
        void Load();

        /// <summary>
        /// 加密持久化本地数据库、网络数据库信息
        /// </summary>
        void Save();
    }
}
