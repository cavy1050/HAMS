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

namespace HQMS.Extension.Control.Main.Models
{
    public class MainModel:BindableBase
    {
        string sqlSentence;
        List<SettingKind> menuHub;

        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeBaseController;

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
        }

        public void Loaded()
        {
            sqlSentence = "SELECT Code,Item,Name,Content,Description,Note,Rank,DefaultFlag,EnabledFlag FROM HQMS_DictionarySetting WHERE CategoryCode = '01GT3M1KGWN67V9ARD839D7ZSF' AND EnabledFlag = True";

            if (nativeBaseController.Query<SettingKind>(sqlSentence, out menuHub))
                Menus = new ObservableCollection<SettingKind>(menuHub);
        }
    }
}
