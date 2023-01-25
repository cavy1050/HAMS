using HAMS.Frame.Kernel.Services;

namespace HAMS.Frame.Kernel.Core
{
    public class DataBaseKind : SettingKind
    {
        public IDataBaseController DataBaseController { get; set; }

        public DataBaseKind() : base()
        {

        }

        public DataBaseKind(string codeArg, string itemArg, string nameArg, string contentArg, string descriptionArg, string noteArg,
                                int rankArg, bool defaultFlag, bool enabledFlag, IDataBaseController dataBaseControllerArg) :
                                    base(codeArg, itemArg, nameArg, contentArg, descriptionArg, noteArg, rankArg, defaultFlag, enabledFlag)
        {
            DataBaseController = dataBaseControllerArg;
        }
    }
}
