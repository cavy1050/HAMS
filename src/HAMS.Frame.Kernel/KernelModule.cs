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

        public void RegisterTypes(IContainerRegistry containerRegistryArg)
        {
            containerRegistryArg.RegisterSingleton<IEnvironmentMonitor, EnvironmentMonitor>();

            containerRegistryArg.Register<IDataBaseController, NativeBaseController>(DataBasePart.Native.ToString());
            containerRegistryArg.Register<IDataBaseController, BAGLDBBaseController>(DataBasePart.BAGLDB.ToString());
            containerRegistryArg.Register<ISecurityController, SecurityController>();

            containerRegistryArg.Register<ILogController, ApplicationLogController>(LogPart.Application.ToString());
            containerRegistryArg.Register<ILogController, DataBaseLogController>(LogPart.DataBase.ToString());
            containerRegistryArg.Register<ILogController, ServiceEventLogController>(LogPart.ServicEvent.ToString());

            containerRegistryArg.Register<IManager<SeverityLevelPart>, SeverityManager>();
            containerRegistryArg.Register<IManager<PathPart>, PathManager>();
            containerRegistryArg.Register<IManager<DataBasePart>, DataBaseManager>();
            containerRegistryArg.Register<IManager<LogPart>, LogManager>();
        }
    }
}
