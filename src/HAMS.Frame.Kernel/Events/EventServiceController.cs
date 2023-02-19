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

        string sqlSentence, eventJsonSentence;
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
                    Content = serviceEventSettingHub.FirstOrDefault().Content,
                    Name = serviceEventSettingHub.FirstOrDefault().Item,
                    Type = EventServiceTypePart.Request,
                    Code = Ulid.NewUlid().ToString(),
                    RecordTime = DateTime.Now.ToString("G"),
                    SourceModuleName = sourceModuleArg,
                    TargetModuleName = targetModuleArg,
                    EventServiceContent = eventServiceContentArg
                };

                eventJsonSentence = JsonConvert.SerializeObject(requestService);
                servicEventLogController.WriteDebug(eventJsonSentence);
            }

            return eventJsonSentence;
        }

        public string Response(EventServicePart eventServiceArg, FrameModulePart sourceModuleArg, FrameModulePart targetModuleArg,
                                bool returnResultArg, string returnMessageArg,
                                 IEventServiceContent eventServiceContentArg)
        {
            sqlSentence = "SELECT Code,Item,Name,Content,Description,Note,Rank,DefaultFlag,EnabledFlag FROM System_ServiceEventSetting WHERE Item='" + eventServiceArg.ToString() + "' AND EnabledFlag = True";

            if (nativeBaseController.Query<SettingKind>(sqlSentence, out serviceEventSettingHub))
            {
                responseService = new ResponseServiceKind
                {
                    Content = serviceEventSettingHub.FirstOrDefault().Content,
                    Name = serviceEventSettingHub.FirstOrDefault().Item,
                    Type = EventServiceTypePart.Response,
                    Code = Ulid.NewUlid().ToString(),
                    RecordTime = DateTime.Now.ToString("G"),
                    SourceModuleName = sourceModuleArg,
                    TargetModuleName = targetModuleArg,
                    EventServiceContent = eventServiceContentArg,
                    ReturnResult = returnResultArg,
                    ReturnMessage = returnMessageArg
                };

                eventJsonSentence = JsonConvert.SerializeObject(responseService);
                servicEventLogController.WriteDebug(eventJsonSentence);
            }

            return eventJsonSentence;
        }
    }
}
