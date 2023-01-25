using System;
using System.Collections.Generic;
using System.Linq;
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
        List<SettingKind> costomLogSettingHub;
        LogLevelConverter logLevelConverter;

        IContainerProvider containerProvider;
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeBaseController;
        ILogController errorLogController;
        ILogController dataBaseLogController;
        ILogController servicEventLogController;

        /// <summary>
        /// 程序运行日志文件路径
        /// </summary>
        public string ApplicationLogFilePath { get; set; }

        /// <summary>
        /// 数据库日志文件路径
        /// </summary>
        public string DataBaseLogFilePath { get; set; }

        /// <summary>
        /// 服务事件日志文件路径
        /// </summary>
        public string ServicEventLogFilePath { get; set; }

        /// <summary>
        /// 全局日志启用标志
        /// </summary>
        public bool GlobalLogEnabledFlag { get; set; }

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
                    GlobalLogEnabledFlag = true;
                    GlobalLogLevel = Level.Debug;
                    break;

                case LogPart.Application:
                    ApplicationLogFilePath = logFileCatalogue + "Application_" + DateTime.Now.ToString("d") + ".txt";
                    break;

                case LogPart.DataBase:
                    DataBaseLogFilePath = logFileCatalogue + "DataBase_" + DateTime.Now.ToString("d") + ".txt";
                    break;

                case LogPart.ServicEvent:
                    ServicEventLogFilePath = logFileCatalogue + "ServicEvent_" + DateTime.Now.ToString("d") + ".txt";
                    break;

                case LogPart.All:
                    DeInit(LogPart.Global);
                    DeInit(LogPart.Application);
                    DeInit(LogPart.DataBase);
                    DeInit(LogPart.ServicEvent);
                    break;
            }
        }

        public void Init(LogPart logPartArg)
        {
            logLevelConverter = new LogLevelConverter();
            logFileCatalogue = environmentMonitor.PathSetting.GetContent(PathPart.LogFileCatalogue);

            nativeBaseController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.Native);
            sqlSentence = "SELECT Code,Item,Name,Content,Description,Note,Rank,DefaultFlag,EnabledFlag FROM System_LogSetting WHERE EnabledFlag = True AND DefaultFlag = False";
            nativeBaseController.QueryNoLog<SettingKind>(sqlSentence, out costomLogSettingHub);

            switch (logPartArg)
            {
                case LogPart.Global:
                    GlobalLogLevel= (Level)logLevelConverter.ConvertFrom(costomLogSettingHub.FirstOrDefault(x => x.Code == "01GPT3T83953EVANVTJ0ATFAK5").Note);
                    GlobalLogEnabledFlag = costomLogSettingHub.FirstOrDefault(x=>x.Code== "01GPT3T83953EVANVTJ0ATFAK5").EnabledFlag;
                    break;

                case LogPart.Application:
                    ApplicationLogFilePath = logFileCatalogue + "Application_" + DateTime.Now.ToString("d") + ".txt";
                    break;

                case LogPart.DataBase:
                    DataBaseLogFilePath = logFileCatalogue + "DataBase_" + DateTime.Now.ToString("d") + ".txt";
                    break;

                case LogPart.ServicEvent:
                    ServicEventLogFilePath = logFileCatalogue + "ServicEvent_" + DateTime.Now.ToString("d") + ".txt";
                    break;

                case LogPart.All:
                    Init(LogPart.Global);
                    Init(LogPart.Application);
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
                            EnabledFlag = GlobalLogEnabledFlag
                        });
                    else
                    {
                        environmentMonitor.LogSetting[LogPart.Global].EnabledFlag = GlobalLogEnabledFlag;
                        environmentMonitor.LogSetting[LogPart.Global].Note = GlobalLogLevel.ToString();
                    }
                    break;

                case LogPart.Application:
                    if (!environmentMonitor.LogSetting.Exists(x => x.Code == "01GPT3T839Q7VP6GAGQT12PBXK"))
                        environmentMonitor.LogSetting.Add(new LogKind
                        {
                            Code = "01GPT3T839Q7VP6GAGQT12PBXK",
                            Item = LogPart.Application .ToString(),
                            Name = EnumExtension.GetDescription(LogPart.Application),
                            Content = ApplicationLogFilePath,
                            Rank = Convert.ToInt32(LogPart.Application),
                            EnabledFlag = true
                        });
                    else
                        environmentMonitor.LogSetting[LogPart.Application].Content = ApplicationLogFilePath;

                    errorLogController = containerProvider.Resolve<ILogController>(LogPart.Application.ToString());
                    environmentMonitor.LogSetting[LogPart.Application].LogController = errorLogController;

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
                            EnabledFlag = true
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
                            EnabledFlag = true
                        });
                        environmentMonitor.LogSetting[LogPart.ServicEvent].Content = ServicEventLogFilePath;

                    servicEventLogController = containerProvider.Resolve<ILogController>(LogPart.ServicEvent.ToString());
                    environmentMonitor.LogSetting[LogPart.ServicEvent].LogController = servicEventLogController;

                    break;

                case LogPart.All:
                    Load(LogPart.Global);
                    Load(LogPart.Application);
                    Load(LogPart.DataBase);
                    Load(LogPart.ServicEvent);
                    break;
            }
        }

        public void Save(LogPart logPartArg)
        {
            switch (logPartArg)
            {
                case LogPart.Global:
                    sqlSentence = "UPDATE System_LogSetting SET EnabledFlag=" + GlobalLogEnabledFlag + ",GlobalLogLevel='"+GlobalLogLevel.ToString()+"' WHERE Code='01GPT3T83953EVANVTJ0ATFAK5'";
                    nativeBaseController.ExecNoLog(sqlSentence);
                    break;

                case LogPart.Application:
                    sqlSentence = "UPDATE System_PathSetting SET Content='" + ApplicationLogFilePath + "' WHERE Code='01GPT3T839Q7VP6GAGQT12PBXK'";
                    nativeBaseController.ExecNoLog(sqlSentence);
                    break;

                case LogPart.DataBase:
                    sqlSentence = "UPDATE System_PathSetting SET Content='" + DataBaseLogFilePath + "' WHERE Code='01GPT3T839BDKG6EA03W22MX0K'";
                    nativeBaseController.ExecNoLog(sqlSentence);
                    break;

                case LogPart.ServicEvent:
                    sqlSentence = "UPDATE System_PathSetting SET Content='" + ServicEventLogFilePath + "' WHERE Code='01GPT3T839QKWK6A7GHVV2PH8E'";
                    nativeBaseController.ExecNoLog(sqlSentence);
                    break;

                case LogPart.All:
                    Save(LogPart.Global);
                    Save(LogPart.Application);
                    Save(LogPart.DataBase);
                    Save(LogPart.ServicEvent);
                    break;
            }
        }
    }
}
