using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HAMS.Frame.Kernel.Core
{
    public class ApplicationAlterationContentKind : IEventServiceContent
    {
        [JsonProperty(PropertyName = "app_ctl_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ControlTypePart ApplicationControlType { get; set; }

        [JsonProperty(PropertyName = "app_act_flag")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ActiveFlagPart ApplicationActiveFlag { get; set; }
    }
}