using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Modularity;
using IDMS.Extension.Control.InfluenzaSurveillance.Views;

namespace IDMS.Extension.Control.InfluenzaSurveillance
{
    public class InfluenzaSurveillanceModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProviderArg)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistryArg)
        {
            containerRegistryArg.RegisterForNavigation<InfluenzaSurveillanceView>("InfluenzaSurveillanceModule");
        }
    }
}
