using System;
using Prism.Ioc;
using Prism.Mvvm;
using MaterialDesignThemes.Wpf;
using HAMS.Models;

namespace HAMS.ViewModels
{
    public class LoginWindowViewModel : BindableBase
    {
        LoginWindowModel loginWindowModel;
        public LoginWindowModel LoginWindowModel
        {
            get => loginWindowModel;
            set => SetProperty(ref loginWindowModel, value);
        }

        ISnackbarMessageQueue messageQueue;
        public ISnackbarMessageQueue LoginMessageQueue
        {
            get => messageQueue;
            set => SetProperty(ref messageQueue, value);
        }

        public LoginWindowViewModel(IContainerProvider containerProviderArg)
        {
            LoginWindowModel = new LoginWindowModel(containerProviderArg);
            LoginMessageQueue = containerProviderArg.Resolve<ISnackbarMessageQueue>();
        }
    }
}
