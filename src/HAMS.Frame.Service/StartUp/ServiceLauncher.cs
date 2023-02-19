using System;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Events;
using Newtonsoft.Json.Linq;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Events;
using HAMS.Frame.Kernel.Services;
using HAMS.Frame.Service.Peripherals;

namespace HAMS.Frame.Service
{
    public class ServiceLauncher
    {
        IContainerProvider containerProvider;
        IModuleManager moduleManager;
        IEventAggregator eventAggregator;
        IEventServiceController eventServiceController;

        IServiceController applicationAlterationController;
        IServiceController accountVerificationControler;
        IServiceController extensionModuleInitializationServiceControler;
        IServiceController extensionModuleActivationServiceControler;

        string eventJsonSentence;
        ApplicationAlterationContentKind applicationAlterationContent;

        public ServiceLauncher(IContainerProvider containerProviderArg)
        {
            containerProvider = containerProviderArg;
            moduleManager = containerProviderArg.Resolve<IModuleManager>();
            eventAggregator = containerProviderArg.Resolve<IEventAggregator>();
            eventServiceController = containerProviderArg.Resolve<IEventServiceController>();
        }

        public static void RegisterServices(IContainerRegistry containerRegistryArg)
        {
            containerRegistryArg.Register<IServiceController, ApplicationAlterationController>(EventServicePart.ApplicationAlterationService.ToString());
            containerRegistryArg.Register<IServiceController, AccountVerificationControler>(EventServicePart.AccountVerificationService.ToString());
            containerRegistryArg.Register<IServiceController, ModuleInitializationServiceControler>(EventServicePart.ModuleInitializationService.ToString());
            containerRegistryArg.Register<IServiceController, ModuleActivationServiceController>(EventServicePart.ModuleActivationService.ToString());
        }

        public void Initialize()
        {
            moduleManager.LoadModuleCompleted += ModuleManager_LoadModuleCompleted;

            eventAggregator.GetEvent<RequestServiceEvent>().Subscribe(OnRequestApplicationAlterationService, ThreadOption.PublisherThread, false, x => x.Contains("ApplicationAlterationService"));
            eventAggregator.GetEvent<RequestServiceEvent>().Subscribe(OnRequestAccountVerificationService, ThreadOption.PublisherThread, false, x => x.Contains("AccountVerificationService"));
            eventAggregator.GetEvent<RequestServiceEvent>().Subscribe(OnRequestModuleInitializationService, ThreadOption.PublisherThread, false, x => x.Contains("ModuleInitializationService"));
            eventAggregator.GetEvent<RequestServiceEvent>().Subscribe(OnRequestModuleActivationService, ThreadOption.PublisherThread, false, x => x.Contains("ModuleActivationService"));
        }

        private void ModuleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs args)
        {
            if (args.ModuleInfo.ModuleName == "LoginModule")
            {
                applicationAlterationContent = new ApplicationAlterationContentKind
                {
                    ApplicationControlType = ControlTypePart.LoginWindow,
                    ApplicationActiveFlag = ActiveFlagPart.Active
                };

                eventJsonSentence = eventServiceController.Request(EventServicePart.ApplicationAlterationService, FrameModulePart.ServiceModule, FrameModulePart.ServiceModule, applicationAlterationContent);
                eventAggregator.GetEvent<RequestServiceEvent>().Publish(eventJsonSentence);
            }
        }

        private void OnRequestApplicationAlterationService(string requestServiceTextArg)
        {
            JObject requestObj = JObject.Parse(requestServiceTextArg);
            FrameModulePart targetModule = (FrameModulePart)Enum.Parse(typeof(FrameModulePart), requestObj.Value<string>("tagt_mod_name"));

            if (targetModule == FrameModulePart.ServiceModule)
            {
                applicationAlterationController = containerProvider.Resolve<IServiceController>(EventServicePart.ApplicationAlterationService.ToString());
                eventJsonSentence = applicationAlterationController.Response(requestServiceTextArg);
                eventAggregator.GetEvent<ResponseServiceEvent>().Publish(eventJsonSentence);
            }
        }

        private void OnRequestAccountVerificationService(string requestServiceTextArg)
        {
            JObject requestObj = JObject.Parse(requestServiceTextArg);
            FrameModulePart targetModule = (FrameModulePart)Enum.Parse(typeof(FrameModulePart), requestObj.Value<string>("tagt_mod_name"));

            if (targetModule == FrameModulePart.ServiceModule)
            {
                accountVerificationControler = containerProvider.Resolve<IServiceController>(EventServicePart.AccountVerificationService.ToString());
                eventJsonSentence = accountVerificationControler.Response(requestServiceTextArg);
                eventAggregator.GetEvent<ResponseServiceEvent>().Publish(eventJsonSentence);
            }
        }

        private void OnRequestModuleInitializationService(string requestServiceTextArg)
        {
            JObject requestObj = JObject.Parse(requestServiceTextArg);
            FrameModulePart targetModule = (FrameModulePart)Enum.Parse(typeof(FrameModulePart), requestObj.Value<string>("tagt_mod_name"));

            if (targetModule == FrameModulePart.ServiceModule)
            {
                extensionModuleInitializationServiceControler = containerProvider.Resolve<IServiceController>(EventServicePart.ModuleInitializationService.ToString());
                eventJsonSentence = extensionModuleInitializationServiceControler.Response(requestServiceTextArg);
                eventAggregator.GetEvent<ResponseServiceEvent>().Publish(eventJsonSentence);
            }
        }

        private void OnRequestModuleActivationService(string requestServiceTextArg)
        {
            JObject requestObj = JObject.Parse(requestServiceTextArg);
            FrameModulePart targetModule = (FrameModulePart)Enum.Parse(typeof(FrameModulePart), requestObj.Value<string>("tagt_mod_name"));

            if (targetModule == FrameModulePart.ServiceModule)
            {
                extensionModuleActivationServiceControler = containerProvider.Resolve<IServiceController>(EventServicePart.ModuleActivationService.ToString());
                eventJsonSentence = extensionModuleActivationServiceControler.Response(requestServiceTextArg);
                eventAggregator.GetEvent<ResponseServiceEvent>().Publish(eventJsonSentence);
            }
        }
    }
}
