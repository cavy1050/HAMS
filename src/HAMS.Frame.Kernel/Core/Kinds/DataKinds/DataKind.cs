
using Newtonsoft.Json;

namespace HAMS.Frame.Kernel.Core
{
    /// <summary>
    /// 操作信息类型 记录程序框架操作信息
    /// </summary>
    public class DataKind : BaseKind
    {
        /// <summary>
        /// 记录时间 时间格式为yyyy-MM-dd HHmmss
        /// </summary>
        /// 
        /// <remarks>
        /// eventJsonSentence.svc_time
        /// </remarks>
        [JsonProperty(PropertyName = "svc_time", Order = 7)]
        public string RecordTime { get; set; }
    }
}
