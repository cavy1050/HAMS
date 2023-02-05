using System;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Commands;
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

        public LoginWindowViewModel(IContainerProvider containerProviderArg)
        {
            LoginWindowModel = new LoginWindowModel(containerProviderArg);
            
        }
    }
}
