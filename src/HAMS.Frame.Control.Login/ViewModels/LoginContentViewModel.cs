using System;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Commands;
using HAMS.Frame.Control.Login.Models;

namespace HAMS.Frame.Control.Login.ViewModels
{
    public class LoginContentViewModel : BindableBase
    {
        LoginContentModel loginContentModel;
        public LoginContentModel LoginContentModel
        {
            get => loginContentModel;
            set => SetProperty(ref loginContentModel, value);
        }

        public DelegateCommand LoginContentLoadedCommand { get; private set; }
        public DelegateCommand<object> LoginCommand { get; private set; }

        public LoginContentViewModel(IContainerProvider containerProviderArg)
        {
            LoginContentModel = new LoginContentModel(containerProviderArg);
            LoginContentLoadedCommand = new DelegateCommand(OnLoginContentLoaded);
            LoginCommand = new DelegateCommand<object>(OnLogin);
        }

        private void OnLoginContentLoaded()
        {
            LoginContentModel.ShowInitializeServiceMessage();
        }

        private void OnLogin(object currentWindow)
        {
            LoginContentModel.Login(currentWindow);
        }
    }
}
