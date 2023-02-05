using System;
using System.Collections.Generic;
using HAMS.Frame.Kernel.Core;

namespace HAMS.Frame.Kernel.Events
{
    public interface IEventServiceController
    {
        string Request(EventServicePart eventServiceArg, FrameModulePart sourceModuleArg, FrameModulePart targetModuleArg, IEventServiceContent eventServiceContent);

        string Response(EventServicePart eventServiceArg, FrameModulePart sourceModuleArg, IEnumerable<FrameModulePart> targetModuleArg, bool returnCodeArg, string returnMessageArgs, IEventServiceContent eventServiceContent);
    }
}
