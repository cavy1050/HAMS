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
    public class ModuleActivationServiceController : IServiceController
    {
        IContainerProvider containerProvider;
        IModuleManager moduleManager;
        IRegionManager regionManager;
        IEventServiceController eventServiceController;

        ExtensionModuleKind extensionModule;
        ExtensionModuleCatalog extensionModuleCatalog;

        string eventJsonSentence;

        public ModuleActivationServiceController(IContainerProvider containerProviderArg)
        {
            containerProvider = containerProviderArg;
            moduleManager = containerProviderArg.Resolve<IModuleManager>();
            regionManager = containerProviderArg.Resolve<IRegionManager>();
            eventServiceController = containerProviderArg.Resolve<IEventServiceController>();
        }

        private void Analyze(string requestServiceTextArg)
        {
            JObject requestObj = JObject.Parse(requestServiceTextArg);
            JObject requestContentObj = requestObj.Value<JObject>("svc_cont");

            extensionModule = new ExtensionModuleKind
            {
                Code = requestContentObj.Value<string>("menu_code"),
                Name = requestContentObj.Value<string>("menu_name"),
                Item = requestContentObj.Value<string>("menu_mod_name"),
                Content = requestContentObj.Value<string>("menu_mod_ref"),
                Description = requestContentObj.Value<string>("menu_mod_type"),
            };
        }

        private bool Navigate(out string errorMessageArg)
        {
            bool ret = false;
            errorMessageArg = string.Empty;

            try
            {
                extensionModuleCatalog = new ExtensionModuleCatalog(containerProvider, extensionModule);
                extensionModuleCatalog.Load();
                moduleManager.Run();

                regionManager.RequestNavigate("MainContentRegion", extensionModule.Item);

                ret = true;
            }
            catch (ModuleTypeLoadingException ex)
            {
                errorMessageArg = ex.Message;
            }

            return ret;
        }

        public string Response(string requestServiceTextArg)
        {
            string errorMessage;

            Analyze(requestServiceTextArg);

            if (Navigate(out errorMessage))
                eventJsonSentence = eventServiceController.Response(EventServicePart.ModuleActivationService, FrameModulePart.ServiceModule,
                    FrameModulePart.MainLeftDrawerModule, true, string.Empty, new EmptyContentKind());
            else
                eventJsonSentence = eventServiceController.Response(EventServicePart.ModuleActivationService, FrameModulePart.ServiceModule,
                    FrameModulePart.MainLeftDrawerModule, false, errorMessage, new EmptyContentKind());

            return eventJsonSentence;
        }
    }
}
