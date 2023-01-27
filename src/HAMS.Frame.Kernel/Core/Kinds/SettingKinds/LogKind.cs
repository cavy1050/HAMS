using HAMS.Frame.Kernel.Services;

namespace HAMS.Frame.Kernel.Core
{
    public class LogKind : SettingKind
    {
        public ILogController LogController { get; set; }

        public LogKind() : base()
        {

        }

        public LogKind(string codeArg, string itemArg, string nameArg, string contentArg, string descriptionArg, string noteArg,
                                int rankArg, bool defaultFlag, bool enabledFlag, ILogController logControllerArg) :
                                    base(codeArg, itemArg, nameArg, contentArg, descriptionArg, noteArg, rankArg, defaultFlag, enabledFlag)
        {
            LogController = logControllerArg;
        }
    }
}
