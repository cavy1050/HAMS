using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Prism.Ioc;
using Newtonsoft.Json.Linq;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Events;
using HAMS.Frame.Kernel.Services;

namespace HAMS.Frame.Service.Peripherals
{
    public class ThemeController : IEventResponseController
    {
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeBaseController;
        IEventController eventController;

        List<ThemeKind> customThemeSettingHub;

        JObject requestObj, requestContentObj;
        string sqlSentence, eventJsonSentence;

        IEventContent themeResponseContent;

        public BaseTheme BaseTheme { get; set; }
        public PrimaryColor PrimaryColor { get; set; }
        public SecondaryColor SecondaryColor { get; set; }
        public FrameModulePart TargetModule { get; set; }

        public ThemeController(IContainerProvider containerProviderArgs)
        {
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
            nativeBaseController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.Native);
            eventController = containerProviderArgs.Resolve<IEventController>();
        }

        public string Response(string requestServiceTextArg)
        {
            requestObj = JObject.Parse(requestServiceTextArg);
            requestContentObj = requestObj.Value<JObject>("svc_cont");
            FrameModulePart sourceModule = (FrameModulePart)Enum.Parse(typeof(FrameModulePart), requestObj.Value<string>("souc_mdl"));
            EventBehaviourPart eventBehaviour = (EventBehaviourPart)Enum.Parse(typeof(EventBehaviourPart), requestObj.Value<string>("svc_bhvr_type"));

            //如果请求消息由服务模块发起,则默认为程序初始化服务,应答消息为广播消息
            if (sourceModule == FrameModulePart.ServiceModule)
                TargetModule = FrameModulePart.All;
            else
                TargetModule = sourceModule;

            switch (eventBehaviour)
            {
                case EventBehaviourPart.DefaultInitialization:
                    {
                        BaseTheme = BaseTheme.Dark;
                        PrimaryColor = PrimaryColor.Yellow;
                        SecondaryColor = SecondaryColor.Red;

                        themeResponseContent = new ThemeContentKind
                        {
                            BaseTheme = Convert.ToInt32(this.BaseTheme).ToString(),
                            PrimaryColor = Convert.ToInt32(this.PrimaryColor).ToString(),
                            SecondaryColor = Convert.ToInt32(this.SecondaryColor).ToString()
                        };

                        break;
                    }
                case EventBehaviourPart.Initialization:
                    {
                        //如果自定义设置不为空,则覆盖默认值,否则保持默认值
                        sqlSentence = "SELECT Code,Item,Name,Content,BaseTheme,PrimaryColor,SecondaryColor,Description,Note,Rank,DefaultFlag,EnabledFlag FROM System_ThemeSetting WHERE DefaultFlag=False";
                        nativeBaseController.Query<ThemeKind>(sqlSentence, out customThemeSettingHub);

                        BaseTheme = customThemeSettingHub.FirstOrDefault(code => code.Code == "01GX8353SEH9NXXND2KZWMYB79").BaseTheme;
                        PrimaryColor = customThemeSettingHub.FirstOrDefault(code => code.Code == "01GX8353SEH9NXXND2KZWMYB79").PrimaryColor;
                        SecondaryColor = customThemeSettingHub.FirstOrDefault(code => code.Code == "01GX8353SEH9NXXND2KZWMYB79").SecondaryColor;

                        themeResponseContent = new ThemeContentKind
                        {
                            BaseTheme = Convert.ToInt32(this.BaseTheme).ToString(),
                            PrimaryColor = Convert.ToInt32(this.PrimaryColor).ToString(),
                            SecondaryColor = Convert.ToInt32(this.SecondaryColor).ToString()
                        };

                        break;
                    }
                case EventBehaviourPart.Alteration:
                    {
                        BaseTheme = (BaseTheme)Enum.Parse(typeof(BaseTheme), requestContentObj.Value<string>("thm_type"));
                        PrimaryColor = (PrimaryColor)Enum.Parse(typeof(PrimaryColor), requestContentObj.Value<string>("thm_pry_col"));
                        SecondaryColor = (SecondaryColor)Enum.Parse(typeof(SecondaryColor), requestContentObj.Value<string>("thm_sec_col"));

                        themeResponseContent = new EmptyContentKind();

                        break;
                    }
                case EventBehaviourPart.Activation:
                    {
                        PaletteHelper paletteHelper = new PaletteHelper();
                        IBaseTheme baseTheme = BaseTheme == BaseTheme.Dark ? Theme.Dark : Theme.Light;
                        Color primaryColor = SwatchHelper.Lookup[(MaterialDesignColor)PrimaryColor];
                        Color secondaryColor = SwatchHelper.Lookup[(MaterialDesignColor)SecondaryColor];

                        ITheme theme = Theme.Create(baseTheme, primaryColor, secondaryColor);
                        paletteHelper.SetTheme(theme);

                        themeResponseContent = new EmptyContentKind();

                        break;
                    }
                case EventBehaviourPart.Persistence:
                    {
                        sqlSentence = "UPDATE System_ThemeSetting SET BaseTheme='" + BaseTheme.ToString() + "' WHERE Code='01GX8353SEH9NXXND2KZWMYB79'";
                        nativeBaseController.Execute(sqlSentence);

                        sqlSentence = "UPDATE System_ThemeSetting SET PrimaryColor='" + PrimaryColor.ToString() + "' WHERE Code='01GX8353SEH9NXXND2KZWMYB79'";
                        nativeBaseController.Execute(sqlSentence);

                        sqlSentence = "UPDATE System_ThemeSetting SET SecondaryColor='" + SecondaryColor.ToString() + "' WHERE Code='01GX8353SEH9NXXND2KZWMYB79'";
                        nativeBaseController.Execute(sqlSentence);

                        themeResponseContent = new EmptyContentKind();

                        break;
                    }
            }

            eventJsonSentence = eventController.Response(EventPart.ThemeEvent, eventBehaviour, FrameModulePart.ServiceModule, TargetModule, themeResponseContent, true, string.Empty);

            return eventJsonSentence;
        }
    }
}
