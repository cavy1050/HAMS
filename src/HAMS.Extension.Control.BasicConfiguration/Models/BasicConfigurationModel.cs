using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Events;
using Newtonsoft.Json.Linq;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Events;

namespace HAMS.Extension.Control.BasicConfiguration.Models
{
    public class BasicConfigurationModel : BindableBase
    {
        IEventAggregator eventAggregator;
        ISnackbarMessageQueue messageQueue;
        IEnvironmentMonitor environmentMonitor;
        IEventServiceController eventServiceController;

        JObject responseObj, responseContentObj;
        string eventJsonSentence;

        ThemeInitializationRequestContentKind themeInitializationRequestContent;

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
            set => SetProperty(ref nativeConnectString, value);
        }

        string baglDBConnectString;
        public string BAGLDBConnectString
        {
            get => baglDBConnectString;
            set => SetProperty(ref baglDBConnectString, value);
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
            eventAggregator = containerProviderArg.Resolve<IEventAggregator>();
            messageQueue = containerProviderArg.Resolve<ISnackbarMessageQueue>();
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
            eventServiceController = containerProviderArg.Resolve<IEventServiceController>();

            PrimaryColors = new ObservableCollection<PrimaryColorKind>();
            SecondaryColors = new ObservableCollection<SecondaryColorKind>();

            eventAggregator.GetEvent<ResponseServiceEvent>().Subscribe(OnThemeInitializationResponseService, ThreadOption.PublisherThread, false, x => x.Contains("ThemeInitializationService"));
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
                    Name = color.ToString(),
                    BackGroundColor = SwatchHelper.Lookup[(MaterialDesignColor)color]
                });
            }

            foreach (object color in Enum.GetValues(typeof(SecondaryColor)))
            {
                SecondaryColors.Add(new SecondaryColorKind
                {
                    Name = color.ToString(),
                    BackGroundColor = SwatchHelper.Lookup[(MaterialDesignColor)color]
                });
            }
        }

        private void RequestThemeInitializationService()
        {
            themeInitializationRequestContent = new ThemeInitializationRequestContentKind
            {
                InitializationType = InitializationTypePart.Custom
            };

            eventJsonSentence = eventServiceController.Request(EventServicePart.ThemeInitializationService, FrameModulePart.BasicConfigurationModule, FrameModulePart.ServiceModule, themeInitializationRequestContent);
            eventAggregator.GetEvent<RequestServiceEvent>().Publish(eventJsonSentence);
        }

        private void LoadThemeSetting()
        {
            LoadColorsSetting();
            RequestThemeInitializationService();
        }

        public void OnLoaded()
        {
            LoadPathSetting();
            LoadDataBaseSetting();
            LoadLogSetting();
            LoadThemeSetting();
        }

        private void OnThemeInitializationResponseService(string responseServiceTextArg)
        {
            responseObj = JObject.Parse(responseServiceTextArg);
            FrameModulePart targetModule = (FrameModulePart)Enum.Parse(typeof(FrameModulePart), responseObj.Value<string>("tagt_mod_name"));

            if (targetModule == FrameModulePart.BasicConfigurationModule)
            {
                responseContentObj= responseObj.Value<JObject>("svc_cont");
                BaseTheme baseTheme= (BaseTheme)Enum.Parse(typeof(BaseTheme), responseContentObj.Value<string>("thm_type"));
                PrimaryColor primaryColor = (PrimaryColor)Enum.Parse(typeof(PrimaryColor), responseContentObj.Value<string>("thm_pry_col"));
                SecondaryColor secondaryColor = (SecondaryColor)Enum.Parse(typeof(SecondaryColor), responseContentObj.Value<string>("thm_sec_col"));

                ColorLightFlag = baseTheme == BaseTheme.Light ? false : true;
                CurrentPrimaryColor = PrimaryColors.FirstOrDefault(primary => primary.Name == primaryColor.ToString());
                CurrentSecondaryColor = SecondaryColors.FirstOrDefault(secondary => secondary.Name == secondaryColor.ToString());
            }
        }

        private static void ModifyTheme(Action<ITheme> modificationAction)
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
            modificationAction?.Invoke(theme);

            paletteHelper.SetTheme(theme);
        }

        public void OnOpenLogFileCatalogue()
        {
            
        }

        public void NativeDBConnection()
        {

        }

        public void OnBAGLDBConnectString()
        {

        }

        public void BAGLDBConnection()
        {

        }

        public void OnDefault()
        {

        }

        public void OnApply()
        {
            ModifyTheme(theme => theme.SetBaseTheme(ColorLightFlag ? Theme.Dark : Theme.Light));
            ModifyTheme(theme => theme.SetPrimaryColor(CurrentPrimaryColor.BackGroundColor));
            ModifyTheme(theme => theme.SetSecondaryColor(CurrentSecondaryColor.BackGroundColor));
        }

        public void OnSave()
        {

        }
    }
}
