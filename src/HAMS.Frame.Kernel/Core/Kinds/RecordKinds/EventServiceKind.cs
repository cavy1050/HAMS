using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HAMS.Frame.Kernel.Core
{
    public abstract class EventServiceKind : RecordKind
    {
        /// <summary>
        /// 服务代码
        /// </summary>
        [JsonProperty(PropertyName = "svc_code", Order = 1)]
        public override string Content { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        [JsonProperty(PropertyName = "svc_name", Order = 2)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        [JsonProperty(PropertyName = "svc_type", Order = 3)]
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual EventServiceTypePart Type { get; set; }

        /// <summary>
        /// 服务序号
        /// </summary>
        [JsonProperty(PropertyName = "msg_code", Order = 4)]
        public override string Code { get; set; }

        /// <summary>
        /// 服务源模块名称
        /// </summary>
        [JsonProperty(PropertyName = "souc_mod_name", Order = 5)]
        [JsonConverter(typeof(StringEnumConverter))]
        public FrameModulePart SourceModuleName { get; set; }

        /// <summary>
        /// 服务时间
        /// </summary>
        [JsonProperty(PropertyName = "svc_time", Order = 7)]
        public override string RecordTime { get; set; }

        /// <summary>
        /// 服务内容
        /// </summary>
        [JsonProperty(PropertyName = "svc_cont", Order = 8)]
        public IEventServiceContent EventServiceContent { get; set; }

        [JsonIgnore]
        public override string Note { get; set; }

        [JsonIgnore]
        public override bool EnabledFlag { get; set; }
    }
}
