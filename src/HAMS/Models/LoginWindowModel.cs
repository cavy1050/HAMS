using System;
using System.Windows;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Events;
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
        ApplicationAlterationServiceContentKind requestServiceContent;

        public LoginWindowModel(IContainerProvider containerProviderArg)
        {
            containerProvider = containerProviderArg;
            eventAggregator = containerProviderArg.Resolve<IEventAggregator>();
            requestServiceContent = new ApplicationAlterationServiceContentKind();
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
            requestServiceContent.ApplicationControlType = ControlTypePart.LoginWindow;
            requestServiceContent.ApplicationActiveFlag = ActiveFlagPart.Active;

            eventServiceJsonText = eventServiceController.Request(EventServicePart.ApplicationAlterationService, FrameModulePart.ApplictionModule, FrameModulePart.ServiceModule, requestServiceContent);
            eventAggregator.GetEvent<RequestServiceEvent>().Publish(eventServiceJsonText);
        }

        private void OnAccountVerificationResponseService(string responseServiceTextArg)
        {
            JObject responseObj = JObject.Parse(responseServiceTextArg);
            if (responseObj["ret_code"].ToString() == "1")
            {
                requestServiceContent.ApplicationControlType = ControlTypePart.LoginWindow;
                requestServiceContent.ApplicationActiveFlag = ActiveFlagPart.InActive;

                eventServiceJsonText = eventServiceController.Request(EventServicePart.ApplicationAlterationService, FrameModulePart.ApplictionModule, FrameModulePart.ServiceModule, requestServiceContent);
                eventAggregator.GetEvent<RequestServiceEvent>().Publish(eventServiceJsonText);

                Window mainWindow = containerProvider.Resolve<MainWindow>();
                mainWindow.Show();
            }
        }
    }
}
