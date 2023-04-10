using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Modularity;
using HAMS.Extension.Control.BasicConfiguration.Views;

namespace HAMS.Extension.Control.BasicConfiguration
{
    public class BasicConfigurationModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProviderArg)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistryArg)
        {
            containerRegistryArg.RegisterForNavigation<BasicConfigurationView>("BasicConfigurationModule");
        }
    }
}
