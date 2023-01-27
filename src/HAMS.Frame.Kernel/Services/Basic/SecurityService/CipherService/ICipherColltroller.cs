using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Frame.Kernel.Services
{
    public interface ICipherColltroller
    {
        /// <summary>
        /// 封装数据库连接字符串加密过程
        /// </summary>
        string DataBaseConnectionStringEncrypt(string plainTextArg);

        /// <summary>
        /// 封装数据库连接字符串解密过程
        /// </summary>
        string DataBaseConnectionStringDecrypt(string cipherTextArg);
    }
}
