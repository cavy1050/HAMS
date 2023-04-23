using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Events;
using Prism.Services.Dialogs;
using Newtonsoft.Json.Linq;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using MaterialDesignExtensions.Controls;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Services;

namespace HAMS.Extension.Control.BasicConfiguration.Models
{
    public class BasicConfigurationModel : BindableBase
    {
        ISnackbarMessageQueue messageQueue;
        IEnvironmentMonitor environmentMonitor;
        IDialogService dialogService;
        IDataBaseController dataBaseController;

        bool isConnectionStringChanged;

        PathManager pathManager;
        DataBaseManager dataBaseManager;
        LogManager logManager;

        string applictionCatalogue;
        public string ApplictionCatalogue
        {
            get => applictionCatalogue;
            set => SetProperty(ref applictionCatalogue, value);
        }

        string nativeDataBaseFilePath;
        public string NativeDataBaseFilePath
        {
            get => nativeDataBaseFilePath;
            set => SetProperty(ref nativeDataBaseFilePath, value);
        }

        string logFileCatalogue;
        public string LogFileCatalogue
        {
            get => logFileCatalogue;
            set => SetProperty(ref logFileCatalogue, value);
        }

        string nativeConnectString;
        public string NativeConnectString
        {
            get => nativeConnectString;
            set
            {
                isConnectionStringChanged = true;
                SetProperty(ref nativeConnectString, value);
            }
        }

        string baglDBConnectString;
        public string BAGLDBConnectString
        {
            get => baglDBConnectString;
            set
            {
                isConnectionStringChanged = true;
                SetProperty(ref baglDBConnectString, value);
            }
        }

        bool globalLogEnabledFlag;
        public bool GlobalLogEnabledFlag
        {
            get => globalLogEnabledFlag;
            set => SetProperty(ref globalLogEnabledFlag, value);
        }

        string globalLogCurrentLevelName;
        public string GlobalLogCurrentLevelName
        {
            get => globalLogCurrentLevelName;
            set => SetProperty(ref globalLogCurrentLevelName, value);
        }

        LogLevelPart globalLogCurrentLevel;
        public LogLevelPart GlobalLogCurrentLevel
        {
            get => globalLogCurrentLevel;
            set
            {
                globalLogCurrentLevel = (LogLevelPart)Enum.Parse(typeof(LogLevelPart), GlobalLogCurrentLevelName);
            }
        }

        bool colorLightFlag;
        public bool ColorLightFlag
        {
            get => colorLightFlag;
            set => SetProperty(ref colorLightFlag, value);
        }

        ObservableCollection<PrimaryColorKind> primaryColors;
        public ObservableCollection<PrimaryColorKind> PrimaryColors
        {
            get => primaryColors;
            set => SetProperty(ref primaryColors, value);
        }

        PrimaryColorKind currentPrimaryColor;
        public PrimaryColorKind CurrentPrimaryColor
        {
            get => currentPrimaryColor;
            set => SetProperty(ref currentPrimaryColor, value);
        }

        ObservableCollection<SecondaryColorKind> secondaryColors;
        public ObservableCollection<SecondaryColorKind> SecondaryColors
        {
            get => secondaryColors;
            set => SetProperty(ref secondaryColors, value);
        }

        SecondaryColorKind currentSecondaryColor;
        public SecondaryColorKind CurrentSecondaryColor
        {
            get => currentSecondaryColor;
            set => SetProperty(ref currentSecondaryColor, value);
        }

        public BasicConfigurationModel(IContainerProvider containerProviderArg)
        {
            messageQueue = containerProviderArg.Resolve<ISnackbarMessageQueue>();
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
            dialogService = containerProviderArg.Resolve<IDialogService>();

            PrimaryColors = new ObservableCollection<PrimaryColorKind>();
            SecondaryColors = new ObservableCollection<SecondaryColorKind>();

            pathManager = (PathManager)containerProviderArg.Resolve<IManager<PathPart>>();
            dataBaseManager = (DataBaseManager)containerProviderArg.Resolve<IManager<DataBasePart>>();
            logManager = (LogManager)containerProviderArg.Resolve<IManager<LogPart>>();
        }

        private void LoadPathSetting()
        {
            ApplictionCatalogue = environmentMonitor.PathSetting.GetContent(PathPart.ApplictionCatalogue);
            NativeDataBaseFilePath = environmentMonitor.PathSetting.GetContent(PathPart.NativeDataBaseFilePath);
            LogFileCatalogue = environmentMonitor.PathSetting.GetContent(PathPart.LogFileCatalogue);
        }

