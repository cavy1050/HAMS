namespace HAMS.Frame.Kernel.Core
{
    /// <summary>
    /// 根类型
    /// </summary>
    public class BaseKind
    {
        /// <summary>
        /// 标识项,采用ULID编码
        /// </summary>
        /// 
        /// <remarks>
        /// ULID是JavaScript原生编码，26位字符格式.
        /// 更多信息请参见<seealso cref="ULID" href="https://github.com/ulid"/>
        /// 程序采用ULID-C#实现库：<seealso cref="NUlid" href="https://github.com/RobThree/NUlid"/>
        /// </remarks>
        public string Code { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>
        /// 
        /// <remarks>
        /// 1：有效 0：无效
        /// </remarks>
        public bool EnabledFlag { get; set; }

        public BaseKind()
        {

        }

        public BaseKind(string codeArg, bool enabledFlagArg)
        {
            Code = codeArg;
            EnabledFlag = enabledFlagArg;
        }
    }
}
