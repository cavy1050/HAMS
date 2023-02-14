using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HAMS.Frame.Kernel.Core
{
    public class ExtensionModuleResponseContentKind : IEventServiceContent
    {
        [JsonProperty(PropertyName = "menus")]
        public IEnumerable<ExtensionModuleKind> EtensionModules { get; set; }
    }
}
