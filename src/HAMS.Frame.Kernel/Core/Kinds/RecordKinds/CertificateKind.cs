namespace HAMS.Frame.Kernel.Core
{
    public class CertificateKind : RecordKind
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
    }
}
