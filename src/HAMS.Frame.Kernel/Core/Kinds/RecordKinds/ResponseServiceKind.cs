using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HAMS.Frame.Kernel.Core
{
    public class ResponseServiceKind : EventServiceKind
    {
        /// <summary>
        /// 应答服务目标模块名称
        /// </summary>
        /// 
        /// <remarks>
        /// eventJsonSentence.tagt_mod_name
        /// </remarks>
        [JsonProperty(PropertyName = "tagt_mod_name", ItemConverterType = typeof(StringEnumConverter), Order = 6)]
        public IEnumerable<FrameModulePart> TargetModuleName { get; set; }

        /// <summary>
        /// 应答服务错误代码
        /// </summary>
        /// 
        /// <remarks>
        /// eventJsonSentence.ret_code
        /// </remarks>
        [JsonProperty(PropertyName = "ret_code", Order = 9)]
        public string ReturnCode { get; set; }

        /// <summary>
        /// 应答服务错误信息
        /// </summary>
        /// 
        /// <remarks>
        /// eventJsonSentence.ret_msg
        /// </remarks>
        [JsonProperty(PropertyName = "ret_msg", Order = 10)]
        public string ReturnMessage { get; set; }
    }
}
