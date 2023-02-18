using System;
using System.Linq;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Events;
using Newtonsoft.Json.Linq;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Events;

namespace HAMS.Frame.Control.MainHeader.Models
{
    public class MainHeaderModel : BindableBase
    {
        IEventAggregator eventAggregator;
        IEnvironmentMonitor environmentMonitor;
        IEventServiceController eventServiceController;

        string eventJsonSentence;

        bool isLeftDrawerSwitch;
        public bool IsLeftDrawerSwitch
        {
            get => isLeftDrawerSwitch;
            set
            {
                SetProperty(ref isLeftDrawerSwitch, value);

                if (isLeftDrawerSwitch)
                    RequestApplicationAlterationService(isLeftDrawerSwitch);
            }
        }

        string userName;
        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        public MainHeaderModel(IContainerProvider containerProviderArgs)
        {
            eventAggregator = containerProviderArgs.Resolve<IEventAggregator>();
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
            eventServiceController = containerProviderArgs.Resolve<IEventServiceController>();
        }

        public void Loaded()
        {
            UserName = environmentMonitor.UserSetting.Name;

            eventAggregator.GetEvent<ResponseServiceEvent>().Subscribe(OnApplicationAlterationResponseService, ThreadOption.PublisherThread, false, x => x.Contains("ApplicationAlterationService"));
        }

        private void RequestApplicationAlterationService(bool isLeftDrawerOpenArg)
        {
            if (isLeftDrawerOpenArg)
                eventJsonSentence = eventServiceController.Request(EventServicePart.ApplicationAlterationService, FrameModulePart.MainHeaderModule, FrameModulePart.ServiceModule,
                    new ApplicationAlterationContentKind
                    {
                        ApplicationControlType = ControlTypePart.MainLeftDrawer,
                        ApplicationActiveFlag = ActiveFlagPart.Active
                    });
            else
                eventJsonSentence = eventServiceController.Request(EventServicePart.ApplicationAlterationService, FrameModulePart.MainHeaderModule, FrameModulePart.ServiceModule,
                    new ApplicationAlterationContentKind
                    {
                        ApplicationControlType = ControlTypePart.MainLeftDrawer,
                        ApplicationActiveFlag = ActiveFlagPart.InActive
                    });

            eventAggregator.GetEvent<RequestServiceEvent>().Publish(eventJsonSentence);
        }


        private void OnApplicationAlterationResponseService(string responseServiceTextArg)
        {
            JObject responseObj = JObject.Parse(responseServiceTextArg);
            JObject responseContentObj = responseObj["svc_cont"].Value<JObject>();
            JArray targetModules = responseObj.Value<JArray>("tagt_mod_name");
            if (targetModules.FirstOrDefault(module => module.Value<string>() == "MainHeaderModule") != null)
            {
                ControlTypePart responseControlType = (ControlTypePart)Enum.Parse(typeof(ControlTypePart), responseContentObj["app_ctl_type"].Value<string>());
                ActiveFlagPart responseActiveFlag = (ActiveFlagPart)Enum.Parse(typeof(ActiveFlagPart), responseContentObj["app_act_flag"].Value<string>());

                if (responseControlType == ControlTypePart.MainLeftDrawer)
                {
                    if (responseActiveFlag == ActiveFlagPart.InActive)
                        IsLeftDrawerSwitch = false;
                }
            }
        }
    }
}
