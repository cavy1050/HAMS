using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using NUlid;
using Newtonsoft.Json;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Services;

namespace HAMS.Frame.Kernel.Events
{
    public class EventController : IEventController
    {
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeBaseController;
        ILogController servicEventLogController;

        string sqlSentence, eventJsonSentence;
        List<SettingKind> serviceEventSettingHub;
        RequestEventKind requestEvent;
        ResponseEventKind responseEvent;

        public EventController(IContainerProvider containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();

            servicEventLogController = environmentMonitor.LogSetting.GetContent(LogPart.ServicEvent);
            nativeBaseController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.Native);
        }

        public string Request(EventPart eventArg, EventBehaviourPart eventBehaviourPartArg, FrameModulePart sourceModuleArg, FrameModulePart targetModuleArg, IEventContent eventContentArg)
        {
            sqlSentence = "SELECT Code,Item,Name,Content,Description,Note,Rank,DefaultFlag,EnabledFlag FROM System_EventSetting WHERE Item='" + eventArg.ToString() + "' AND EnabledFlag = True";

            if (nativeBaseController.Query<SettingKind>(sqlSentence, out serviceEventSettingHub))
            {
                requestEvent = new RequestEventKind
                {
                    Content = serviceEventSettingHub.FirstOrDefault().Content,
                    Name = serviceEventSettingHub.FirstOrDefault().Item,
                    Type = Convert.ToInt32(EventTypePart.Request).ToString(),
                    Behaviour = Convert.ToInt32(eventBehaviourPartArg).ToString(),
                    Code = Ulid.NewUlid().ToString(),
                    RecordTime = DateTime.Now.ToString("G"),
                    SourceModule = Convert.ToInt32(sourceModuleArg).ToString(),
                    TargetModule = Convert.ToInt32(targetModuleArg).ToString(),
                    EventContent = eventContentArg
                };

                eventJsonSentence = JsonConvert.SerializeObject(requestEvent);
                servicEventLogController.WriteDebug(eventJsonSentence);
            }

            return eventJsonSentence;
        }

        public string Response(EventPart eventArg, EventBehaviourPart eventBehaviourPartArg, FrameModulePart sourceModuleArg, FrameModulePart targetModuleArg, IEventContent eventContentArg, bool returnCodeArg, string returnMessageArgs)
        {
            sqlSentence = "SELECT Code,Item,Name,Content,Description,Note,Rank,DefaultFlag,EnabledFlag FROM System_EventSetting WHERE Item='" + eventArg.ToString() + "' AND EnabledFlag = True";

            if (nativeBaseController.Query<SettingKind>(sqlSentence, out serviceEventSettingHub))
            {
                responseEvent = new ResponseEventKind
                {
                    Content = serviceEventSettingHub.FirstOrDefault().Content,
                    Name = serviceEventSettingHub.FirstOrDefault().Item,
                    Type = Convert.ToInt32(EventTypePart.Response).ToString(),
                    Behaviour = Convert.ToInt32(eventBehaviourPartArg).ToString(),
                    Code = Ulid.NewUlid().ToString(),
                    RecordTime = DateTime.Now.ToString("G"),
                    SourceModule = Convert.ToInt32(sourceModuleArg).ToString(),
                    TargetModule = Convert.ToInt32(targetModuleArg).ToString(),
                    EventContent = eventContentArg,
                    ReturnResult = returnCodeArg == true ? 1 : 0,
                    ReturnMessage = returnMessageArgs
                };

                eventJsonSentence = JsonConvert.SerializeObject(responseEvent);
                servicEventLogController.WriteDebug(eventJsonSentence);
            }

            return eventJsonSentence;
        }
    }
}
