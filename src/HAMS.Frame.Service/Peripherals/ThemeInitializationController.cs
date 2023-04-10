using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Newtonsoft.Json.Linq;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Events;
using HAMS.Frame.Kernel.Services;

namespace HAMS.Frame.Service.Peripherals
{
    public class ThemeInitializationController : IServiceController
    {
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeBaseController;
        IEventServiceController eventServiceController;

        List<ThemeKind> customThemeSettingHub;

        JObject requestObj, requestContentObj;
        string sqlSentence, eventJsonSentence;

        public BaseTheme BaseTheme { get; set; }
        public PrimaryColor PrimaryColor { get; set; }
        public SecondaryColor SecondaryColor { get; set; }

        ThemeInitializationResponseContentKind themeInitializationResponseContent;

        public ThemeInitializationController(IContainerProvider containerProviderArgs)
        {
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
            nativeBaseController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.Native);
            eventServiceController = containerProviderArgs.Resolve<IEventServiceController>();
        }

        public string Response(string requestServiceTextArg)
        {
            requestObj = JObject.Parse(requestServiceTextArg);
            requestContentObj = requestObj.Value<JObject>("svc_cont");

            InitializationTypePart initializationType = (InitializationTypePart)Enum.Parse(typeof(InitializationTypePart), requestContentObj.Value<string>("init_type"));
            if (initializationType == InitializationTypePart.Default)
            {
                this.BaseTheme = BaseTheme.Light;
                this.PrimaryColor = PrimaryColor.Blue;
                this.SecondaryColor = SecondaryColor.Red;

                themeInitializationResponseContent = new ThemeInitializationResponseContentKind
                {
                    BaseTheme = this.BaseTheme,
                    PrimaryColor = this.PrimaryColor,
                    SecondaryColor = this.SecondaryColor
                };
            }
            else
            {
                //如果自定义设置不为空,则覆盖默认值,否则保持默认值
                sqlSentence = "SELECT Code,Item,Name,Content,BaseTheme,PrimaryColor,SecondaryColor,Description,Note,Rank,DefaultFlag,EnabledFlag FROM System_ThemeSetting WHERE DefaultFlag=False";
                nativeBaseController.Query<ThemeKind>(sqlSentence, out customThemeSettingHub);

                this.BaseTheme = customThemeSettingHub.FirstOrDefault(code => code.Code == "01GX8353SEH9NXXND2KZWMYB79").BaseTheme;
                this.PrimaryColor = customThemeSettingHub.FirstOrDefault(code => code.Code == "01GX8353SEH9NXXND2KZWMYB79").PrimaryColor;
                this.SecondaryColor = customThemeSettingHub.FirstOrDefault(code => code.Code == "01GX8353SEH9NXXND2KZWMYB79").SecondaryColor;

                themeInitializationResponseContent = new ThemeInitializationResponseContentKind
                {
                    BaseTheme = this.BaseTheme,
                    PrimaryColor = this.PrimaryColor,
                    SecondaryColor = this.SecondaryColor
                };
            }

            eventJsonSentence = eventServiceController.Response(EventServicePart.ThemeInitializationService, FrameModulePart.ServiceModule,FrameModulePart.BasicConfigurationModule, true, string.Empty, themeInitializationResponseContent);

            return eventJsonSentence;
        }
    }
}
