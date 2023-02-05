using System;
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
        IEventServiceController eventServiceController;

        string eventServiceJsonText;
        ApplicationAlterationRequestContentKind applicationAlterationRequestContent;

        ISnackbarMessageQueue messageQueue;
        public ISnackbarMessageQueue LoginMessageQueue
        {
            get => messageQueue;
            set => SetProperty(ref messageQueue, value);
        }

        public LoginWindowModel(IContainerProvider containerProviderArg)
        {
            containerProvider = containerProviderArg;
            eventAggregator = containerProviderArg.Resolve<IEventAggregator>();
            LoginMessageQueue = containerProviderArg.Resolve<ISnackbarMessageQueue>();
            applicationAlterationRequestContent = new ApplicationAlterationRequestContentKind();
            eventAggregator.GetEvent<RequestServiceEvent>().Subscribe(OnRequestEventInitializationService, ThreadOption.PublisherThread, false, x => x.Contains("EventInitializationService"));
        }

        private void OnRequestEventInitializationService(string requestServiceTextArg)
        {
            eventServiceController = containerProvider.Resolve<IEventServiceController>();
            PublishApplicationAlterationService();
            eventAggregator.GetEvent<ResponseServiceEvent>().Subscribe(OnAccountVerificationResponseService, ThreadOption.PublisherThread, false, x => x.Contains("AccountVerificationService"));
        }

        private void PublishApplicationAlterationService()
        {
            applicationAlterationRequestContent.ApplicationControlType = ControlTypePart.LoginWindow;
            applicationAlterationRequestContent.ApplicationActiveFlag = ActiveFlagPart.Active;

            eventServiceJsonText = eventServiceController.Request(EventServicePart.ApplicationAlterationService, FrameModulePart.ApplictionModule, FrameModulePart.ServiceModule, applicationAlterationRequestContent);
            eventAggregator.GetEvent<RequestServiceEvent>().Publish(eventServiceJsonText);
        }

        private void OnAccountVerificationResponseService(string responseServiceTextArg)
        {
            JObject responseObj = JObject.Parse(responseServiceTextArg);
            if (responseObj["ret_code"].ToString() == "1")
            {
                applicationAlterationRequestContent.ApplicationControlType = ControlTypePart.LoginWindow;
                applicationAlterationRequestContent.ApplicationActiveFlag = ActiveFlagPart.InActive;

                eventServiceJsonText = eventServiceController.Request(EventServicePart.ApplicationAlterationService, FrameModulePart.ApplictionModule, FrameModulePart.ServiceModule, applicationAlterationRequestContent);
                eventAggregator.GetEvent<RequestServiceEvent>().Publish(eventServiceJsonText);

                Window mainWindow = containerProvider.Resolve<MainWindow>();
                mainWindow.Show();
            }
        }
    }
}
