using Newtonsoft.Json;

namespace HAMS.Frame.Kernel.Core
{
    /// <summary>
    /// 根类型
    /// </summary>
    public abstract class BaseKind
    {
        /// <summary>
        /// 标识项,采用ULID编码
        /// </summary>
        /// <remarks>
        /// <para>
        /// ULID是JavaScript原生编码，26位字符格式.
        /// 更多信息请参见<seealso cref="ULID" href="https://github.com/ulid"/>
        /// 程序采用ULID-C#实现库：<seealso cref="NUlid" href="https://github.com/RobThree/NUlid"/>
        /// </para>
        /// <para>
        /// eventJsonSentence.msg_code ULID编码
        /// </para>
        /// </remarks>
        [JsonProperty(PropertyName = "msg_code", Order = 4)]
        public virtual string Code { get; set; }

        /// <summary>
        /// 内容项
        /// </summary>
        /// 
        /// <remarks>
        /// eventJsonSentence中忽略此项
        /// </remarks>
        [JsonIgnore]
        public virtual string Content { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        /// 
        /// <remarks>
        /// eventJsonSentence中忽略此项
        /// </remarks>
        [JsonIgnore]
        public virtual string Note { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>
        /// 
        /// <remarks>
        /// 1：有效 0：无效
        /// eventJsonSentence中忽略此项
        /// </remarks>
        [JsonIgnore]
        public virtual bool EnabledFlag { get; set; }

        public BaseKind()
        {

        }

        public BaseKind(string codeArg, string contentArg, string noteArg, bool enabledFlagArg)
        {
            Code = codeArg;
            Content = contentArg;
            Note = noteArg;
            EnabledFlag = enabledFlagArg;
        }
    }
}
