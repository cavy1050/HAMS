using System;
using System.Linq;
using System.Windows;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Events;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json.Linq;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Events;

namespace HAMS.Models
{
    public class MainWindowModel : BindableBase
    {
        IEventAggregator eventAggregator;
        IEventController eventController;

        string eventJsonSentence;

        ISnackbarMessageQueue messageQueue;
        public ISnackbarMessageQueue MessageQueue
        {
            get => messageQueue;
            set => SetProperty(ref messageQueue, value);
        }

        IRegionManager regionManager;
        public IRegionManager RegionManager
        {
            get => regionManager;
            set => SetProperty(ref regionManager, value);
        }

        double workAreaWidth;
        public double WorkAreaWidth
        {
            get => workAreaWidth;
            set => SetProperty(ref workAreaWidth, value);
        }

        double workAreaHeight;
        public double WorkAreaHeight
        {
            get => workAreaHeight;
            set => SetProperty(ref workAreaHeight, value);
        }

        bool isLeftDrawerOpen;
        public bool IsLeftDrawerOpen
        {
            get => isLeftDrawerOpen;
            set => SetProperty(ref isLeftDrawerOpen, value);
        }

        public MainWindowModel(IContainerProvider containerProviderArg)
        {
            WorkAreaWidth = SystemParameters.WorkArea.Width;
            WorkAreaHeight = SystemParameters.WorkArea.Height;

            eventAggregator = containerProviderArg.Resolve<IEventAggregator>();
            MessageQueue = containerProviderArg.Resolve<ISnackbarMessageQueue>();
            RegionManager = containerProviderArg.Resolve<IRegionManager>();
            eventController = containerProviderArg.Resolve<IEventController>();

            eventAggregator.GetEvent<ResponseEvent>().Subscribe(OnApplicationAlterationResponseEvent, ThreadOption.PublisherThread, false, x => x.Contains("ApplicationEvent"));
        }

        private void OnApplicationAlterationResponseEvent(string responseEventTextArg)
        {
            JObject responseObj = JObject.Parse(responseEventTextArg);
            JObject responseContentObj = responseObj["svc_cont"].Value<JObject>();
            FrameModulePart targetModule = (FrameModulePart)Enum.Parse(typeof(FrameModulePart), responseObj.Value<string>("tagt_mdl"));

            if (targetModule == FrameModulePart.ApplictionModule)
            {
                ControlTypePart responseControlType = (ControlTypePart)Enum.Parse(typeof(ControlTypePart), responseContentObj["app_ctl_type"].Value<string>());
                ActiveFlagPart responseActiveFlag = (ActiveFlagPart)Enum.Parse(typeof(ActiveFlagPart), responseContentObj["app_act_flag"].Value<string>());

                if (responseActiveFlag == ActiveFlagPart.Active)
                    IsLeftDrawerOpen = true;
                else
                    IsLeftDrawerOpen = false;
            }
        }

        public void DrawerHostClosed()
        {
            if (!IsLeftDrawerOpen)
            {
                eventJsonSentence = eventController.Request(EventPart.ApplicationEvent , EventBehaviourPart.Alteration , FrameModulePart.ApplictionModule, FrameModulePart.ServiceModule,
                    new ApplicationContentKind
                    {
                        ApplicationControlType = Convert.ToInt32(ControlTypePart.MainLeftDrawer).ToString(),
                        ApplicationActiveFlag = Convert.ToInt32(ActiveFlagPart.InActive).ToString()
                    });

                eventAggregator.GetEvent<RequestEvent>().Publish(eventJsonSentence);
            }
        }
    }
}
