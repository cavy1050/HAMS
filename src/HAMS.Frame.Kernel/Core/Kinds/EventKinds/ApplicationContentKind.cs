using Newtonsoft.Json;

namespace HAMS.Frame.Kernel.Core
{
    public class ApplicationContentKind : IEventContent
    {
        [JsonProperty(PropertyName = "app_ctl_type", Order = 1)]
        public string ApplicationControlType { get; set; }

        [JsonProperty(PropertyName = "app_act_flag", Order = 2)]
        public string ApplicationActiveFlag { get; set; }
    }
}