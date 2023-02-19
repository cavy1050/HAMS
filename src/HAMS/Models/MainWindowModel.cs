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
        IEventServiceController eventServiceController;

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
            eventServiceController = containerProviderArg.Resolve<IEventServiceController>();

            eventAggregator.GetEvent<ResponseServiceEvent>().Subscribe(OnApplicationAlterationResponseService, ThreadOption.PublisherThread, false, x => x.Contains("ApplicationAlterationService"));
        }

        private void OnApplicationAlterationResponseService(string responseServiceTextArg)
        {
            JObject responseObj = JObject.Parse(responseServiceTextArg);
            JObject responseContentObj = responseObj["svc_cont"].Value<JObject>();
            FrameModulePart targetModule = (FrameModulePart)Enum.Parse(typeof(FrameModulePart), responseObj.Value<string>("tagt_mod_name"));

            if (targetModule == FrameModulePart.ApplictionModule)
            {
                ControlTypePart responseControlType = (ControlTypePart)Enum.Parse(typeof(ControlTypePart), responseContentObj["app_ctl_type"].Value<string>());
                ActiveFlagPart responseActiveFlag = (ActiveFlagPart)Enum.Parse(typeof(ActiveFlagPart), responseContentObj["app_act_flag"].Value<string>());

                if (responseControlType == ControlTypePart.MainLeftDrawer)
                {
                    if (responseActiveFlag == ActiveFlagPart.Active)
                        IsLeftDrawerOpen = true;
                    else
                        IsLeftDrawerOpen = false;
                }
            }
        }

        public void DrawerHostClosed()
        {
            if (!IsLeftDrawerOpen)
            {
                eventJsonSentence = eventServiceController.Request(EventServicePart.ApplicationAlterationService, FrameModulePart.ApplictionModule, FrameModulePart.ServiceModule,
                    new ApplicationAlterationContentKind
                    {
                        ApplicationControlType = ControlTypePart.MainLeftDrawer,
                        ApplicationActiveFlag = ActiveFlagPart.InActive
                    });

                eventAggregator.GetEvent<RequestServiceEvent>().Publish(eventJsonSentence);
            }
        }
    }
}
