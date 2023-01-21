using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using log4net.Core;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Extensions;

namespace HAMS.Frame.Kernel.Services
{
    /// <summary>
    /// 程序日志管理,详细信息请查阅 <see cref="IManager<TEnum>"/>
    /// </summary>
    public class LogManager : IManager<LogPart>
    {
        string sqlSentence, logFileCatalogue;
        List<BaseKind> costomLogSettingHub;

        IContainerProvider containerProvider;
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeBaseController;
        ILogController errorLogController;
        ILogController dataBaseLogController;
        ILogController servicEventLogController;

        /// <summary>
        /// 程序错误日志文件路径
        /// </summary>
        public string ErrorLogFilePath { get; set; }

        /// <summary>
        /// 数据库日志文件路径
        /// </summary>
        public string DataBaseLogFilePath { get; set; }

        /// <summary>
        /// 服务事件日志文件目录
        /// </summary>
        public string ServicEventLogFilePath { get; set; }

        /// <summary>
        /// 全局日志启用设置
        /// </summary>
        public bool GlobalLogEnabledFilter { get; set; }

        /// <summary>
        /// 全局日志级别
        /// </summary>
        public Level GlobalLogLevel { get; set; }

        public LogManager(IContainerProvider containerProviderArg)
        {
            containerProvider = containerProviderArg;
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
        }

        public void DeInit(LogPart logPartArg)
        {
            logFileCatalogue = environmentMonitor.PathSetting.GetContent(PathPart.LogFileCatalogue);

            switch (logPartArg)
            {
                case LogPart.Global:
                    GlobalLogEnabledFilter = true;
                    GlobalLogLevel = Level.Debug;
                    break;

                case LogPart.Error:
                    ErrorLogFilePath = logFileCatalogue + "Error_" + DateTime.Now.ToString("d") + ".txt";
                    break;

                case LogPart.DataBase:
                    DataBaseLogFilePath = logFileCatalogue + "DataBase_" + DateTime.Now.ToString("d") + ".txt";
                    break;

                case LogPart.ServicEvent:
                    ServicEventLogFilePath = logFileCatalogue + "ServicEvent_" + DateTime.Now.ToString("d") + ".txt";
                    break;

                case LogPart.All:
                    DeInit(LogPart.Global);
                    DeInit(LogPart.Error);
                    DeInit(LogPart.DataBase);
                    DeInit(LogPart.ServicEvent);
                    break;
            }
        }

        public void Init(LogPart logPartArg)
        {
            logFileCatalogue = environmentMonitor.PathSetting.GetContent(PathPart.LogFileCatalogue);

            nativeBaseController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.Native);
            sqlSentence = "SELECT Code,Item,Name,Content,Description,Note,Rank,Flag FROM System_LogSetting WHERE Code = '01GPT3T83953EVANVTJ0ATFAK5'";
            nativeBaseController.QueryNoLog<BaseKind>(sqlSentence, out costomLogSettingHub);

            switch (logPartArg)
            {
                case LogPart.Global:
                    GlobalLogEnabledFilter = costomLogSettingHub.FirstOrDefault(x=>x.Code== "01GPT3T83953EVANVTJ0ATFAK5").Flag;
                    break;

                case LogPart.Error:
                    ErrorLogFilePath = logFileCatalogue + "Error_" + DateTime.Now.ToString("d") + ".txt";
                    break;

                case LogPart.DataBase:
                    DataBaseLogFilePath = logFileCatalogue + "DataBase_" + DateTime.Now.ToString("d") + ".txt";
                    break;

                case LogPart.ServicEvent:
                    ServicEventLogFilePath = logFileCatalogue + "ServicEvent_" + DateTime.Now.ToString("d") + ".txt";
                    break;

                case LogPart.All:
                    Init(LogPart.Global);
                    Init(LogPart.Error);
                    Init(LogPart.DataBase);
                    Init(LogPart.ServicEvent);
                    break;
            }
        }

