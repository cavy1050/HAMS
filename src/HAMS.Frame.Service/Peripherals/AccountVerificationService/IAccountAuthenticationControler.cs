using System;
using HAMS.Frame.Kernel.Core;

namespace HAMS.Frame.Service.Peripherals
{
    public interface IAccountAuthenticationControler
    {
        bool Validate(AccountVerificationServiceRequestContentKind requestContentArg, out string errorMessageArg);
    }
}
