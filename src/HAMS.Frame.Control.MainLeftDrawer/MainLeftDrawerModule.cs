using System;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using HAMS.Frame.Control.MainLeftDrawer.Views;

namespace HAMS.Frame.Control.MainLeftDrawer
{
    public class MainLeftDrawerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProviderArg)
        {
            IRegionManager regionManager = containerProviderArg.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("MainLeftDrawerRegion", typeof(MainLeftDrawerView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistryArg)
        {

        }
    }
}
