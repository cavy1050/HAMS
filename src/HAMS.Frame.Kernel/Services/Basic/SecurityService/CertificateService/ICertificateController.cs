using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAMS.Frame.Kernel.Core;

namespace HAMS.Frame.Kernel.Services
{
    public interface ICertificateController
    {
        byte[] CertificateFileData { get; set; }

        CertificateKind AnalyseData();
    }
}
