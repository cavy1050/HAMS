using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HAMS.Frame.Kernel.Core
{
    public class EventServiceKind : InfoKind
    {
        /// <summary>
        /// 服务代码
        /// </summary>
        /// 
        /// <remarks>
        /// EventServiceJsonText.svc_code
        /// </remarks>
        [JsonProperty(PropertyName = "svc_code", Order = 1)]
        public string Item { get; set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        /// 
        /// <remarks>
        /// EventServiceJsonText.svc_type
        /// </remarks>
        [JsonProperty(PropertyName = "svc_type", Order = 3)]
        [JsonConverter(typeof(StringEnumConverter))]
        public EventServiceTypePart Type { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        /// 
        /// <remarks>
        /// EventServiceJsonText.svc_name
        /// </remarks>
        [JsonProperty(PropertyName = "svc_name", Order = 2)]
        public string Name { get; set; }

        /// <summary>
        /// 服务源模块名称
        /// </summary>
        /// 
        /// <remarks>
        /// EventServiceJsonText.souc_mod_name
        /// </remarks>
        [JsonProperty(PropertyName = "souc_mod_name", Order = 5)]
        [JsonConverter(typeof(StringEnumConverter))]
        public FrameModulePart SourceModuleName { get; set; }

        /// <summary>
        /// 服务内容
        /// </summary>
        /// 
        /// <remarks>
        /// EventServiceJsonText.svc_cont
        /// </remarks>
        [JsonProperty(PropertyName = "svc_cont", Order = 8)]
        public IEventServiceContent EventServiceContent { get; set; }

        public EventServiceKind() : base()
        {

        }

        public EventServiceKind(string codeArg, string itemArg, string nameArg, FrameModulePart sourceModuleNameArg,
                                        IEventServiceContent eventServiceContentArg, string contentArg, string noteArg, string recordTimeArg, bool enabledFlagArg) :
                                            base(codeArg, contentArg, noteArg, recordTimeArg, enabledFlagArg)
        {

        }
    }
}
