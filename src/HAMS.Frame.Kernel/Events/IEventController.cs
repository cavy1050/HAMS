using HAMS.Frame.Kernel.Core;

namespace HAMS.Frame.Kernel.Events
{
    public interface IEventController
    {
        string Request(EventPart eventArg, EventBehaviourPart eventBehaviourPartArg, FrameModulePart sourceModuleArg, FrameModulePart targetModuleArg, IEventContent eventContentArg);

        string Response(EventPart eventArg, EventBehaviourPart eventBehaviourPartArg, FrameModulePart sourceModuleArg, FrameModulePart targetModuleArg, IEventContent eventContentArg, bool returnCodeArg, string returnMessageArgs);
    }
}
