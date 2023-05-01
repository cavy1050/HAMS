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
    public class ApplicationController : IEventResponseController
    {
        IEnvironmentMonitor environmentMonitor;
        IEventController eventController;

        JObject requestObj, requestContentObj;

        FrameModulePart sourceModule, targetModule;
        EventBehaviourPart targetEventBehaviour;
        ControlTypePart requestControlType;
        ActiveFlagPart requestActiveFlag;

        string eventJsonSentence;

        public ApplicationController(IContainerProvider containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
            eventController = containerProviderArg.Resolve<IEventController>();
        }

        private void Analyze(string requestServiceTextArg)
        {
            requestObj = JObject.Parse(requestServiceTextArg);
            requestContentObj = requestObj.Value<JObject>("svc_cont");

            sourceModule = (FrameModulePart)Enum.Parse(typeof(FrameModulePart), requestObj.Value<string>("souc_mdl"));
            targetEventBehaviour = (EventBehaviourPart)Enum.Parse(typeof(EventBehaviourPart), requestObj.Value<string>("svc_bhvr_type"));
            requestControlType = (ControlTypePart)Enum.Parse(typeof(ControlTypePart), requestContentObj.Value<string>("app_ctl_type"));
            requestActiveFlag = (ActiveFlagPart)Enum.Parse(typeof(ActiveFlagPart), requestContentObj.Value<string>("app_act_flag"));
        }

        private void GenerateTargetModules()
        {
            switch (sourceModule)
            {
                case FrameModulePart.ServiceModule:
                    targetModule = FrameModulePart.All;
                    break;
                case FrameModulePart.LoginModule:
                    targetModule = FrameModulePart.ApplictionModule;
                    break;
                case FrameModulePart.MainHeaderModule:
                    targetModule = FrameModulePart.ApplictionModule;
                    break;
                case FrameModulePart.ApplictionModule:
                    targetModule = FrameModulePart.MainHeaderModule;
                    break;
                default:
                    targetModule = sourceModule;
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

            eventJsonSentence = eventController.Response(EventPart.ApplicationEvent, targetEventBehaviour, FrameModulePart.ServiceModule, targetModule,
                                        new ApplicationContentKind
                                        {
                                            ApplicationControlType = Convert.ToInt32(requestControlType).ToString(),
                                            ApplicationActiveFlag = Convert.ToInt32(requestActiveFlag).ToString()
                                        }, true, string.Empty);

            return eventJsonSentence;
        }
    }
}
