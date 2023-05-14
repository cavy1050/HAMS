using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Services;
using HQMS.Extension.Kernel.Core;

namespace HQMS.Extension.Kernel
{
    public class ExtensionKernelLauncher
    {
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeBaseController;
        ISetting setting;

        string sqlSentence;
        List<SettingKind> customSettingHub;

        public ExtensionKernelLauncher(IContainerProvider containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
            nativeBaseController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.Native);
            setting = containerProviderArg.Resolve<ISetting>();
        }

        public static void RegisterServices(IContainerRegistry containerRegistryArg)
        {
            containerRegistryArg.RegisterSingleton<ISetting, Setting>();
        }

        public void Initialize()
        {
            sqlSentence = "SELECT Code,Item,Name,Content,Description,Note,Rank,DefaultFlag,EnabledFlag FROM HQMS_DictionarySetting WHERE CategoryCode='01GVGA3FQM9M5H3ZRP14H3ZJSX' AND EnabledFlag = True";
            nativeBaseController.Query(sqlSentence, out customSettingHub);

            setting.HospitalCode = customSettingHub.FirstOrDefault(code => code.Code == "01GVGA3FQNHBTC5HWAYHGCVT45").Content;
            setting.UpLoadFileCatalogue = customSettingHub.FirstOrDefault(code => code.Code == "01GVGA3FQN1W8600ZFKR4K74MY").Content;
            setting.DisplayMode = (DisplayModePart)Enum.Parse(typeof(DisplayModePart), customSettingHub.FirstOrDefault(code => code.Code == "01GVGA3FQNHVVFEM8KQ6FDCYFS").Content);
        }
    }
}
