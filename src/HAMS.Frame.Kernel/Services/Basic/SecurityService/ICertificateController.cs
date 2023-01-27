using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Frame.Kernel.Services
{
    public interface ICertificateController
    {
        /// <summary>
        /// 程序证书有效期验证
        /// </summary>
        bool ApplictionCertificateValidate(byte[] certificateDataArg,out DateTime validTimeArg);
    }
}
