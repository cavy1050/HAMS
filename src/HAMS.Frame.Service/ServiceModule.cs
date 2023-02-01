using System;
using Prism.Modularity;
using Prism.Ioc;

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

        }
    }
}
