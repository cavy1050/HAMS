using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HAMS.Frame.Kernel.Core
{
    public class ResponseServiceKind : EventServiceKind
    {
        /// <summary>
        /// 应答服务结果
        /// </summary>
        [JsonProperty(PropertyName = "ret_rst", Order = 9)]
        public bool ReturnResult { get; set; }

        /// <summary>
        /// 应答服务错误信息
        /// </summary>
        [JsonProperty(PropertyName = "ret_msg", Order = 10)]
        public string ReturnMessage { get; set; }
    }
}
