using Newtonsoft.Json;

namespace HAMS.Frame.Kernel.Core
{
    public class ResponseEventKind : EventKind
    {
        /// <summary>
        /// 应答事件结果
        /// </summary>
        [JsonProperty(PropertyName = "ret_rst", Order = 9)]
        public int ReturnResult { get; set; }

        /// <summary>
        /// 应答事件错误信息
        /// </summary>
        [JsonProperty(PropertyName = "ret_msg", Order = 10)]
        public string ReturnMessage { get; set; }
    }
}
