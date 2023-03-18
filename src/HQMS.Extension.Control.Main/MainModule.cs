using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Modularity;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Services;
using HQMS.Extension.Control.Main.Views;

namespace HQMS.Extension.Control.Main
{
    public class MainModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProviderArg)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistryArg)
        {
            containerRegistryArg.RegisterSingleton<IConfigurator, Configurator>();

            containerRegistryArg.RegisterForNavigation<MainView>("MainModule");
            containerRegistryArg.RegisterForNavigation<DataMappingView>();
            containerRegistryArg.RegisterForNavigation<DataQueryingView>();
            containerRegistryArg.RegisterForNavigation<DataExportingView>();
        }
    }
}
