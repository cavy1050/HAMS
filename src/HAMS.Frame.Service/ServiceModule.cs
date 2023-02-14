using System;
using Prism.Modularity;
using Prism.Ioc;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Service.Peripherals;

namespace HAMS.Frame.Service
{
    public class ServiceModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProviderArg)
        {
            ServiceLauncher serviceLauncher = new ServiceLauncher(containerProviderArg);
            serviceLauncher.Initialize();
        }

        public void RegisterTypes(IContainerRegistry containerRegistryArg)
        {
            containerRegistryArg.Register<IServiceController, ApplicationAlterationController>(EventServicePart.ApplicationAlterationService.ToString());
            containerRegistryArg.Register<IServiceController, AccountVerificationControler>(EventServicePart.AccountVerificationService.ToString());

            containerRegistryArg.Register<IServiceController, ExtensionModuleInitializationServiceControler>(EventServicePart.ExtensionModuleInitializationService.ToString());
        }
    }
}
