using Prism.Ioc;
using Prism.Modularity;

namespace HAMS.Frame.Kernel
{
    public class KernelModule : IModule
    {
        KernelLauncher kernelLauncher;

        public void OnInitialized(IContainerProvider containerProviderArg)
        {
            kernelLauncher = new KernelLauncher(containerProviderArg);
            kernelLauncher.Initialize();
        }

        public void RegisterTypes(IContainerRegistry containerRegistryArg)
        {
            KernelLauncher.RegisterServices(containerRegistryArg);
        }
    }
}
