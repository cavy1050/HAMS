using System;
using System.Collections;
using System.Collections.Generic;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Events;
using MaterialDesignThemes.Wpf;
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

        public void RequestAccountVerificationServiceService()
        {
            LoginContentModelValidator loginContentModelValidator = new LoginContentModelValidator();
            ValidationResult validationResult = loginContentModelValidator.Validate(this);
            if (validationResult.IsValid)
            {
                AccountVerificationServiceRequestContentKind requestServiceContent = new AccountVerificationServiceRequestContentKind();
                requestServiceContent.Account = Account;
                requestServiceContent.Password = Password;

                eventServiceJsonText = eventServiceController.Request(EventServicePart.AccountVerificationService, FrameModulePart.LoginModule, FrameModulePart.ServiceModule, requestServiceContent);
                eventAggregator.GetEvent<RequestServiceEvent>().Publish(eventServiceJsonText);
            }
        }
    }
}
