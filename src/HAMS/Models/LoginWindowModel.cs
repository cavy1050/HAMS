using System;
using System.Windows;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Events;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json.Linq;
using HAMS.Views;
using HAMS.Frame.Kernel.Events;

namespace HAMS.Models
{
    public class LoginWindowModel : BindableBase
    {
        IContainerProvider containerProvider;
        IEventAggregator eventAggregator;
        IEventServiceController eventServiceController;

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
            eventAggregator.GetEvent<RequestServiceEvent>().Subscribe(OnApplicationAlterationRequestService, ThreadOption.PublisherThread, false, x => x.Contains("ApplicationAlterationService"));
        }

        private void OnApplicationAlterationRequestService(string requestServiceTextArg)
        {
            eventServiceController = containerProvider.Resolve<IEventServiceController>();
            eventAggregator.GetEvent<ResponseServiceEvent>().Subscribe(OnAccountVerificationResponseService, ThreadOption.PublisherThread, false, x => x.Contains("AccountVerificationService"));
        }

        private void OnAccountVerificationResponseService(string responseServiceTextArg)
        {
            JObject responseObj = JObject.Parse(responseServiceTextArg);
            if (responseObj["ret_code"].ToString() == "1")
            {
                Window mainWindow = containerProvider.Resolve<MainWindow>();
                mainWindow.Show();
            }
        }
    }
}
