using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using MaterialDesignThemes.Wpf;
using MaterialDesignExtensions.Controls;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Services;
using HAMS.Frame.Kernel.Extensions;
using HQMS.Extension.Kernel.Core;

namespace HQMS.Extension.Control.Configuration.Models
{
    public class ConfigurationModel : BindableBase
    {
        string sqlSentence;

        ISnackbarMessageQueue messageQueue;
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeBaseController;
        ISetting setting;

        List<SettingKind> hospitalAreaSettingHub;

        ObservableCollection<SettingKind> hospitals;
        public ObservableCollection<SettingKind> Hospitals
        {
            get => hospitals;
            set => SetProperty(ref hospitals, value);
        }

        SettingKind currentHospital;
        public SettingKind CurrentHospital
        {
            get => currentHospital;
            set => SetProperty(ref currentHospital, value);
        }

        string upLoadFileCatalogue;
        public string UpLoadFileCatalogue
        {
            get => upLoadFileCatalogue;
            set => SetProperty(ref upLoadFileCatalogue, value);
        }

        ObservableCollection<DisplayModeKind> displayModes;
        public ObservableCollection<DisplayModeKind> DisplayModes
        {
            get => displayModes;
            set => SetProperty(ref displayModes, value);
        }

        DisplayModeKind currentDisplayMode;
        public DisplayModeKind CurrentDisplayMode
        {
            get => currentDisplayMode;
            set => SetProperty(ref currentDisplayMode, value);
        }

        string masterExportFileCatalogue;
        public string MasterExportFileCatalogue
        {
            get => masterExportFileCatalogue;
            set => SetProperty(ref masterExportFileCatalogue, value);
        }

        string detailExportFileCatalogue;
        public string DetailExportFileCatalogue
        {
            get => detailExportFileCatalogue;
            set => SetProperty(ref detailExportFileCatalogue, value);
        }

        public ConfigurationModel(IContainerProvider containerProviderArg)
        {
            messageQueue = containerProviderArg.Resolve<ISnackbarMessageQueue>();
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
            nativeBaseController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.Native);
            setting = containerProviderArg.Resolve<ISetting>();

            Hospitals = new ObservableCollection<SettingKind>();
            DisplayModes = new ObservableCollection<DisplayModeKind>();
        }

        public void Loaded()
        {
            LoadDictionaryData();
            LoadCustomData();
        }

        private void LoadDictionaryData()
        {
            sqlSentence = "SELECT Code,Item,Name,Content,Description,Note,Rank,DefaultFlag,EnabledFlag FROM System_HospitalAreaSetting WHERE EnabledFlag = True";

            if (!nativeBaseController.Query<SettingKind>(sqlSentence, out hospitalAreaSettingHub))
                messageQueue.Enqueue("获取医院基础字典数据错误!");
            else
                Hospitals.AddRange(hospitalAreaSettingHub);

            foreach (DisplayModePart displayMode in Enum.GetValues(typeof(DisplayModePart)))
            {
                DisplayModes.Add(new DisplayModeKind
                {
                    Item = displayMode.ToString(),
                    Name = EnumExtension.GetDescription(displayMode)
                });
            }
        }

        private void LoadCustomData()
        {
            CurrentHospital = Hospitals.FirstOrDefault(item => item.Item == setting.HospitalCode);
            UpLoadFileCatalogue = setting.UpLoadFileCatalogue;
            CurrentDisplayMode = DisplayModes.FirstOrDefault(item => item.Item == setting.DisplayMode.ToString());
        }

        public async void OpenUpLoadFileCatalogue()
        {
            OpenDirectoryDialogArguments openDirectoryDialogArguments = new OpenDirectoryDialogArguments
            {
                Width = 600,
                Height = 500
            };

            OpenDirectoryDialogResult result = await OpenDirectoryDialog.ShowDialogAsync("MainDialog", openDirectoryDialogArguments);
            if (result.Confirmed)
                UpLoadFileCatalogue = result.Directory.EndsWith("\\") == false ? result.Directory + "\\" : result.Directory;
        }

        public void Save()
        {
            sqlSentence = "UPDATE HQMS_DictionarySetting SET Content='" + CurrentHospital.Item + "' WHERE Code='01GVGA3FQNHBTC5HWAYHGCVT45'";
            if (!nativeBaseController.Execute(sqlSentence))
                messageQueue.Enqueue("保存医院代码自定义数据错误!");
            else
            {
                sqlSentence = "UPDATE HQMS_DictionarySetting SET Content='" + UpLoadFileCatalogue + "' WHERE Code='01GVGA3FQN1W8600ZFKR4K74MY'";
                if (!nativeBaseController.Execute(sqlSentence))
                    messageQueue.Enqueue("保存上传文件目录自定义数据错误!");
                else
                {
                    sqlSentence = "UPDATE HQMS_DictionarySetting SET Content='" + CurrentDisplayMode.Item + "' WHERE Code='01GVGA3FQNHVVFEM8KQ6FDCYFS'";
                    if (!nativeBaseController.Execute(sqlSentence))
                        messageQueue.Enqueue("保存显示模式自定义数据错误!");
                    else
                        messageQueue.Enqueue("保存成功!");
                }
            }
        }
    }
}
