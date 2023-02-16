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

        ISnackbarMessageQueue mainMessageQueue;
        public ISnackbarMessageQueue MainMessageQueue
        {
            get => mainMessageQueue;
            set => SetProperty(ref mainMessageQueue, value);
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

        bool isLeftDrawerOpen = true;
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
            MainMessageQueue = containerProviderArg.Resolve<ISnackbarMessageQueue>();
            RegionManager = containerProviderArg.Resolve<IRegionManager>();
            eventServiceController = containerProviderArg.Resolve<IEventServiceController>();

            eventAggregator.GetEvent<ResponseServiceEvent>().Subscribe(OnApplicationAlterationResponseService, ThreadOption.PublisherThread, false, x => x.Contains("ApplicationAlterationService"));
        }

        private void OnApplicationAlterationResponseService(string responseServiceTextArg)
        {
            JObject responseObj = JObject.Parse(responseServiceTextArg);
            JObject responseContentObj = responseObj["svc_cont"].Value<JObject>();
            JArray targetModules = responseObj.Value<JArray>("tagt_mod_name");
            if (targetModules.FirstOrDefault(module => module.Value<string>() == "ApplictionModule") != null)
            {
                ControlTypePart responseControlType = (ControlTypePart)Enum.Parse(typeof(ControlTypePart), responseContentObj["app_ctl_type"].Value<string>());
                ActiveFlagPart responseActiveFlag = (ActiveFlagPart)Enum.Parse(typeof(ActiveFlagPart), responseContentObj["app_act_flag"].Value<string>());

                if (responseControlType == ControlTypePart.LoginWindow && responseActiveFlag == ActiveFlagPart.InActive)
                {
                    eventJsonSentence = eventServiceController.Request(EventServicePart.ApplicationAlterationService,
                                            FrameModulePart.ApplictionModule, FrameModulePart.ServiceModule,
                                            new ApplicationAlterationContentKind
                                            {
                                                ApplicationControlType = ControlTypePart.MainWindow,
                                                ApplicationActiveFlag = ActiveFlagPart.Active
                                            });

                    eventAggregator.GetEvent<RequestServiceEvent>().Publish(eventJsonSentence);
                }
            }
        }
    }
}
