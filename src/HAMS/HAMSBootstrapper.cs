using System;
using System.Windows;
using Prism.Ioc;
using Prism.Unity;
using Prism.Modularity;
using MaterialDesignThemes.Wpf;
using HAMS.Views;
using HAMS.Frame.Kernel;

namespace HAMS
{
    public class HAMSBootstrapper:PrismBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<LoginWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistryArgs)
        {
            containerRegistryArgs.RegisterSingleton<ISnackbarMessageQueue, SnackbarMessageQueue>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalogArgs)
        {
            Type kernelModuleType = typeof(KernelModule);

            moduleCatalogArgs.AddModule(new ModuleInfo()
            {
                ModuleName = kernelModuleType.Name,
                ModuleType = kernelModuleType.AssemblyQualifiedName,
                InitializationMode = InitializationMode.WhenAvailable
            });
        }
    }
}
