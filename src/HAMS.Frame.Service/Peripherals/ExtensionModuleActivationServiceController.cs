using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Newtonsoft.Json.Linq;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Events;
using HAMS.Frame.Service.Extension;

namespace HAMS.Frame.Service.Peripherals
{
    public class ExtensionModuleActivationServiceController : IServiceController
    {
        IContainerProvider containerProvider;
        IModuleManager moduleManager;
        IRegionManager regionManager;
        IEventServiceController eventServiceController;

        ExtensionModuleKind extensionModule;
        ExtensionModuleCatalog extensionModuleCatalog;

        string eventJsonSentence;

        public ExtensionModuleActivationServiceController(IContainerProvider containerProviderArg)
        {
            containerProvider = containerProviderArg;
            moduleManager = containerProviderArg.Resolve<IModuleManager>();
            regionManager = containerProviderArg.Resolve<IRegionManager>();
            eventServiceController= containerProviderArg.Resolve<IEventServiceController>();
        }

        public string Response(string requestContentTextArg)
        {
            JObject requestContextObj = JObject.Parse(requestContentTextArg);

            extensionModule = new ExtensionModuleKind
            {
                Code = requestContextObj.Value<string>("menu_code"),
                Name = requestContextObj.Value<string>("menu_name"),
                Item = requestContextObj.Value<string>("menu_mod_name"),
                Content = requestContextObj.Value<string>("menu_mod_ref"),
                Description = requestContextObj.Value<string>("menu_mod_type"),
            };

            extensionModuleCatalog = new ExtensionModuleCatalog(containerProvider, extensionModule);
            extensionModuleCatalog.Load();
            moduleManager.Run();

            regionManager.RequestNavigate("MainContentRegion", requestContextObj.Value<string>("menu_name"));

            eventJsonSentence = eventServiceController.Response(EventServicePart.ModuleActivationService, FrameModulePart.ServiceModule,
                new List<FrameModulePart> { FrameModulePart.MainLeftDrawerModule },
                    true, string.Empty, new EmptyContentKind());

            return eventJsonSentence;
        }
    }
}
