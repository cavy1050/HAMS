using System;
using System.Linq;
using System.Windows;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Events;
using MaterialDesignThemes.Wpf;
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
        string eventJsonSentence;
        object currentWindow;
        AccountVerificationRequestContentKind accountVerificationRequestContent;
        ApplicationAlterationContentKind applicationAlterationContent;

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

        public void Loaded()
        {
            ShowServiceMessage();
            eventAggregator.GetEvent<ResponseServiceEvent>().Subscribe(OnAccountVerificationResponseService, ThreadOption.PublisherThread, false, x => x.Contains("AccountVerificationService"));
        }

        private void ShowServiceMessage()
        {
            infoSeverityResult = environmentMonitor.SeveritySetting.GetContent(SeverityLevelPart.Info);
            errorSeverityResult = environmentMonitor.SeveritySetting.GetContent(SeverityLevelPart.Error);

            if (infoSeverityResult.IsValid != true)
                infoSeverityResult.Errors.ForEach(x => messageQueue.Enqueue(x.ErrorMessage));

            if (errorSeverityResult.IsValid != true)
                errorSeverityResult.Errors.ForEach(x => messageQueue.Enqueue(x.ErrorMessage));
        }

        public void Login(object windowArg)
        {
            currentWindow = windowArg;
            RequestAccountVerificationService();
        }

        private void RequestAccountVerificationService()
        {
            LoginContentModelValidator loginContentModelValidator = new LoginContentModelValidator();
            ValidationResult validationResult = loginContentModelValidator.Validate(this);
            if (!validationResult.IsValid)
                validationResult.Errors.ForEach(msg => messageQueue.Enqueue(msg.ErrorMessage));
            else
            {
                accountVerificationRequestContent = new AccountVerificationRequestContentKind
                {
                    Account = Account,
                    Password = Password
                };

                eventJsonSentence = eventServiceController.Request(EventServicePart.AccountVerificationService, FrameModulePart.LoginModule, FrameModulePart.ServiceModule, accountVerificationRequestContent);
                eventAggregator.GetEvent<RequestServiceEvent>().Publish(eventJsonSentence);
            }
        }

        private void OnAccountVerificationResponseService(string responseServiceTextArg)
        {
            JObject responseObj = JObject.Parse(responseServiceTextArg);
            FrameModulePart targetModule = (FrameModulePart)Enum.Parse(typeof(FrameModulePart), responseObj.Value<string>("tagt_mod_name"));

            if (targetModule == FrameModulePart.LoginModule)
            {
                if (!responseObj.Value<bool>("ret_rst"))
                    messageQueue.Enqueue(responseObj.Value<string>("ret_msg"));
                else
                {
                    if (currentWindow != null)
                    {
                        SystemCommands.CloseWindow(currentWindow as Window);
                        RequestApplicationAlterationService();
                    }
                }
            }
        }

        private void RequestApplicationAlterationService()
        {
            applicationAlterationContent = new ApplicationAlterationContentKind
            {
                ApplicationControlType = ControlTypePart.LoginWindow,
                ApplicationActiveFlag = ActiveFlagPart.InActive
            };

            eventJsonSentence = eventServiceController.Request(EventServicePart.ApplicationAlterationService, FrameModulePart.LoginModule, FrameModulePart.ServiceModule, applicationAlterationContent);
            eventAggregator.GetEvent<RequestServiceEvent>().Publish(eventJsonSentence);
        }
    }
}
