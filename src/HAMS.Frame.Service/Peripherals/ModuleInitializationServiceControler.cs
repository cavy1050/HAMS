using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Newtonsoft.Json.Linq;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Events;
using HAMS.Frame.Kernel.Services;

namespace HAMS.Frame.Service.Peripherals
{
    public class ModuleInitializationServiceControler : IServiceController
    {
        IEnvironmentMonitor environmentMonitor;
        IEventServiceController eventServiceController;
        IDataBaseController nativeBaseController;

        string sqlSentence, eventJsonSentence;
        List<ExtensionModuleKind> extensionModuleHub;

        public ModuleInitializationServiceControler(IContainerProvider containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
            eventServiceController = containerProviderArg.Resolve<IEventServiceController>();
            nativeBaseController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.Native);
        }

        public string Response(string requestServiceTextArg)
        {
            sqlSentence = "SELECT Code,Item,Name,Content,Description,SuperCode,SuperItem,Note,Rank,DefaultFlag,EnabledFlag FROM System_ExtensionModuleSetting WHERE EnabledFlag=True";
            nativeBaseController.Query<ExtensionModuleKind>(sqlSentence, out extensionModuleHub);

            eventJsonSentence = eventServiceController.Response(EventServicePart.ModuleInitializationService, FrameModulePart.ServiceModule,
                FrameModulePart.MainLeftDrawerModule, true, string.Empty,
                    new ExtensionModuleInitializationResponseContentKind { ExtensionModules = extensionModuleHub.ToList() });

            return eventJsonSentence;
        }
    }
}
