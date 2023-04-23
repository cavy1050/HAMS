using System;
using System.Linq;
using System.Windows;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Events;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json.Linq;
using HAMS.Views;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Events;

namespace HAMS.Models
{
    public class LoginWindowModel : BindableBase
    {
        IContainerProvider containerProvider;
        IEventAggregator eventAggregator;

        JObject responseObj, responseContentObj;

        ISnackbarMessageQueue messageQueue;
        public ISnackbarMessageQueue MessageQueue
        {
            get => messageQueue;
            set => SetProperty(ref messageQueue, value);
        }

        public LoginWindowModel(IContainerProvider containerProviderArg)
        {
            containerProvider = containerProviderArg;
            eventAggregator = containerProviderArg.Resolve<IEventAggregator>();
            MessageQueue = containerProviderArg.Resolve<ISnackbarMessageQueue>();

            eventAggregator.GetEvent<ResponseEvent>().Subscribe(OnApplicationAlterationResponseEvent, ThreadOption.PublisherThread, false, x => x.Contains("ApplicationEvent"));
        }

        private void OnApplicationAlterationResponseEvent(string responseeEventTextArg)
        {
            responseObj = JObject.Parse(responseeEventTextArg);
            responseContentObj = responseObj.Value<JObject>("svc_cont");
            FrameModulePart targetModule = (FrameModulePart)Enum.Parse(typeof(FrameModulePart), responseObj.Value<string>("tagt_mdl"));
            ControlTypePart responseControlType = (ControlTypePart)Enum.Parse(typeof(ControlTypePart), responseContentObj.Value<string>("app_ctl_type"));
            ActiveFlagPart responseActiveFlag = (ActiveFlagPart)Enum.Parse(typeof(ActiveFlagPart), responseContentObj.Value<string>("app_act_flag"));

            if (targetModule == FrameModulePart.ApplictionModule && responseControlType == ControlTypePart.LoginWindow && responseActiveFlag == ActiveFlagPart.InActive)
            {
                Window mainWindow = containerProvider.Resolve<MainWindow>();
                mainWindow.Show();
            }
        }
    }
}
