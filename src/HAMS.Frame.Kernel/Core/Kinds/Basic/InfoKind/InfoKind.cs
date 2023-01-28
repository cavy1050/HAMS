
using Newtonsoft.Json;

namespace HAMS.Frame.Kernel.Core
{
    /// <summary>
    /// 操作信息类型 记录程序框架操作信息
    /// </summary>
    public class InfoKind : BaseKind
    {
        /// <summary>
        /// 记录时间 时间格式为yyyy-MM-dd HHmmss
        /// </summary>
        /// 
        /// <remarks>
        /// EventServiceJsonText.svc_time
        /// </remarks>
        [JsonProperty(PropertyName = "svc_time")]
        public string RecordTime { get; set; }

        public InfoKind() : base()
        {

        }

        public InfoKind(string codeArg, string contentArg, string noteArg, string recordTimeArg, bool enabledFlagArg) :
                            base(codeArg, contentArg, noteArg, enabledFlagArg)
        {
            RecordTime = recordTimeArg;
        }
    }
}
