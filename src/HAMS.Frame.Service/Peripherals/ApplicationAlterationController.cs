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

        FrameModulePart sourceModule;
        ControlTypePart requestControlType;
        ActiveFlagPart requestActiveFlag;
        List<FrameModulePart> targetModules;

        string eventJsonSentence;

        public ApplicationAlterationController(IContainerProvider containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
            eventServiceController = containerProviderArg.Resolve<IEventServiceController>();
        }

        private void Analyze(string requestServiceTextArg)
        {
            JObject requestObj = JObject.Parse(requestServiceTextArg);
            JObject requestContentObj = requestObj.Value<JObject>("svc_cont");

            sourceModule = (FrameModulePart)Enum.Parse(typeof(FrameModulePart), requestObj.Value<string>("souc_mod_name"));
            requestControlType = (ControlTypePart)Enum.Parse(typeof(ControlTypePart), requestContentObj.Value<string>("app_ctl_type"));
            requestActiveFlag = (ActiveFlagPart)Enum.Parse(typeof(ActiveFlagPart), requestContentObj.Value<string>("app_act_flag"));
        }

        private void GenerateTargetModules()
        {
            switch (sourceModule)
            {
                case FrameModulePart.ServiceModule:
                    targetModules = new List<FrameModulePart> { FrameModulePart.All };
                    break;
                case FrameModulePart.LoginModule:
                    targetModules = new List<FrameModulePart> { FrameModulePart.ApplictionModule };
                    break;
                case FrameModulePart.MainHeaderModule:
                    targetModules = new List<FrameModulePart> { FrameModulePart.ApplictionModule };
                    break;
                case FrameModulePart.ApplictionModule:
                    targetModules= targetModules = new List<FrameModulePart> { FrameModulePart.MainHeaderModule };
                    break;
                default:
                    targetModules = new List<FrameModulePart> { sourceModule };
                    break;
            }
        }

        private void SynchronizeServices()
        {
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
        }

        public string Response(string requestServiceTextArg)
        {
            Analyze(requestServiceTextArg);
            GenerateTargetModules();
            SynchronizeServices();

            eventJsonSentence = eventServiceController.Response(EventServicePart.ApplicationAlterationService, FrameModulePart.ServiceModule, targetModules,
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
