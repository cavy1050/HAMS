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
        IEventController eventController;

        string eventJsonSentence;
        JObject responseObj, responseContentObj;

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
            eventController = containerProviderArgs.Resolve<IEventController>();
        }

        public void Loaded()
        {
            UserName = environmentMonitor.UserSetting.Name;

            eventAggregator.GetEvent<ResponseEvent>().Subscribe(OnApplicationAlterationResponseEvent, ThreadOption.PublisherThread, false, x => x.Contains("ApplicationEvent"));
        }

        public void LeftDrawerSwitchClick()
        {
            eventJsonSentence = eventController.Request(EventPart.ApplicationEvent, EventBehaviourPart.Alteration, FrameModulePart.MainHeaderModule, FrameModulePart.ServiceModule,
                new ApplicationContentKind
                {
                    ApplicationControlType = Convert.ToInt32(ControlTypePart.MainLeftDrawer).ToString(),
                    ApplicationActiveFlag = IsLeftDrawerSwitch == true ? Convert.ToInt32(ActiveFlagPart.Active).ToString() : Convert.ToInt32(ActiveFlagPart.InActive).ToString()
                });

            eventAggregator.GetEvent<RequestEvent>().Publish(eventJsonSentence);
        }

        private void OnApplicationAlterationResponseEvent(string responseEventTextArg)
        {
            responseObj = JObject.Parse(responseEventTextArg);
            responseContentObj = responseObj.Value<JObject>("svc_cont");
            FrameModulePart targetModule = (FrameModulePart)Enum.Parse(typeof(FrameModulePart), responseObj.Value<string>("tagt_mdl"));

            if (targetModule == FrameModulePart.MainHeaderModule)
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
