using System;
using Prism.Ioc;
using log4net.Core;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HAMS.Frame.Kernel.Test
{
    [TestClass]
    public class KernelModuleTest
    {
        IContainerRegistry containerRegistry;
        IContainerProvider containerProvider;

        [TestInitialize]
        public void Initialize(IContainerRegistry containerRegistryArg,IContainerProvider containerProviderArg)
        {
            containerRegistry = containerRegistryArg;
            containerProvider = containerProviderArg;
        }

        [TestMethod]
        public void TestKernelModuleInitialize()
        {
            KernelModule kernelModule = new KernelModule();
            kernelModule.RegisterTypes(containerRegistry);
            kernelModule.OnInitialized(containerProvider);

            KernelLauncher kernelLauncher = new KernelLauncher(containerProvider);
            kernelLauncher.Initialize();

            IEnvironmentMonitor environmentMonitor = containerProvider.Resolve<IEnvironmentMonitor>();
            string logFileCataloguePath = environmentMonitor.PathSetting.GetContent(PathPart.LogFileCatalogue);
            string appPath = AppDomain.CurrentDomain.BaseDirectory + "Log\\";

            Assert.AreEqual(logFileCataloguePath, appPath);
        }
    }
}
