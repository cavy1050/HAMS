using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HAMS.Frame.Kernel.Core
{
    public class EventInitializationRequestContentKind : IEventServiceContent
    {
        [JsonProperty(PropertyName = "app_ctl_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ControlTypePart ApplicationControlType { get; set; }
    }
}
