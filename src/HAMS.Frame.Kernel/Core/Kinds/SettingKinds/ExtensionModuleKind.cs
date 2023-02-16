using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HAMS.Frame.Kernel.Core
{
    public class ExtensionModuleKind : NestKind
    {
        [JsonProperty(PropertyName = "menu_code", Order = 1)]
        public override string Code { get; set; }

        [JsonProperty(PropertyName = "menu_name", Order = 2)]
        public override string Name { get; set; }

        [JsonProperty(PropertyName = "menu_super_code", Order = 3)]
        public override string SuperCode { get; set; }

        [JsonProperty(PropertyName = "menu_super_item", Order = 4)]
        public override string SuperItem { get; set; }

        [JsonProperty(PropertyName = "menu_mod_name", Order = 5)]
        public override string Item { get; set; }

        [JsonProperty(PropertyName = "menu_mod_ref", Order = 6)]
        public override string Content { get; set; }

        [JsonProperty(PropertyName = "menu_mod_type", Order = 7)]
        public override string Description { get; set; }

        [JsonProperty(PropertyName = "menu_rank", Order = 8)]
        public override int Rank { get; set; }

        [JsonIgnore]
        public override string Note { get; set; }

        [JsonIgnore]
        public override bool EnabledFlag { get; set; }

        [JsonIgnore]
        public override bool DefaultFlag { get; set; }
    }
}
