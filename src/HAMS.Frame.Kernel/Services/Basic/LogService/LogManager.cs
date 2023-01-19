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
    public class LogManager : IManager<LogPart>
    {
        /// <summary>
        /// 默认程序错误日志文件路径
        /// </summary>
        public string DefaultErrorLogFilePath { get; set; }

        /// <summary>
        /// 默认数据库日志文件路径
        /// </summary>
        public string DefaultDataBaseLogFilePath { get; set; }

        /// <summary>
        /// 默认服务事件日志文件目录
        /// </summary>
        public string DefaultServicEventLogFilePath { get; set; }

        /// <summary>
        /// 默认错误日志级别
        /// </summary>
        public Level DefaultErrorLogLevel { get; set; }

        /// <summary>
        /// 默认数据库日志级别
        /// </summary>
        public Level DefaultDataBaseLogLevel { get; set; }

        /// <summary>
        /// 默认服务事件日志级别
        /// </summary>
        public Level DefaultServicEventLogLevel { get; set; }

        /// <summary>
        /// 默认全局日志启用设置
        /// </summary>
        public bool DefaultGlobalLogEnabledFilter { get; set; }

        /// <summary>
        /// 默认全局日志级别
        /// </summary>
        public Level DefaultGlobalLogLevel { get; set; }

        IEnvironmentMonitor environmentMonitor;

        public LogManager(IContainerProvider containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
        }

        public void DeInit(LogPart logPartArg)
        {
            string logLogFileCatalogueText = environmentMonitor.PathSetting.GetContent(PathPart.LogFileCatalogue);

            switch (logPartArg)
            {
                case LogPart.Global:
                    DefaultGlobalLogEnabledFilter = true;
                    DefaultGlobalLogLevel = Level.Debug;
                    break;

                case LogPart.Error:
                    DefaultErrorLogFilePath = logLogFileCatalogueText + "Error" + DateTime.Now.ToString("d") + ".txt";
                    DefaultErrorLogLevel = Level.Error;
                    break;

                case LogPart.DataBase:
                    DefaultDataBaseLogFilePath = logLogFileCatalogueText + "DataBase" + DateTime.Now.ToString("d") + ".txt";
                    DefaultDataBaseLogLevel = Level.Debug;
                    break;

                case LogPart.ServicEvent:
                    DefaultServicEventLogFilePath = logLogFileCatalogueText + "ServicEvent" + DateTime.Now.ToString("d") + ".txt";
                    DefaultServicEventLogLevel = Level.Debug;
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

        }

        public void DeLoad(LogPart logPartArg)
        {

        }

        public void Load(LogPart logPartArg)
        {
            switch (logPartArg)
            {
                case LogPart.Global:
                    if (!environmentMonitor.LogSetting.Exists(x => x.Code == "01GPT3T83953EVANVTJ0ATFAK5"))
                    {
                        environmentMonitor.LogSetting.Add(new LogKind
                        {
                            Code = "01GPT3T83953EVANVTJ0ATFAK5",
                            Item = LogPart.Global.ToString(),
                            Name = EnumExtension.GetDescription(LogPart.Global),
                            Note = DefaultGlobalLogLevel.ToString(),
                            Rank = Convert.ToInt32(LogPart.Global),
                            Flag = true
                        });
                    }
                    break;

                case LogPart.Error:
                    if (!environmentMonitor.LogSetting.Exists(x => x.Code == "01GPT3T839Q7VP6GAGQT12PBXK"))
                    {
                        environmentMonitor.LogSetting.Add(new LogKind
                        {
                            Code = "01GPT3T839Q7VP6GAGQT12PBXK",
                            Item = LogPart.Error.ToString(),
                            Name = EnumExtension.GetDescription(LogPart.Error),
                            Content = DefaultErrorLogFilePath,
                            Note = DefaultErrorLogLevel.ToString(),
                            Rank = Convert.ToInt32(LogPart.Error),
                            Flag = true
                        });
                    }
                    break;

                case LogPart.DataBase:
                    if (!environmentMonitor.LogSetting.Exists(x => x.Code == "01GPT3T839BDKG6EA03W22MX0K"))
                    {
                        environmentMonitor.LogSetting.Add(new LogKind
                        {
                            Code = "01GPT3T839BDKG6EA03W22MX0K",
                            Item = LogPart.DataBase.ToString(),
                            Name = EnumExtension.GetDescription(LogPart.DataBase),
                            Content = DefaultDataBaseLogFilePath,
                            Note = DefaultDataBaseLogLevel.ToString(),
                            Rank = Convert.ToInt32(LogPart.DataBase),
                            Flag = true
                        });
                    }
                    break;

                case LogPart.ServicEvent:
                    if (!environmentMonitor.LogSetting.Exists(x => x.Code == "01GPT3T839QKWK6A7GHVV2PH8E"))
                    {
                        environmentMonitor.LogSetting.Add(new LogKind
                        {
                            Code = "01GPT3T839QKWK6A7GHVV2PH8E",
                            Item = LogPart.ServicEvent.ToString(),
                            Name = EnumExtension.GetDescription(LogPart.ServicEvent),
                            Content = DefaultServicEventLogFilePath,
                            Note = DefaultServicEventLogLevel.ToString(),
                            Rank = Convert.ToInt32(LogPart.ServicEvent),
                            Flag = true
                        });
                    }
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
