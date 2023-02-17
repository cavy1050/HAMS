using System;
using Prism.Modularity;
using Prism.Ioc;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Service.Peripherals;

namespace HAMS.Frame.Service
{
    public class ServiceModule : IModule
    {
        ServiceLauncher serviceLauncher;

        public void OnInitialized(IContainerProvider containerProviderArg)
        {
            serviceLauncher = new ServiceLauncher(containerProviderArg);
            serviceLauncher.Initialize();
        }

        public void RegisterTypes(IContainerRegistry containerRegistryArg)
        {
            ServiceLauncher.RegisterServices(containerRegistryArg);
        }
    }
}
