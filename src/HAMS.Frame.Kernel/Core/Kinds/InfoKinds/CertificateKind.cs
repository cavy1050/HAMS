using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Frame.Kernel.Core
{
    public class CertificateKind : InfoKind
    {
        /// <summary>
        /// 证书序列号
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// 版本标识
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// 证书到期时间
        /// </summary>
        public string ExpirationDate { get; set; }

        /// <summary>
        /// 证书密钥M5效验
        /// </summary>
        public string MD5KeyData { get; set; }

        public CertificateKind()
        {

        }

        public CertificateKind(string codeArg, string contentArg, string noteArg, string recordTimeArg, bool enabledFlagArg,
                                string serialNumberArg, string expirationDateArg, string md5KeyDataArg) :
                                    base(codeArg, contentArg, noteArg, recordTimeArg, enabledFlagArg)
        {
            SerialNumber = serialNumberArg;
            ExpirationDate = expirationDateArg;
            MD5KeyData = md5KeyDataArg;
        }
    }
}
