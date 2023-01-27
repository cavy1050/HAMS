using System;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using HAMS.Frame.Control.Login.Views;
using HAMS.Frame.Control.Login.ViewModels;

namespace HAMS.Frame.Control.Login
{
    public class LoginModule : IModule
    {
        IRegionManager regionManager;

        public void OnInitialized(IContainerProvider containerProviderArg)
        {
            regionManager = containerProviderArg.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("LoginHeaderRegion", typeof(LoginHeaderView));
            regionManager.RegisterViewWithRegion("LoginContentRegion", typeof(LoginContentView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistryArg)
        {
            containerRegistryArg.RegisterForNavigation<LoginContentView, LoginContentViewModel>();
            containerRegistryArg.RegisterForNavigation<LoginConfigView, LoginConfigViewModel>();
        }
    }
}
