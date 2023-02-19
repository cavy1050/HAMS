using Newtonsoft.Json;

namespace HAMS.Frame.Kernel.Core
{
    public class AccountVerificationRequestContentKind : IEventServiceContent
    {
        [JsonProperty(PropertyName = "acct_code", Order = 1)]
        public string Account { get; set; }

        [JsonProperty(PropertyName = "acct_pwd", Order = 2)]
        public string Password { get; set; }
    }
}
