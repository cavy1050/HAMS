using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Modularity;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Services;

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

        public void RegisterTypes(IContainerRegistry containerRegistryArgs)
        {
            containerRegistryArgs.RegisterSingleton<IEnvironmentMonitor, EnvironmentMonitor>();

            containerRegistryArgs.Register<IDataBaseController, NativeBaseController>(DataBasePart.Native.ToString());
            containerRegistryArgs.Register<ISecurityController, SecurityController>();

            containerRegistryArgs.Register<IManager<PathPart>, PathManager>();
            containerRegistryArgs.Register<IManager<DataBasePart>, DataBaseManager>();
            containerRegistryArgs.Register<IManager<LogPart>, LogManager>();


        }
    }
}
