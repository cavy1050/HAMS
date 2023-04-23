using System;
using System.Linq;
using System.Collections.Generic;
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
        IEventController eventController;

        string eventJsonSentence;
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
            eventController = containerProviderArg.Resolve<IEventController>();
        }

        public void Loaded()
        {
            ShowValidatedServiceMessage();
            eventAggregator.GetEvent<ResponseEvent>().Subscribe(OnAccountActivationResponseEvent, ThreadOption.PublisherThread, false, x => x.Contains("AccountEvent"));
        }

        private void ShowValidatedServiceMessage()
        {
            if (!environmentMonitor.ValidationResult.IsValid)
            {
                environmentMonitor.ValidationResult.Errors.ForEach(failure => messageQueue.Enqueue(failure.ErrorMessage));
                environmentMonitor.ValidationResult.Errors.Clear();
            }
        }

        public void Login(object windowArg)
        {
            currentWindow = windowArg;
            RequestAccountActivationEvent();
        }

        private void RequestAccountActivationEvent()
        {
            LoginContentModelValidator loginContentModelValidator = new LoginContentModelValidator();
            ValidationResult validationResult = loginContentModelValidator.Validate(this);
            if (!validationResult.IsValid)
                validationResult.Errors.ForEach(msg => messageQueue.Enqueue(msg.ErrorMessage));
            else
            {
                eventJsonSentence = eventController.Request(EventPart.AccountEvent, EventBehaviourPart.Activation, FrameModulePart.LoginModule, FrameModulePart.ServiceModule, new AccountActivationRequestContentKind
                {
                    Account = this.Account,
                    Password = this.Password
                });

                eventAggregator.GetEvent<RequestEvent>().Publish(eventJsonSentence);
            }
        }

        private void OnAccountActivationResponseEvent(string responseEvnetTextArg)
        {
            JObject responseObj = JObject.Parse(responseEvnetTextArg);
            FrameModulePart targetModule = (FrameModulePart)Enum.Parse(typeof(FrameModulePart), responseObj.Value<string>("tagt_mdl"));

            if (targetModule == FrameModulePart.LoginModule)
            {
                if (!responseObj.Value<bool>("ret_rst"))
                    messageQueue.Enqueue(responseObj.Value<string>("ret_msg"));
                else
                {
                    if (currentWindow != null)
                    {
                        SystemCommands.CloseWindow(currentWindow as Window);
                        RequestApplicationAlterationEvent();
                    }
                }
            }
        }

        private void RequestApplicationAlterationEvent()
        {
            eventJsonSentence = eventController.Request(EventPart.ApplicationEvent, EventBehaviourPart.Alteration, FrameModulePart.LoginModule, FrameModulePart.ServiceModule, new ApplicationContentKind
            {
                ApplicationControlType = Convert.ToInt32(ControlTypePart.LoginWindow).ToString(),
                ApplicationActiveFlag = Convert.ToInt32(ActiveFlagPart.InActive).ToString()
            });

            eventAggregator.GetEvent<RequestEvent>().Publish(eventJsonSentence);
        }
    }
}
