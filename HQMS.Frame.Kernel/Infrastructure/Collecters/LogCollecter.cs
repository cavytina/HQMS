using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HQMS.Frame.Kernel.Services;

namespace HQMS.Frame.Kernel.Infrastructure
{
    public class LogCollecter : List<LogKind>, ICollecter<ILogController>
    {

        /// <summary>
        /// 获取指定日志目录控制器
        /// </summary>
        /// <param name="nameIndex">以字符串形式传入LogPart列举值:DataBase,ServicEvent</param>
        /// <returns></returns>
        public ILogController GetContent(string nameIndex)
        {
            return Find(x => x.Name == nameIndex).LogController;
        }

        public LogKind this[string nameIndex]
        {
            set { base[FindIndex(x => x.Name == nameIndex)] = value; }
            get { return Find(x => x.Name == nameIndex); }
        }
    }
}
