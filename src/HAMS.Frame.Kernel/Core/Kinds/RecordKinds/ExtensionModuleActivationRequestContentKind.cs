using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HAMS.Frame.Kernel.Core
{
    public class ExtensionModuleActivationRequestContentKind : IEventServiceContent
    {
        [JsonProperty(PropertyName = "menu_code", Order = 1)]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "menu_name", Order = 2)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "menu_mod_name", Order = 3)]
        public string ModuleName { get; set; }

        [JsonProperty(PropertyName = "menu_mod_ref", Order = 4)]
        public string ModuleRef { get; set; }

        [JsonProperty(PropertyName = "menu_mod_type", Order = 5)]
        public string ModuleType { get; set; }
    }
}
