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
            JObject responseObj = JObject.Parse(responseEventTextArg);
            JObject responseContentObj = responseObj.Value<JObject>("svc_cont");
            string targetModule = responseObj.Value<string>("tagt_mdl");

            if (targetModule == "05")
            {
                string responseControlType = responseContentObj.Value<string>("app_ctl_type");
                string responseActiveFlag = responseContentObj.Value<string>("app_act_flag");

                if (responseControlType == "3")
                {
                    if (responseActiveFlag == "0")
                        IsLeftDrawerSwitch = false;
                }
            }
        }
    }
}
