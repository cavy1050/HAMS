using System;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using HAMS.Frame.Control.MainHeader.Views;

namespace HAMS.Frame.Control.MainHeader
{
    public class MainHeaderModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProviderArg)
        {
            IRegionManager regionManager = containerProviderArg.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("MainHeaderRegion", typeof(MainHeaderView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistryArg)
        {

        }
    }
}
