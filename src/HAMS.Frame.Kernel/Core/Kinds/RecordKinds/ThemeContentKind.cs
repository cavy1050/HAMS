using Newtonsoft.Json;

namespace HAMS.Frame.Kernel.Core
{
    public class ThemeContentKind : IEventContent
    {
        [JsonProperty(PropertyName = "thm_type", Order = 1)]
        public string BaseTheme { get; set; }

        [JsonProperty(PropertyName = "thm_pry_col", Order = 2)]
        public string PrimaryColor { get; set; }

        [JsonProperty(PropertyName = "thm_sec_col", Order = 3)]
        public string SecondaryColor { get; set; }
    }
}
