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
        ExtensionModuleKind dependencyModule;
        ExtensionModuleCatalog extensionModuleCatalog;
        List<ExtensionModuleKind> extensionModuleSettingHub;
        FrameModulePart requestTargetModule, requestSourceModule;
        EventBehaviourPart requestEventBehaviour;

        /// <summary>
        /// 静态扩展模块清单,缓存扩展模块信息
        /// </summary>
        public static List<ExtensionModuleKind> ExtensionModuleSetting { get; set; }

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

        private bool LoadModule(out string errorMessageArg)
        {
            bool ret = false;
            errorMessageArg = string.Empty;

            try
            {
                if (!moduleCatalog.Exists(extensionModule.Item))
                {
                    if (!string.IsNullOrEmpty(extensionModule.Note))
                    {
                        foreach (string dependencyModuleItem in extensionModule.Note.Split(','))
                        {
                            if (!moduleCatalog.Exists(dependencyModuleItem))
                                dependencyModule = new ExtensionModuleKind
                                {
                                    Code = ExtensionModuleSetting.FirstOrDefault(item => item.Item == dependencyModuleItem).Code,
                                    Name = ExtensionModuleSetting.FirstOrDefault(item => item.Item == dependencyModuleItem).Name,
                                    Item = dependencyModuleItem,
                                    Content = ExtensionModuleSetting.FirstOrDefault(item => item.Item == dependencyModuleItem).Content,
                                    Description = ExtensionModuleSetting.FirstOrDefault(item => item.Item == dependencyModuleItem).Description
                                };

                            extensionModuleCatalog = new ExtensionModuleCatalog(containerProvider, dependencyModule);
                            extensionModuleCatalog.Load();
                        }
                    }

                    extensionModuleCatalog = new ExtensionModuleCatalog(containerProvider, extensionModule);
                    extensionModuleCatalog.Load();
                    moduleManager.Run();
                }
            }
            catch (Exception ex)
            {
                errorMessageArg = ex.Message;
            }
            finally
            {
                ret = true;
            }

            return ret;
        }

        private bool Navigate(out string errorMessageArg)
        {
            bool ret = false;
            errorMessageArg = string.Empty;

            try
            {
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

            requestSourceModule = (FrameModulePart)Enum.Parse(typeof(FrameModulePart), requestObj.Value<string>("souc_mdl"));
            requestTargetModule = (FrameModulePart)Enum.Parse(typeof(FrameModulePart), requestObj.Value<string>("tagt_mdl"));
            requestEventBehaviour = (EventBehaviourPart)Enum.Parse(typeof(EventBehaviourPart), requestObj.Value<string>("svc_bhvr_type"));

            if (requestTargetModule == FrameModulePart.ServiceModule)
            {
                if (requestEventBehaviour == EventBehaviourPart.Initialization)
                {
                    if (ExtensionModuleSetting == null)
                    {
                        sqlSentence = "SELECT Code,Item,Name,Content,Description,SuperCode,SuperItem,Note,Rank,DefaultFlag,EnabledFlag FROM System_ExtensionModuleSetting WHERE EnabledFlag=True";
                        nativeBaseController.Query<ExtensionModuleKind>(sqlSentence, out extensionModuleSettingHub);
                        ExtensionModuleSetting = extensionModuleSettingHub;
                    }

                    eventJsonSentence = eventController.Response(EventPart.ExtensionModuleEvent, EventBehaviourPart.Initialization, FrameModulePart.ServiceModule,
                        requestSourceModule, new ExtensionModuleInitializationResponseContentKind { ExtensionModules = ExtensionModuleSetting.ToList() }, true, string.Empty);
                }
                else if (requestEventBehaviour == EventBehaviourPart.Activation)
                {
                    extensionModule = new ExtensionModuleKind
                    {
                        Code = requestContentObj.Value<string>("menu_code"),
                        Name = requestContentObj.Value<string>("menu_name"),
                        Item = requestContentObj.Value<string>("menu_mod_name"),
                        Content = requestContentObj.Value<string>("menu_mod_ref"),
                        Description = requestContentObj.Value<string>("menu_mod_type"),
                        Note = requestContentObj.Value<string>("menu_mod_dep")
                    };

                    if (LoadModule(out errorMessage))
                    {
                        if (Navigate(out errorMessage))
                            eventJsonSentence = eventController.Response(EventPart.ExtensionModuleEvent, EventBehaviourPart.Activation, FrameModulePart.ServiceModule,
                                requestSourceModule, new EmptyContentKind(), true, string.Empty);
                        else
                            eventJsonSentence = eventController.Response(EventPart.ExtensionModuleEvent, EventBehaviourPart.Activation, FrameModulePart.ServiceModule,
                                requestSourceModule, new EmptyContentKind(), false, errorMessage);
                    }
                    else
                    {
                        eventJsonSentence = eventController.Response(EventPart.ExtensionModuleEvent, EventBehaviourPart.Activation, FrameModulePart.ServiceModule,
                            requestSourceModule, new EmptyContentKind(), false, errorMessage);
                    }

                }
            }

            return eventJsonSentence;
        }
    }
}
