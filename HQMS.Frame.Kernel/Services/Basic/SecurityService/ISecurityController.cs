using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Frame.Kernel.Services
{
    public interface ISecurityController
    {
        /// <summary>
        /// 封装数据库连接字符串加密过程
        /// </summary>
        /// <param name="plainTextArgs"></param>
        /// <returns></returns>
        string DataBaseConnectionStringEncrypt(string plainTextArgs);

        /// <summary>
        /// 封装数据库连接字符串解密过程
        /// </summary>
        /// <param name="cipherTextArgs"></param>
        /// <returns></returns>
        string DataBaseConnectionStringDecrypt(string cipherTextArgs);
    }
}
