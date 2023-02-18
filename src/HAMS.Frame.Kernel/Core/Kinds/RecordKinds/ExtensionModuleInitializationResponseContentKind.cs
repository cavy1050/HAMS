﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HAMS.Frame.Kernel.Core
{
    public class ExtensionModuleInitializationResponseContentKind : IEventServiceContent
    {
        [JsonProperty(PropertyName = "menus")]
        public IEnumerable<ExtensionModuleKind> ExtensionModules { get; set; }
    }
}