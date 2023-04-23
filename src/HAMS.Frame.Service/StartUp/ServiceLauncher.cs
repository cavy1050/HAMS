using System;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Events;
using Newtonsoft.Json.Linq;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Events;
using HAMS.Frame.Service.Peripherals;

namespace HAMS.Frame.Service
{
    public class ServiceLauncher
    {
        IContainerProvider containerProvider;
        IModuleManager moduleManager;
        IEventAggregator eventAggregator;
        IEventController eventController;

        IEventResponseController applicationController;
        IEventResponseController accountControler;
        IEventResponseController extensionModuleControler;
        IEventResponseController themeController;

        string eventJsonSentence, targetModule;
        JObject requestObj;

        public ServiceLauncher(IContainerProvider containerProviderArg)
        {
            containerProvider = containerProviderArg;
            moduleManager = containerProviderArg.Resolve<IModuleManager>();
            eventAggregator = containerProviderArg.Resolve<IEventAggregator>();
            eventController = containerProviderArg.Resolve<IEventController>();
        }

        public static void RegisterServices(IContainerRegistry containerRegistryArg)
        {
            containerRegistryArg.Register<IEventResponseController, ApplicationController>(EventPart.ApplicationEvent.ToString());
            containerRegistryArg.Register<IEventResponseController, AccountControler>(EventPart.AccountEvent.ToString());
            containerRegistryArg.Register<IEventResponseController, ExtensionModuleController>(EventPart.ExtensionModuleEvent.ToString());
            containerRegistryArg.Register<IEventResponseController, ThemeController>(EventPart.ThemeEvent.ToString());
        }

        public void Initialize()
        {
            moduleManager.LoadModuleCompleted += ModuleManager_LoadModuleCompleted;

            eventAggregator.GetEvent<RequestEvent>().Subscribe(OnApplicationRequestEvent, ThreadOption.PublisherThread, false, x => x.Contains("ApplicationEvent"));
            eventAggregator.GetEvent<RequestEvent>().Subscribe(OnAccountRequestEvent, ThreadOption.PublisherThread, false, x => x.Contains("AccountEvent"));
            eventAggregator.GetEvent<RequestEvent>().Subscribe(OnExtensionModuleRequestEvent, ThreadOption.PublisherThread, false, x => x.Contains("ExtensionModuleEvent"));
            eventAggregator.GetEvent<RequestEvent>().Subscribe(OnThemeRequestEvent, ThreadOption.PublisherThread, false, x => x.Contains("ThemeEvent"));
        }

        private void ModuleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs args)
        {
            if (args.ModuleInfo.ModuleName == "LoginModule")
            {
                //初始化程序控件设置
                eventJsonSentence = eventController.Request(EventPart.ApplicationEvent, EventBehaviourPart.Initialization, FrameModulePart.ServiceModule, FrameModulePart.ServiceModule, new ApplicationContentKind
                {
                    ApplicationControlType = Convert.ToInt32(ControlTypePart.LoginWindow).ToString(),
                    ApplicationActiveFlag = Convert.ToInt32(ActiveFlagPart.Active).ToString(),
                });

                eventAggregator.GetEvent<RequestEvent>().Publish(eventJsonSentence);
            }
        }

        private void OnApplicationRequestEvent(string requestEventTextArg)
        {
            requestObj = JObject.Parse(requestEventTextArg);
            FrameModulePart targetModule = (FrameModulePart)Enum.Parse(typeof(FrameModulePart), requestObj.Value<string>("tagt_mdl"));

            if (targetModule == FrameModulePart.ServiceModule)
            {
                applicationController = containerProvider.Resolve<IEventResponseController>(EventPart.ApplicationEvent.ToString());
                eventJsonSentence = applicationController.Response(requestEventTextArg);
                eventAggregator.GetEvent<ResponseEvent>().Publish(eventJsonSentence);
            }
        }

        private void OnAccountRequestEvent(string requestEventTextArg)
        {
            requestObj = JObject.Parse(requestEventTextArg);
            FrameModulePart targetModule = (FrameModulePart)Enum.Parse(typeof(FrameModulePart), requestObj.Value<string>("tagt_mdl"));

            if (targetModule == FrameModulePart.ServiceModule)
            {
                accountControler = containerProvider.Resolve<IEventResponseController>(EventPart.AccountEvent.ToString());
                eventJsonSentence = accountControler.Response(requestEventTextArg);
                eventAggregator.GetEvent<ResponseEvent>().Publish(eventJsonSentence);
            }
        }

        private void OnExtensionModuleRequestEvent(string requestEventTextArg)
        {
            requestObj = JObject.Parse(requestEventTextArg);
            FrameModulePart targetModule = (FrameModulePart)Enum.Parse(typeof(FrameModulePart), requestObj.Value<string>("tagt_mdl"));

            if (targetModule == FrameModulePart.ServiceModule)
            {
                extensionModuleControler = containerProvider.Resolve<IEventResponseController>(EventPart.ExtensionModuleEvent.ToString());
                eventJsonSentence = extensionModuleControler.Response(requestEventTextArg);
                eventAggregator.GetEvent<ResponseEvent>().Publish(eventJsonSentence);
            }
        }

        private void OnThemeRequestEvent(string requestEventTextArg)
        {
            requestObj = JObject.Parse(requestEventTextArg);
            FrameModulePart targetModule = (FrameModulePart)Enum.Parse(typeof(FrameModulePart), requestObj.Value<string>("tagt_mdl"));

            if (targetModule == FrameModulePart.ServiceModule)
            {
                themeController = containerProvider.Resolve<IEventResponseController>(EventPart.ThemeEvent.ToString());
                eventJsonSentence = themeController.Response(requestEventTextArg);
                eventAggregator.GetEvent<ResponseEvent>().Publish(eventJsonSentence);
            }
        }
    }
}
