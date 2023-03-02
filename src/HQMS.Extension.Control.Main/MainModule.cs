using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Modularity;
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
            containerRegistryArg.RegisterForNavigation<MainView>("MainModule");

            containerRegistryArg.RegisterForNavigation<DataMappingView>();
        }
    }
}
