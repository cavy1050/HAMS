using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using NUlid;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Services;

namespace HAMS.Frame.Kernel.Events
{
    public class EventServiceController : IEventServiceController
    {
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeBaseController;
        ILogController servicEventLogController;

        string sqlSentence, eventServiceJsonText;
        List<SettingKind> serviceEventSettingHub;
        RequestServiceKind requestService;
        ResponseServiceKind responseService;

        public EventServiceController(IContainerProvider containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();

            servicEventLogController = environmentMonitor.LogSetting.GetContent(LogPart.ServicEvent);
            nativeBaseController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.Native);
        }

        public string Request(EventServicePart eventServiceArg, FrameModulePart sourceModuleArg, FrameModulePart targetModuleArg, IEventServiceContent eventServiceContentArg)
        {
            sqlSentence = "SELECT Code,Item,Name,Content,Description,Note,Rank,DefaultFlag,EnabledFlag FROM System_ServiceEventSetting WHERE Item='" + eventServiceArg.ToString() + "' AND EnabledFlag = True";
            if (nativeBaseController.Query<SettingKind>(sqlSentence, out serviceEventSettingHub))
            {
                requestService = new RequestServiceKind
                {
                    Item = serviceEventSettingHub.FirstOrDefault().Content,
                    Name = serviceEventSettingHub.FirstOrDefault().Item,
                    Type = EventServiceTypePart.Request,
                    Code = Ulid.NewUlid().ToString(),
                    RecordTime = DateTime.Now.ToString("G"),
                    SourceModuleName = sourceModuleArg,
                    TargetModuleName = targetModuleArg,
                    EventServiceContent = eventServiceContentArg
                };

                eventServiceJsonText = JsonConvert.SerializeObject(requestService);
                servicEventLogController.WriteDebug(eventServiceJsonText);
            }

            return eventServiceJsonText;
        }

        public string Response(EventServicePart eventServiceArg, FrameModulePart sourceModuleArg, IEnumerable<FrameModulePart> targetModuleArgs,
                                bool returnCodeArg, string returnMessageArg,
                                 IEventServiceContent eventServiceContentArg)
        {
            sqlSentence = "SELECT Code,Item,Name,Content,Description,Note,Rank,DefaultFlag,EnabledFlag FROM System_ServiceEventSetting WHERE Item='" + eventServiceArg.ToString() + "' AND EnabledFlag = True";
            if (nativeBaseController.Query<SettingKind>(sqlSentence, out serviceEventSettingHub))
            {
                responseService = new ResponseServiceKind
                {
                    Item = serviceEventSettingHub.FirstOrDefault().Content,
                    Name = serviceEventSettingHub.FirstOrDefault().Item,
                    Type = EventServiceTypePart.Response,
                    Code = Ulid.NewUlid().ToString(),
                    RecordTime = DateTime.Now.ToString("G"),
                    SourceModuleName = sourceModuleArg,
                    TargetModuleName = targetModuleArgs,
                    EventServiceContent = eventServiceContentArg,
                    ReturnCode = returnCodeArg == true ? "1" : "0",
                    ReturnMessage = returnMessageArg
                };

                eventServiceJsonText = JsonConvert.SerializeObject(responseService);
                servicEventLogController.WriteDebug(eventServiceJsonText);
            }

            return eventServiceJsonText;
        }
    }
}
