using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HAMS.Frame.Kernel.Core
{
    public class AccountVerificationServiceRequestContentKind : IServiceContent
    {
        [JsonProperty(PropertyName = "acct_code")]
        public string Account { get; set; }

        [JsonProperty(PropertyName = "acct_pwd")]
        public string Password { get; set; }
    }
}
