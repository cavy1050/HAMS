using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Modularity;

namespace HQMS.Extension.Kernel
{
    public class ExtensionKernelModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProviderArg)
        {
            ExtensionKernelLauncher extensionKernelLauncher = new ExtensionKernelLauncher(containerProviderArg);
            extensionKernelLauncher.Initialize();
        }

        public void RegisterTypes(IContainerRegistry containerRegistryArg)
        {
            ExtensionKernelLauncher.RegisterServices(containerRegistryArg);
        }
    }
}
