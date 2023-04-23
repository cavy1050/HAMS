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
using HAMS.Frame.Kernel.Services;
using HAMS.Frame.Service.Extension;

namespace HAMS.Frame.Service.Peripherals
{
    public class ExtensionModuleController : IEventResponseController
    {
        IContainerProvider containerProvider;
        IModuleCatalog moduleCatalog;
        IModuleManager moduleManager;
        IRegionManager regionManager;
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeBaseController;
        IEventController eventController;

        string sqlSentence, eventJsonSentence;
        JObject requestObj, requestContentObj;
        ExtensionModuleKind extensionModule;
        ExtensionModuleCatalog extensionModuleCatalog;
        List<ExtensionModuleKind> extensionModuleHub;

        public ExtensionModuleController(IContainerProvider containerProviderArg)
        {
            containerProvider = containerProviderArg;
            moduleCatalog = containerProviderArg.Resolve<IModuleCatalog>();
            moduleManager = containerProviderArg.Resolve<IModuleManager>();
            regionManager = containerProviderArg.Resolve<IRegionManager>();
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
            nativeBaseController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.Native);
            eventController = containerProviderArg.Resolve<IEventController>();
        }

        private bool Navigate(out string errorMessageArg)
        {
            bool ret = false;
            errorMessageArg = string.Empty;

            try
            {
                if (!moduleCatalog.Exists(extensionModule.Item))
                {
                    extensionModuleCatalog = new ExtensionModuleCatalog(containerProvider, extensionModule);
                    extensionModuleCatalog.Load();
                    moduleManager.Run();
                }

                regionManager.RequestNavigate("MainContentRegion", extensionModule.Item);

                ret = true;
            }
            catch (Exception ex)
            {
                errorMessageArg = ex.Message;
            }

            return ret;
        }

        public string Response(string requestServiceTextArg)
        {
            string errorMessage;

            requestObj = JObject.Parse(requestServiceTextArg);
            requestContentObj = requestObj.Value<JObject>("svc_cont");
            FrameModulePart targetModule = (FrameModulePart)Enum.Parse(typeof(FrameModulePart), requestObj.Value<string>("tagt_mdl"));
            EventBehaviourPart eventBehaviour = (EventBehaviourPart)Enum.Parse(typeof(EventBehaviourPart), requestObj.Value<string>("svc_bhvr_type"));

            if (targetModule == FrameModulePart.ServiceModule)
            {
                if (eventBehaviour == EventBehaviourPart.Initialization)
                {
                    sqlSentence = "SELECT Code,Item,Name,Content,Description,SuperCode,SuperItem,Note,Rank,DefaultFlag,EnabledFlag FROM System_ExtensionModuleSetting WHERE EnabledFlag=True";
                    nativeBaseController.Query<ExtensionModuleKind>(sqlSentence, out extensionModuleHub);

                    eventJsonSentence = eventController.Response(EventPart.ExtensionModuleEvent, EventBehaviourPart.Initialization, FrameModulePart.ServiceModule,
                        FrameModulePart.MainLeftDrawerModule, new ExtensionModuleInitializationResponseContentKind { ExtensionModules = extensionModuleHub.ToList() }, true, string.Empty);
                }
                else if (eventBehaviour == EventBehaviourPart.Activation)
                {
                    extensionModule = new ExtensionModuleKind
                    {
                        Code = requestContentObj.Value<string>("menu_code"),
                        Name = requestContentObj.Value<string>("menu_name"),
                        Item = requestContentObj.Value<string>("menu_mod_name"),
                        Content = requestContentObj.Value<string>("menu_mod_ref"),
                        Description = requestContentObj.Value<string>("menu_mod_type"),
                    };

                    if (Navigate(out errorMessage))
                        eventJsonSentence = eventController.Response(EventPart.ExtensionModuleEvent, EventBehaviourPart.Activation, FrameModulePart.ServiceModule,
                            FrameModulePart.MainLeftDrawerModule, new EmptyContentKind(), true, string.Empty);
                    else
                        eventJsonSentence = eventController.Response(EventPart.ExtensionModuleEvent, EventBehaviourPart.Activation, FrameModulePart.ServiceModule,
                            FrameModulePart.MainLeftDrawerModule, new EmptyContentKind(), false, errorMessage);
                }
            }

            return eventJsonSentence;
        }
    }
}
