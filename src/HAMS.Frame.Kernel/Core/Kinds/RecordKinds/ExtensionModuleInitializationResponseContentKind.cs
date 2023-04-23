using System.Collections.Generic;
using Newtonsoft.Json;

namespace HAMS.Frame.Kernel.Core
{
    public class ExtensionModuleInitializationResponseContentKind : IEventContent
    {
        [JsonProperty(PropertyName = "menus")]
        public IEnumerable<ExtensionModuleKind> ExtensionModules { get; set; }
    }
}
