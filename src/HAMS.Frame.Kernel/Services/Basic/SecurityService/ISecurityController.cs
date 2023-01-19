using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Frame.Kernel.Services
{
    public interface ISecurityController
    {
        /// <summary>
        /// 封装数据库连接字符串加密过程
        /// </summary>
        string DataBaseConnectionStringEncrypt(string plainTextArg);

        /// <summary>
        /// 封装数据库连接字符串解密过程
        /// </summary>
        string DataBaseConnectionStringDecrypt(string cipherTextArg);

        /// <summary>
        /// 程序证书有效期验证
        /// </summary>
        /// TODO
        // bool ApplictionCertificateValidate(byte[] certificateDataArg);
    }
}
