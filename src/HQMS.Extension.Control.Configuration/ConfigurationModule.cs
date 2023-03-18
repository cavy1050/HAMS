using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Modularity;
using HQMS.Extension.Control.Configuration.Views;

namespace HQMS.Extension.Control.Configuration
{
    public class ConfigurationModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProviderArg)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistryArg)
        {
            containerRegistryArg.RegisterForNavigation<ConfigurationView>("ConfigurationModule");
        }
    }
}
