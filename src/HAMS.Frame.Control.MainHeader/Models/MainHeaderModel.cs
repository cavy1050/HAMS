using System;
using System.Linq;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Events;
using Newtonsoft.Json.Linq;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Events;
using System.Windows;

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
            set => SetProperty(ref isLeftDrawerSwitch, value);
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

        public void LeftDrawerSwitchClick()
        {
            eventJsonSentence = eventServiceController.Request(EventServicePart.ApplicationAlterationService, FrameModulePart.MainHeaderModule, FrameModulePart.ServiceModule,
                new ApplicationAlterationContentKind
                {
                    ApplicationControlType = ControlTypePart.MainLeftDrawer,
                    ApplicationActiveFlag = IsLeftDrawerSwitch == true ? ActiveFlagPart.Active : ActiveFlagPart.InActive
                });

            eventAggregator.GetEvent<RequestServiceEvent>().Publish(eventJsonSentence);
        }

        private void OnApplicationAlterationResponseService(string responseServiceTextArg)
        {
            JObject responseObj = JObject.Parse(responseServiceTextArg);
            JObject responseContentObj = responseObj.Value<JObject>("svc_cont");
            FrameModulePart targetModule = (FrameModulePart)Enum.Parse(typeof(FrameModulePart), responseObj.Value<string>("tagt_mod_name"));

            if (targetModule == FrameModulePart.MainHeaderModule)
            {
                ControlTypePart responseControlType = (ControlTypePart)Enum.Parse(typeof(ControlTypePart), responseContentObj.Value<string>("app_ctl_type"));
                ActiveFlagPart responseActiveFlag = (ActiveFlagPart)Enum.Parse(typeof(ActiveFlagPart), responseContentObj.Value<string>("app_act_flag"));

                if (responseControlType == ControlTypePart.MainLeftDrawer)
                {
                    if (responseActiveFlag == ActiveFlagPart.InActive)
                        IsLeftDrawerSwitch = false;
                }
            }
        }
    }
}
