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

namespace HQMS.Extension.Control.Configuration.Models
{
    public class ConfigurationModel : BindableBase
    {
        string sqlSentence;

        ISnackbarMessageQueue messageQueue;
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeBaseController;
        IDataBaseController BAGLDBController;

        List<TyydmkKind> TyydmkHub;
        List<SettingKind> customHub;

        ObservableCollection<TyydmkKind> hospitals;
        public ObservableCollection<TyydmkKind> Hospitals
        {
            get => hospitals;
            set => SetProperty(ref hospitals, value);
        }

        string currentHospitalCode;
        public string CurrentHospitalCode
        {
            get => currentHospitalCode;
            set => SetProperty(ref currentHospitalCode, value);
        }

        int currentHospitalIndex;
        public int CurrentHospitalIndex
        {
            get => currentHospitalIndex;
            set => SetProperty(ref currentHospitalIndex, value);
        }

        string exportFileCatalogue;
        public string ExportFileCatalogue
        {
            get => exportFileCatalogue;
            set => SetProperty(ref exportFileCatalogue, value);
        }

        string upLoadFileCatalogue;
        public string UpLoadFileCatalogue
        {
            get => upLoadFileCatalogue;
            set => SetProperty(ref upLoadFileCatalogue, value);
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
            BAGLDBController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.BAGLDB);

            Hospitals = new ObservableCollection<TyydmkKind>();
        }

        public void Loaded()
        {
            LoadHospitalData();
            LoadCustomData();
        }

        private void LoadHospitalData()
        {
            string hospitalCode;

            sqlSentence = "SELECT fyydm,fyymc FROM dbo.Tyydmk";

            if (!BAGLDBController.Query<TyydmkKind>(sqlSentence, out TyydmkHub))
                messageQueue.Enqueue("获取医院基础字典数据错误!");
            else
                Hospitals.AddRange(TyydmkHub);

            sqlSentence = "SELECT Code,Item,Name,Content,Description,Note,Rank,DefaultFlag,EnabledFlag FROM HQMS_DictionarySetting WHERE CategoryCode = '01GVGA3FQM9M5H3ZRP14H3ZJSX' AND EnabledFlag = True";
            if (!nativeBaseController.Query<SettingKind>(sqlSentence, out customHub))
                messageQueue.Enqueue("获取医院自定义数据错误!");
            else
            {
                hospitalCode = customHub.FirstOrDefault(setting => setting.Code == "01GVGA3FQNHBTC5HWAYHGCVT45").Content;

                switch (hospitalCode)
                {
                    case "01": CurrentHospitalIndex = 0; break;
                    case "02": CurrentHospitalIndex = 1; break;
                    case "03": CurrentHospitalIndex = 2; break;
                }
            }
        }

        private void LoadCustomData()
        {
            ExportFileCatalogue = customHub.FirstOrDefault(setting => setting.Code == "01GVGA3FQNBZ9YZ9R2PVEBXZKC").Content;
            UpLoadFileCatalogue= customHub.FirstOrDefault(setting => setting.Code == "01GVGA3FQN1W8600ZFKR4K74MY").Content;
            MasterExportFileCatalogue = customHub.FirstOrDefault(setting => setting.Code == "01GVGA3FQNHVVFEM8KQ6FDCYFS").Content;
            DetailExportFileCatalogue = customHub.FirstOrDefault(setting => setting.Code == "01GVGAG8VD3GYZMXG1MQZ2YW1T").Content;
        }

        public async void OpenExportFileCatalogue()
        {
            OpenDirectoryDialogArguments openDirectoryDialogArguments = new OpenDirectoryDialogArguments
            {
                Width = 600,
                Height = 500
            };

            OpenDirectoryDialogResult result = await OpenDirectoryDialog.ShowDialogAsync("MainDialog", openDirectoryDialogArguments);
            if (result.Confirmed)
                ExportFileCatalogue = result.Directory+"\\";
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
                UpLoadFileCatalogue = result.Directory + "\\";
        }

        public async void OpenMasterExportFileCatalogue()
        {
            OpenDirectoryDialogArguments openDirectoryDialogArguments = new OpenDirectoryDialogArguments
            {
                Width = 600,
                Height = 500
            };

            OpenDirectoryDialogResult result = await OpenDirectoryDialog.ShowDialogAsync("MainDialog", openDirectoryDialogArguments);
            if (result.Confirmed)
                MasterExportFileCatalogue = result.Directory + "\\";
        }

        public async void OpenDetailExportFileCatalogue()
        {
            OpenDirectoryDialogArguments openDirectoryDialogArguments = new OpenDirectoryDialogArguments
            {
                Width = 600,
                Height = 500
            };

            OpenDirectoryDialogResult result = await OpenDirectoryDialog.ShowDialogAsync("MainDialog", openDirectoryDialogArguments);
            if (result.Confirmed)
                DetailExportFileCatalogue = result.Directory + "\\";
        }

        public void Save()
        {
            sqlSentence = "UPDATE HQMS_DictionarySetting SET Content='" + CurrentHospitalCode + "' WHERE Code='01GVGA3FQNHBTC5HWAYHGCVT45'";
            if (!nativeBaseController.Execute(sqlSentence))
                messageQueue.Enqueue("保存医院代码自定义数据错误!");
            else
            {
                sqlSentence = "UPDATE HQMS_DictionarySetting SET Content='" + ExportFileCatalogue + "' WHERE Code='01GVGA3FQNBZ9YZ9R2PVEBXZKC'";
                if (!nativeBaseController.Execute(sqlSentence))
                    messageQueue.Enqueue("保存导出文件目录自定义数据错误!");
                else
                {
                    sqlSentence = "UPDATE HQMS_DictionarySetting SET Content='" + UpLoadFileCatalogue + "' WHERE Code='01GVGA3FQN1W8600ZFKR4K74MY'";
                    if (!nativeBaseController.Execute(sqlSentence))
                        messageQueue.Enqueue("保存上传文件目录自定义数据错误!");
                    else
                    {
                        sqlSentence = "UPDATE HQMS_DictionarySetting SET Content='" + MasterExportFileCatalogue + "' WHERE Code='01GVGA3FQNHVVFEM8KQ6FDCYFS'";
                        if (!nativeBaseController.Execute(sqlSentence))
                            messageQueue.Enqueue("保存汇总文件导出目录自定义数据错误!");
                        else
                        {
                            sqlSentence = "UPDATE HQMS_DictionarySetting SET Content='" + DetailExportFileCatalogue + "' WHERE Code='01GVGAG8VD3GYZMXG1MQZ2YW1T'";
                            if (!nativeBaseController.Execute(sqlSentence))
                                messageQueue.Enqueue("保存明细文件导出目录自定义数据错误!");
                            else
                                messageQueue.Enqueue("保存成功!");
                        }
                    }
                }
            }
        }
    }
}
