using System;
using Prism.Ioc;
using Prism.Mvvm;
using MaterialDesignThemes.Wpf;
using FluentValidation.Results;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Services;

namespace HAMS.Frame.Control.Login.Models
{
    public class LoginContentModel : BindableBase
    {
        ISnackbarMessageQueue messageQueue;
        IEnvironmentMonitor environmentMonitor;

        ValidationResult infoSeverityResult, errorSeverityResult;

        string returnMessage;
        public string ReturnMessage
        {
            get => returnMessage;
            set => returnMessage = value;
        }

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
            messageQueue = containerProviderArg.Resolve<ISnackbarMessageQueue>();
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
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
    }
}
