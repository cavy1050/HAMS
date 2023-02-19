using Newtonsoft.Json;

namespace HAMS.Frame.Kernel.Core
{
    public class AccountVerificationResponseContentKind : AccountVerificationRequestContentKind
    {
        [JsonProperty(PropertyName = "acct_name", Order = 3)]
        public string Name { get; set; }
    }
}