        public void Load(LogPart logPartArg)
        {
            switch (logPartArg)
            {
                case LogPart.Global:
                    if (!environmentMonitor.LogSetting.Exists(x => x.Code == "01GPT3T83953EVANVTJ0ATFAK5"))
                        environmentMonitor.LogSetting.Add(new LogKind
                        {
                            Code = "01GPT3T83953EVANVTJ0ATFAK5",
                            Item = LogPart.Global.ToString(),
                            Name = EnumExtension.GetDescription(LogPart.Global),
                            Note = GlobalLogLevel.ToString(),
                            Rank = Convert.ToInt32(LogPart.Global),
                            Flag = GlobalLogEnabledFilter
                        });
                    else
                    {
                        environmentMonitor.LogSetting[LogPart.Global].Flag = GlobalLogEnabledFilter;
                        environmentMonitor.LogSetting[LogPart.Global].Note = GlobalLogLevel.ToString();
                    }
                    break;

                case LogPart.Error:
                    if (!environmentMonitor.LogSetting.Exists(x => x.Code == "01GPT3T839Q7VP6GAGQT12PBXK"))
                        environmentMonitor.LogSetting.Add(new LogKind
                        {
                            Code = "01GPT3T839Q7VP6GAGQT12PBXK",
                            Item = LogPart.Error.ToString(),
                            Name = EnumExtension.GetDescription(LogPart.Error),
                            Content = ErrorLogFilePath,
                            Rank = Convert.ToInt32(LogPart.Error),
                            Flag = true
                        });
                    else
                        environmentMonitor.LogSetting[LogPart.Error].Content = ErrorLogFilePath;

                    errorLogController = containerProvider.Resolve<ILogController>(LogPart.Error.ToString());
                    environmentMonitor.LogSetting[LogPart.Error].LogController = null;
                    environmentMonitor.LogSetting[LogPart.Error].LogController = errorLogController;

                    break;

                case LogPart.DataBase:
                    if (!environmentMonitor.LogSetting.Exists(x => x.Code == "01GPT3T839BDKG6EA03W22MX0K"))
                        environmentMonitor.LogSetting.Add(new LogKind
                        {
                            Code = "01GPT3T839BDKG6EA03W22MX0K",
                            Item = LogPart.DataBase.ToString(),
                            Name = EnumExtension.GetDescription(LogPart.DataBase),
                            Content = DataBaseLogFilePath,
                            Rank = Convert.ToInt32(LogPart.DataBase),
                            Flag = true
                        });
                    else
                        environmentMonitor.LogSetting[LogPart.DataBase].Content = DataBaseLogFilePath;

                    dataBaseLogController = containerProvider.Resolve<ILogController>(LogPart.DataBase.ToString());
                    environmentMonitor.LogSetting[LogPart.DataBase].LogController = dataBaseLogController;

                    break;

                case LogPart.ServicEvent:
                    if (!environmentMonitor.LogSetting.Exists(x => x.Code == "01GPT3T839QKWK6A7GHVV2PH8E"))
                        environmentMonitor.LogSetting.Add(new LogKind
                        {
                            Code = "01GPT3T839QKWK6A7GHVV2PH8E",
                            Item = LogPart.ServicEvent.ToString(),
                            Name = EnumExtension.GetDescription(LogPart.ServicEvent),
                            Content = ServicEventLogFilePath,
                            Rank = Convert.ToInt32(LogPart.ServicEvent),
                            Flag = true
                        });
                        environmentMonitor.LogSetting[LogPart.ServicEvent].Content = ServicEventLogFilePath;

                    servicEventLogController = containerProvider.Resolve<ILogController>(LogPart.ServicEvent.ToString());
                    environmentMonitor.LogSetting[LogPart.ServicEvent].LogController = servicEventLogController;

                    break;

                case LogPart.All:
                    Load(LogPart.Global);
                    Load(LogPart.Error);
                    Load(LogPart.DataBase);
                    Load(LogPart.ServicEvent);
                    break;
            }
        }

        public void Save(LogPart logPartArg)
        {

        }
    }
}
