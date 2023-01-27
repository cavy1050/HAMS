using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Prism.Ioc;
using NUlid;
using HAMS.Frame.Kernel.Core;

namespace HAMS.Frame.Kernel.Services
{
    public class CertificateController:ICertificateController
    {
        public byte[] CertificateFileData { get; set; }

        public CertificateKind AnalyseData()
        {
            CertificateKind ret;

            X509Certificate2 certificateData = new X509Certificate2(CertificateFileData);
            MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] md5KeyData = md5CryptoServiceProvider.ComputeHash((certificateData.GetPublicKey()));

            ret = new CertificateKind
            {
                Code = Ulid.NewUlid().ToString(),
                Content = certificateData.GetPublicKeyString(),
                SerialNumber = certificateData.GetSerialNumberString(),
                Item = certificateData.GetNameInfo(X509NameType.SimpleName, true),
                ExpirationDate = certificateData.GetExpirationDateString(),
                RecordTime = DateTime.Now.ToString("g"),
                MD5KeyData = BitConverter.ToString(md5KeyData).Replace("-", "")
            };

            return ret;
        }
    }
}
