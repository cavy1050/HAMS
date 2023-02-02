using System;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Events;
using System.Windows;

namespace HAMS.Frame.Service
{
    public class ServiceLauncher
    {
        IModuleManager moduleManager;
        IEventAggregator eventAggregator;
        IEnvironmentMonitor environmentMonitor;
        IEventServiceController eventServiceController;

        string eventServiceJsonText;

        public ServiceLauncher(IContainerProvider containerProviderArg)
        {
            moduleManager = containerProviderArg.Resolve<IModuleManager>();
            eventAggregator = containerProviderArg.Resolve<IEventAggregator>();
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
            eventServiceController = containerProviderArg.Resolve<IEventServiceController>();
        }

        public void Initialize()
        {
            moduleManager.LoadModuleCompleted += ModuleManager_LoadModuleCompleted;

            eventAggregator.GetEvent<RequestServiceEvent>().Subscribe(OnRequestApplicationAlterationService, ThreadOption.PublisherThread, false, x => x.Contains("ApplicationAlterationService"));
        }

        private void ModuleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs args)
        {
            if (args.ModuleInfo.ModuleName == "LoginModule")
            {
                eventServiceJsonText = eventServiceController.Request(EventServicePart.EventInitializationService, FrameModulePart.ServiceModule, FrameModulePart.All, new EmptyContentKind());
                eventAggregator.GetEvent<RequestServiceEvent>().Publish(eventServiceJsonText);
            }
        }

        private void OnRequestApplicationAlterationService(string requestServiceTextArg)
        {
            JObject requestObj = JObject.Parse(requestServiceTextArg);
            JObject requestContentObj = requestObj["svc_cont"].Value<JObject>();

            ControlTypePart requestControlTypeObj = (ControlTypePart)Enum.Parse(typeof(ControlTypePart), requestContentObj["app_ctl_type"].Value<string>());
            ActiveFlagPart requestActiveFlagObj = (ActiveFlagPart)Enum.Parse(typeof(ActiveFlagPart), requestContentObj["app_act_flag"].Value<string>());

            switch (requestControlTypeObj)
            {
                case ControlTypePart.LoginWindow:
                    if (!environmentMonitor.ApplicationControlSetting.ContainsKey(ControlTypePart.LoginWindow))
                        environmentMonitor.ApplicationControlSetting.Add(requestControlTypeObj, requestActiveFlagObj);
                    else
                        environmentMonitor.ApplicationControlSetting[ControlTypePart.LoginWindow] = requestActiveFlagObj;
                    break;
                case ControlTypePart.MainWindow:
                    if (!environmentMonitor.ApplicationControlSetting.ContainsKey(ControlTypePart.MainWindow))
                        environmentMonitor.ApplicationControlSetting.Add(requestControlTypeObj, requestActiveFlagObj);
                    else
                        environmentMonitor.ApplicationControlSetting[ControlTypePart.MainWindow] = requestActiveFlagObj;
                    break;
                case ControlTypePart.MainLeftDrawer:
                    if (!environmentMonitor.ApplicationControlSetting.ContainsKey(ControlTypePart.MainLeftDrawer))
                        environmentMonitor.ApplicationControlSetting.Add(requestControlTypeObj, requestActiveFlagObj);
                    else
                        environmentMonitor.ApplicationControlSetting[ControlTypePart.MainLeftDrawer] = requestActiveFlagObj;
                    break;
            }
        }
    }
}
