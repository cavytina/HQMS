using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using Prism.Ioc;
using HQMS.Frame.Kernel.Environment;

namespace HQMS.Frame.Kernel.Services
{
    public class SecurityController : ISecurityController
    {
        /// <summary>
        /// DES加解密 密钥和向量必须为8字节数据
        /// </summary>
        static readonly byte[] desKeyBytes = Encoding.Default.GetBytes("s6|x*GB3");
        static readonly byte[] desIVBytes = Encoding.Default.GetBytes("Dw_R~L6o");

        /// <summary>
        /// AES加解密 密钥必须为16位(128位)、24位(192位)、32位(256位) 向量为16位
        /// </summary>
        static readonly byte[] aesKeyBytes = Encoding.Default.GetBytes("#>?q-j6T22Qv1H8u");
        static readonly byte[] aesIVBytes = Encoding.Default.GetBytes("m%95-6C)#^')8<TL");

        ILogController logController;
        IEnvironmentMonitor environmentMonitor;

        public SecurityController(IContainerProvider containerProviderArgs)
        {
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
            logController = environmentMonitor.LogSetting.GetContent("DataBase");
        }

        public string DataBaseConnectionStringEncrypt(string plainTextArgs)
        {
            DESCryptoServiceProvider desCryptoSrvPrv = new DESCryptoServiceProvider();
            MemoryStream desMemStream = new MemoryStream();

            AesCryptoServiceProvider aesCryptoSrvPrv = new AesCryptoServiceProvider();
            MemoryStream aesMemStream = new MemoryStream();

            try
            {
                byte[] desTextArr = Encoding.Default.GetBytes(plainTextArgs);
                CryptoStream desCryStream = new CryptoStream(desMemStream, desCryptoSrvPrv.CreateEncryptor(desKeyBytes, desIVBytes), CryptoStreamMode.Write);
                desCryStream.Write(desTextArr, 0, desTextArr.Length);
                desCryStream.FlushFinalBlock();
            }
            catch (Exception ex)
            {
                logController.WriteLog(ex.Message);
            }

            try
            {
                byte[] aesTextArr = desMemStream.ToArray();
                CryptoStream aesCryStream = new CryptoStream(aesMemStream, aesCryptoSrvPrv.CreateEncryptor(aesKeyBytes, aesIVBytes), CryptoStreamMode.Write);
                aesCryStream.Write(aesTextArr, 0, aesTextArr.Length);
                aesCryStream.FlushFinalBlock();
            }
            catch (Exception ex)
            {
                logController.WriteLog(ex.Message);
            }

            return Convert.ToBase64String(aesMemStream.ToArray());
        }

        public string DataBaseConnectionStringDecrypt(string cipherTextArgs)
        {
            AesCryptoServiceProvider aesCryptoSrvPrv = new AesCryptoServiceProvider();
            MemoryStream aesMemStream = new MemoryStream();

            DESCryptoServiceProvider desCryptoSrvPrv = new DESCryptoServiceProvider();
            MemoryStream desMemStream = new MemoryStream();

            try
            {
                byte[] aesTextArr = Convert.FromBase64String(cipherTextArgs);
                CryptoStream aesCryStream = new CryptoStream(aesMemStream, aesCryptoSrvPrv.CreateDecryptor(aesKeyBytes, aesIVBytes), CryptoStreamMode.Write);
                aesCryStream.Write(aesTextArr, 0, aesTextArr.Length);
                aesCryStream.FlushFinalBlock();
            }
            catch (Exception ex)
            {
                logController.WriteLog(ex.Message);
            }

            try
            {
                byte[] desTextArr = aesMemStream.ToArray();
                CryptoStream desCryStream = new CryptoStream(desMemStream, desCryptoSrvPrv.CreateDecryptor(desKeyBytes, desIVBytes), CryptoStreamMode.Write);
                desCryStream.Write(desTextArr, 0, desTextArr.Length);
                desCryStream.FlushFinalBlock();
            }
            catch (Exception ex)
            {
                logController.WriteLog(ex.Message);
            }

            return Encoding.Default.GetString(desMemStream.ToArray());
        }
    }
}
