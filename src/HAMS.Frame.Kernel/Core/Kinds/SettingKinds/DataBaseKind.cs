using HAMS.Frame.Kernel.Services;

namespace HAMS.Frame.Kernel.Core
{
    public class DataBaseKind : SettingKind
    {
        public IDataBaseController DataBaseController { get; set; }
    }
}