        private void LoadDataBaseSetting()
        {
            NativeConnectString = environmentMonitor.DataBaseSetting[DataBasePart.Native].Content;
            BAGLDBConnectString = environmentMonitor.DataBaseSetting[DataBasePart.BAGLDB].Content;
        }

        private void LoadLogSetting()
        {
            GlobalLogEnabledFlag = environmentMonitor.LogSetting[LogPart.Global].EnabledFlag;
            GlobalLogCurrentLevelName = environmentMonitor.LogSetting[LogPart.Global].Content;
        }

        private void LoadColorsSetting()
        {
            foreach (object color in Enum.GetValues(typeof(PrimaryColor)))
            {
                PrimaryColors.Add(new PrimaryColorKind
                {
                    Code = Convert.ToInt32(color).ToString().PadLeft(3, '0'),
                    Name = color.ToString(),
                    BackGroundColor = SwatchHelper.Lookup[(MaterialDesignColor)color]
                });
            }

            foreach (object color in Enum.GetValues(typeof(SecondaryColor)))
            {
                SecondaryColors.Add(new SecondaryColorKind
                {
                    Code = Convert.ToInt32(color).ToString().PadLeft(3, '0'),
                    Name = color.ToString(),
                    BackGroundColor = SwatchHelper.Lookup[(MaterialDesignColor)color]
                });
            }
        }

        private void LoadThemeSetting()
        {
            LoadColorsSetting();
        }

        public void OnLoaded()
        {
            LoadPathSetting();
            LoadDataBaseSetting();
            LoadLogSetting();
            LoadThemeSetting();

            isConnectionStringChanged = false;
        }

        public async void OnOpenLogFileCatalogue()
        {
            OpenDirectoryDialogArguments openDirectoryDialogArguments = new OpenDirectoryDialogArguments
            {
                Width = 600,
                Height = 500
            };

            OpenDirectoryDialogResult result = await OpenDirectoryDialog.ShowDialogAsync("MainDialog", openDirectoryDialogArguments);
            if (result.Confirmed)
                LogFileCatalogue = result.Directory.EndsWith("\\") == false ? result.Directory + "\\" : result.Directory;
        }

        public void Connection(string dataBaseIdentifierArg)
        {
            if (isConnectionStringChanged)
                messageQueue.Enqueue("数据库设置已改变,请先应用再测试连接!");
            else
            {
                DataBasePart dataBase = (DataBasePart)Enum.Parse(typeof(DataBasePart), dataBaseIdentifierArg);
                dataBaseController = environmentMonitor.DataBaseSetting.GetContent(dataBase);
                if (dataBaseController.Connection())
                    messageQueue.Enqueue("连接成功!");
            }
        }

        public void ConnectStringSetting(string dataBaseIdentifierArg)
        {
            DialogParameters connectStringParameter = new DialogParameters();
            connectStringParameter.Add("DataBaseIdentifier", dataBaseIdentifierArg);
            connectStringParameter.Add("ConnectionString", BAGLDBConnectString);

            dialogService.ShowDialog("Connection", connectStringParameter,
                ret =>
                {
                    if (ret.Result == ButtonResult.OK)
                        BAGLDBConnectString = ret.Parameters.GetValue<string>("ConnectionString");
                });
        }

        public void OnDefault()
        {
            pathManager.DeInit(PathPart.LogFileCatalogue);
            logManager.DeInit(LogPart.Global);

            LogFileCatalogue = pathManager.LogFileCatalogue;
            GlobalLogEnabledFlag = logManager.GlobalLogEnabledFlag;
            GlobalLogCurrentLevelName = logManager.GlobalLogLevel.ToString();

            messageQueue.Enqueue("已恢复默认设置!");
        }

        public void OnApply()
        {
            isConnectionStringChanged = false;

            pathManager.LogFileCatalogue = LogFileCatalogue;
            dataBaseManager.BAGLDBConnectString = BAGLDBConnectString;
            logManager.GlobalLogEnabledFlag = GlobalLogEnabledFlag;
            logManager.GlobalLogLevel = GlobalLogCurrentLevel;

            pathManager.Load(PathPart.All);
            dataBaseManager.Load(DataBasePart.All);
            logManager.Load(LogPart.All);

            messageQueue.Enqueue("已应用设置!");
        }

        public void OnSave()
        {
            pathManager.Save(PathPart.All);
            dataBaseManager.Save(DataBasePart.All);
            logManager.Save(LogPart.All);

            messageQueue.Enqueue("已保存设置!");
        }
    }
}
