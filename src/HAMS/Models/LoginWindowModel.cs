using System;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Events;
using HAMS.Frame.Kernel.Services;

namespace HAMS.Models
{
    public class LoginWindowModel : BindableBase
    {
        IContainerProvider containerProvider;
        IEventAggregator eventAggregator;
        IEventServiceController eventServiceController;

        string eventServiceJsonText;

        public LoginWindowModel(IContainerProvider containerProviderArg)
        {
            containerProvider = containerProviderArg;
            eventAggregator = containerProviderArg.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<RequestServiceEvent>().Subscribe(OnRequestEventInitializationService, ThreadOption.PublisherThread, false, x => x.Contains("EventInitializationService"));
        }

        private void OnRequestEventInitializationService(string arg)
        {
            eventServiceController = containerProvider.Resolve<IEventServiceController>();
            PublishApplicationAlterationService();
        }

        private void PublishApplicationAlterationService()
        {
            ApplicationAlterationServiceContentKind requestServiceContent = new ApplicationAlterationServiceContentKind();
            requestServiceContent.ApplicationControlType = ControlTypePart.LoginWindow;
            requestServiceContent.ApplicationActiveFlag = ActiveFlagPart.Active;

            eventServiceJsonText = eventServiceController.Request(EventServicePart.ApplicationAlterationService, FrameModulePart.ApplictionModule, FrameModulePart.ServiceModule, requestServiceContent);
            eventAggregator.GetEvent<RequestServiceEvent>().Publish(eventServiceJsonText);
        }
    }
}
