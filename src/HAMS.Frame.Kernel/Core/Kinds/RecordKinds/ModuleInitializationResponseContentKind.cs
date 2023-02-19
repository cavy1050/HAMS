using System.Collections.Generic;
using Newtonsoft.Json;

namespace HAMS.Frame.Kernel.Core
{
    public class ModuleInitializationResponseContentKind : IEventServiceContent
    {
        [JsonProperty(PropertyName = "menus")]
        public IEnumerable<ExtensionModuleKind> ExtensionModules { get; set; }
    }
}
