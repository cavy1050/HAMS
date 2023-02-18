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

            eventAggregator.GetEvent<ResponseServiceEvent>().Subscribe(OnApplicationAlterationResponseService, ThreadOption.PublisherThread, false, x => x.Contains("ApplicationAlterationService"));
        }

        private void OnApplicationAlterationResponseService(string responseServiceTextArg)
        {
            JObject responseObj = JObject.Parse(responseServiceTextArg);
            JObject responseContentObj = responseObj["svc_cont"].Value<JObject>();
            JArray targetModules = responseObj.Value<JArray>("tagt_mod_name");
            if (targetModules.FirstOrDefault(module => module.Value<string>() == "All") != null)
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
