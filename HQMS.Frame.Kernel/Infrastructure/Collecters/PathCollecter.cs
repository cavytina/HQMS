using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Frame.Kernel.Infrastructure
{
    public class PathCollecter : List<BaseKind>, ICollecter<string>
    {
        /// <summary>
        /// 获取指定程序路径
        /// </summary>
        /// <param name="nameIndex">以字符串形式传入PathPart列举值:ApplictionCatalogue,NativeDataBaseFilePath,TextLogFilePath</param>
        /// <returns></returns>
        public string GetContent(string nameIndex)
        {
            return Find(x => x.Name == nameIndex).Content;
        }

        public BaseKind this[string nameIndex]
        {
            set { base[FindIndex(x => x.Name == nameIndex)] = value; }
            get { return Find(x => x.Name == nameIndex); }
        }
    }
}
