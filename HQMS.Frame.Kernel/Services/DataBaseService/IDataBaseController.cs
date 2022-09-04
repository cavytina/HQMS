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
        bool Query<T>(string queryStingArgs, out List<T> tHub);
        bool ExecuteScalar(string execStingArgs, out object objArgs);
        bool Execute(string execStingArgs);
    }
}
