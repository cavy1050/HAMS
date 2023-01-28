using System;
using System.Collections.Generic;
using HAMS.Frame.Kernel.Core;

namespace HAMS.Frame.Kernel.Services
{
    public interface IEventServiceController
    {
        string Request(EventServicePart eventServiceArg, FrameModulePart sourceModuleArg, FrameModulePart targetModuleArg, IServiceContent serviceContentArg);

        string Response(EventServicePart eventServiceArg, FrameModulePart sourceModuleArg, IEnumerable<FrameModulePart> targetModuleArg, bool returnCodeArg, string returnMessageArgs, IServiceContent serviceContentArg);
    }
}
