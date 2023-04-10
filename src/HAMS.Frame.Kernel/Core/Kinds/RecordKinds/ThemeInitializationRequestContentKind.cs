using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HAMS.Frame.Kernel.Core
{
    public class ThemeInitializationRequestContentKind : IEventServiceContent
    {
        [JsonProperty(PropertyName = "init_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public InitializationTypePart InitializationType { get; set; }
    }
}
