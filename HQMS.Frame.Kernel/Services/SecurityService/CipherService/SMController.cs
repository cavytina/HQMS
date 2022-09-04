using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using KYSharp.SM;

namespace HQMS.Frame.Kernel.Services
{
    public class SMController : ICipherController
    {
        readonly string sm4Key;
        SM4Utils sm4Utils;

        public SMController(IContainerProvider containerProviderArgs)
        {
            sm4Key = "037985f760b5c3d7";
            sm4Utils = new SM4Utils();
        }

        public string Encrypt(string plaintextArgs)
        {
            sm4Utils.secretKey = sm4Key;
            return sm4Utils.Encrypt_ECB(plaintextArgs);
        }

        public string Decrypt(string ciphertextArgs)
        {
            sm4Utils.secretKey = sm4Key;
            return sm4Utils.Decrypt_ECB(ciphertextArgs);
        }
    }
}
