using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;

namespace HAMS.Frame.Kernel.Core
{
    public abstract class ThemeContentKind : IEventServiceContent
    {
        [JsonProperty(PropertyName = "thm_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual BaseTheme BaseTheme { get; set; }

        [JsonProperty(PropertyName = "thm_pry_col")]
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual PrimaryColor PrimaryColor { get; set; }

        [JsonProperty(PropertyName = "thm_sec_col")]
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual SecondaryColor SecondaryColor { get; set; }
    }
}
