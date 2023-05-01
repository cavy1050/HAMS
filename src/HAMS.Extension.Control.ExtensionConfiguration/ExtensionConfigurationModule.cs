using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Modularity;
using HAMS.Extension.Control.ExtensionConfiguration.Views;

namespace HAMS.Extension.Control.ExtensionConfiguration
{
    public class ExtensionConfigurationModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProviderArg)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistryArg)
        {
            containerRegistryArg.RegisterForNavigation<ExtensionConfigurationView>("ExtensionConfigurationModule");
        }
    }
}
