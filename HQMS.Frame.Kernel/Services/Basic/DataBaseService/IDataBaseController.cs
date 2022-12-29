using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Frame.Kernel.Services
{
    /// <summary>
    /// 提供对数据库操作的基本支持.
    /// </summary>
    public interface IDataBaseController
    {
        /// <summary>
        /// 提供对简单查询操作的基本支持
        /// </summary>
        bool Query<T>(string queryStingArgs, out List<T> tHub);

        /// <summary>
        /// 提供对存储过程查询操作的基本支持，获取存储过程返回信息
        /// </summary>
        bool QueryWithMessage<T>(string queryStingArgs, out List<T> tHub, out string retStringArg);
        bool ExecuteScalar(string execStingArgs, out object objArgs);

        /// <summary>
        /// 提供对简单数据操作的基本支持
        /// </summary>
        bool Execute(string execStingArgs);

        /// <summary>
        /// 提供对存储过程执行操作的基本支持，获取存储过程返回信息
        /// </summary>
        bool ExecuteWithMessage(string execStingArgs, out string retStringArg);
    }
}
