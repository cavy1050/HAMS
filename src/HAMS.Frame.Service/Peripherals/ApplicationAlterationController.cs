using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Newtonsoft.Json.Linq;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Events;

namespace HAMS.Frame.Service.Peripherals
{
    public class ApplicationAlterationController : IServiceController
    {
        IEnvironmentMonitor environmentMonitor;
        IEventServiceController eventServiceController;

        string eventJsonSentence;      

        public ApplicationAlterationController(IContainerProvider containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
            eventServiceController= containerProviderArg.Resolve<IEventServiceController>();
        }

        public string Response(string requestServiceTextArg)
        {
            JObject requestObj = JObject.Parse(requestServiceTextArg);
            JObject requestContentObj = requestObj["svc_cont"].Value<JObject>();
            ControlTypePart requestControlType = (ControlTypePart)Enum.Parse(typeof(ControlTypePart), requestContentObj["app_ctl_type"].Value<string>());
            ActiveFlagPart requestActiveFlag = (ActiveFlagPart)Enum.Parse(typeof(ActiveFlagPart), requestContentObj["app_act_flag"].Value<string>());

            switch (requestControlType)
            {
                case ControlTypePart.LoginWindow:
                    if (!environmentMonitor.ApplicationControlSetting.ContainsKey(ControlTypePart.LoginWindow))
                        environmentMonitor.ApplicationControlSetting.Add(requestControlType, requestActiveFlag);
                    else
                        environmentMonitor.ApplicationControlSetting[ControlTypePart.LoginWindow] = requestActiveFlag;
                    break;

                case ControlTypePart.MainWindow:
                    if (!environmentMonitor.ApplicationControlSetting.ContainsKey(ControlTypePart.MainWindow))
                        environmentMonitor.ApplicationControlSetting.Add(requestControlType, requestActiveFlag);
                    else
                        environmentMonitor.ApplicationControlSetting[ControlTypePart.MainWindow] = requestActiveFlag;
                    break;

                case ControlTypePart.MainLeftDrawer:
                    if (!environmentMonitor.ApplicationControlSetting.ContainsKey(ControlTypePart.MainLeftDrawer))
                        environmentMonitor.ApplicationControlSetting.Add(requestControlType, requestActiveFlag);
                    else
                        environmentMonitor.ApplicationControlSetting[ControlTypePart.MainLeftDrawer] = requestActiveFlag;
                    break;
            }

            eventJsonSentence = eventServiceController.Response(EventServicePart.ApplicationAlterationService, FrameModulePart.ServiceModule,
                                    new List<FrameModulePart> { FrameModulePart.ApplictionModule },
                                        true, string.Empty,
                                        new ApplicationAlterationContentKind
                                        {
                                            ApplicationControlType = requestControlType,
                                            ApplicationActiveFlag = requestActiveFlag
                                        });

            return eventJsonSentence;
        }
    }
}
