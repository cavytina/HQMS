using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Prism.Ioc;

namespace HQMS.Frame.Service.Peripherals
{
    public class MD5Controller : IOneWayCipherController
    {
        StringBuilder sb;

        public MD5Controller(IContainerProvider containerProviderArgs)
        {
            sb = new StringBuilder();
        }

        public string Encrypt(string plaintextArgs)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                //将输入字符串转换为字节数组并计算哈希。
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(plaintextArgs));

                //X为     十六进制 X都是大写 x都为小写
                //2为 每次都是两位数
                //假设有两个数10和26，正常情况十六进制显示0xA、0x1A，这样看起来不整齐，为了好看，可以指定"X2"，这样显示出来就是：0x0A、0x1A。 
                //遍历哈希数据的每个字节
                //并将每个字符串格式化为十六进制字符串。
                int length = data.Length;
                for (int i = 0; i < length; i++)
                    sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
