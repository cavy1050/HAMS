using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Services;
using HQMS.Extension.Control.Main;

namespace HQMS.Extension.Control.Main.Models
{
    public class MainModel : BindableBase
    {
        string sqlSentence;
        List<SettingKind> menuHub;
        List<SettingKind> customHub;

        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeBaseController;
        IConfigurator configurator;

        ObservableCollection<SettingKind> menus;
        public ObservableCollection<SettingKind> Menus
        {
            get => menus;
            set => SetProperty(ref menus, value);
        }

        string currentItem;
        public string CurrentItem
        {
            get => currentItem;
            set => SetProperty(ref currentItem, value);
        }

        public MainModel(IContainerProvider containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
            nativeBaseController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.Native);
            configurator = containerProviderArg.Resolve<IConfigurator>();
        }

        public void Loaded()
        {
            LoadMenuData();
            LoadConfiguratorData();
        }

        private void LoadMenuData()
        {
            sqlSentence = "SELECT Code,Item,Name,Content,Description,Note,Rank,DefaultFlag,EnabledFlag FROM HQMS_DictionarySetting WHERE CategoryCode = '01GT3M1KGWN67V9ARD839D7ZSF' AND EnabledFlag = True";

            if (nativeBaseController.Query<SettingKind>(sqlSentence, out menuHub))
                Menus = new ObservableCollection<SettingKind>(menuHub);
        }

        private void LoadConfiguratorData()
        {
            sqlSentence = "SELECT Code,Item,Name,Content,Description,Note,Rank,DefaultFlag,EnabledFlag FROM HQMS_DictionarySetting WHERE CategoryCode = '01GVGA3FQM9M5H3ZRP14H3ZJSX' AND EnabledFlag = True";

            if (nativeBaseController.Query<SettingKind>(sqlSentence, out customHub))
            {
                configurator.HospitalCode = customHub.FirstOrDefault(setting => setting.Code == "01GVGA3FQNHBTC5HWAYHGCVT45").Content;
                configurator.ExportFileCatalogue = customHub.FirstOrDefault(setting => setting.Code == "01GVGA3FQNBZ9YZ9R2PVEBXZKC").Content;
                configurator.UpLoadFileCatalogue = customHub.FirstOrDefault(setting => setting.Code == "01GVGA3FQN1W8600ZFKR4K74MY").Content;
                configurator.MasterExportFileCatalogue = customHub.FirstOrDefault(setting => setting.Code == "01GVGA3FQNHVVFEM8KQ6FDCYFS").Content;
                configurator.DetailExportFileCatalogue = customHub.FirstOrDefault(setting => setting.Code == "01GVGAG8VD3GYZMXG1MQZ2YW1T").Content;
            }
        }
    }
}
