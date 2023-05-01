using Newtonsoft.Json;

namespace HAMS.Frame.Kernel.Core
{
    public abstract class EventKind : RecordKind
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
        public virtual string Type { get; set; }

        /// <summary>
        /// 服务行为类型
        /// </summary>
        [JsonProperty(PropertyName = "svc_bhvr_type", Order = 4)]
        public virtual string Behaviour { get; set; }

        /// <summary>
        /// 服务序号
        /// </summary>
        [JsonProperty(PropertyName = "msg_code", Order = 5)]
        public override string Code { get; set; }

        /// <summary>
        /// 服务源模块名称
        /// </summary>
        [JsonProperty(PropertyName = "souc_mdl", Order = 6)]
        public string SourceModule { get; set; }

        /// <summary>
        /// 应答服务目标模块名称
        /// </summary>
        [JsonProperty(PropertyName = "tagt_mdl", Order = 7)]
        public string TargetModule { get; set; }

        /// <summary>
        /// 服务时间
        /// </summary>
        [JsonProperty(PropertyName = "svc_time", Order = 8)]
        public override string RecordTime { get; set; }

        /// <summary>
        /// 服务内容
        /// </summary>
        [JsonProperty(PropertyName = "svc_cont", Order = 9)]
        public IEventContent EventContent { get; set; }

        [JsonIgnore]
        public override string Note { get; set; }

        [JsonIgnore]
        public override bool EnabledFlag { get; set; }
    }
}
