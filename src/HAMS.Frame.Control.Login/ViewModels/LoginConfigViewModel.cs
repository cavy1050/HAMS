using System;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Commands;
using HAMS.Frame.Control.Login.Models;

namespace HAMS.Frame.Control.Login.ViewModels
{
    public class LoginConfigViewModel : BindableBase
    {
        LoginConfigModel loginConfigModel;
        public LoginConfigModel LoginConfigModel
        {
            get => loginConfigModel;
            set => SetProperty(ref loginConfigModel, value);
        }

        public DelegateCommand LoginConfigLoadedCommand { get; private set; }

        public LoginConfigViewModel(IContainerProvider containerProviderArg)
        {
            LoginConfigModel = new LoginConfigModel(containerProviderArg);
            LoginConfigLoadedCommand = new DelegateCommand(OnLoginConfigLoaded);
        }

        private void OnLoginConfigLoaded()
        {
            LoginConfigModel.LoadVersionData();
        }
    }
}
