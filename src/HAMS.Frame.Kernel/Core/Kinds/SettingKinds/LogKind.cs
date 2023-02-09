using HAMS.Frame.Kernel.Services;

namespace HAMS.Frame.Kernel.Core
{
    public class LogKind : SettingKind
    {
        public ILogController LogController { get; set; }
    }
}
