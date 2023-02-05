using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Events;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using FluentValidation.Results;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Events;

namespace HAMS.Frame.Control.Login.Models
{
    public class LoginContentModel : BindableBase
    {
        ISnackbarMessageQueue messageQueue;
        IEnvironmentMonitor environmentMonitor;
        IEventAggregator eventAggregator;
        IEventServiceController eventServiceController;

        ValidationResult infoSeverityResult, errorSeverityResult;
        string eventServiceJsonText;
        object currentWindow;

        string account;
        public string Account
        {
            get => account;
            set => SetProperty(ref account, value);
        }

        string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public LoginContentModel(IContainerProvider containerProviderArg)
        {
            eventAggregator = containerProviderArg.Resolve<IEventAggregator>();
            messageQueue = containerProviderArg.Resolve<ISnackbarMessageQueue>();
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
            eventServiceController = containerProviderArg.Resolve<IEventServiceController>();

            eventAggregator.GetEvent<ResponseServiceEvent>().Subscribe(OnAccountVerificationResponseService, ThreadOption.PublisherThread, false, x => x.Contains("AccountVerificationService"));
        }

        public void Login(object windowArg)
        {
            currentWindow = windowArg;
            RequestAccountVerificationService();
        }

        public void ShowInitializeServiceMessage()
        {
            infoSeverityResult = environmentMonitor.SeveritySetting.GetContent(SeverityLevelPart.Info);
            errorSeverityResult = environmentMonitor.SeveritySetting.GetContent(SeverityLevelPart.Error);

            if (infoSeverityResult.IsValid != true)
                infoSeverityResult.Errors.ForEach(x => messageQueue.Enqueue(x.ErrorMessage));

            if (errorSeverityResult.IsValid != true)
                errorSeverityResult.Errors.ForEach(x => messageQueue.Enqueue(x.ErrorMessage));
        }

        private void RequestAccountVerificationService()
        {
            LoginContentModelValidator loginContentModelValidator = new LoginContentModelValidator();
            ValidationResult validationResult = loginContentModelValidator.Validate(this);
            if (validationResult.IsValid)
            {
                AccountVerificationRequestContentKind requestContent = new AccountVerificationRequestContentKind();
                requestContent.Account = Account;
                requestContent.Password = Password;

                eventServiceJsonText = eventServiceController.Request(EventServicePart.AccountVerificationService, FrameModulePart.LoginModule, FrameModulePart.ServiceModule, requestContent);
                eventAggregator.GetEvent<RequestServiceEvent>().Publish(eventServiceJsonText);
            }
        }

        private void OnAccountVerificationResponseService(string responseServiceTextArg)
        {
            JObject responseObj = JObject.Parse(responseServiceTextArg);
            if (responseObj["ret_code"].ToString() != "1")
                messageQueue.Enqueue(responseObj["ret_msg"].ToString());
            else
            {
                if (currentWindow != null)
                    SystemCommands.CloseWindow(currentWindow as Window);
            }
        }
    }
}
