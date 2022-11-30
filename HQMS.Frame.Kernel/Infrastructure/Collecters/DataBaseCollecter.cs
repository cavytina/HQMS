using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HQMS.Frame.Kernel.Services;

namespace HQMS.Frame.Kernel.Infrastructure
{
    public class DataBaseCollecter : List<DataBaseKind>, ICollecter<IDataBaseController>
    {
        /// <summary>
        /// 获取指定数据库操作控制器
        /// </summary>
        /// <param name="nameIndex">以字符串形式传入DataBasePart列举值:Native,BAGLDB</param>
        /// <returns></returns>
        public IDataBaseController GetContent(string nameIndex)
        {
            return Find(x => x.Name == nameIndex).DataBaseController;
        }

        public DataBaseKind this[string nameIndex]
        {
            set { base[FindIndex(x => x.Name == nameIndex)] = value; }
            get { return Find(x => x.Name == nameIndex); }
        }
    }
}
