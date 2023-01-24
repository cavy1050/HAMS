using Prism.Ioc;
using log4net.Core;
using log4net.Appender;
using log4net.Repository.Hierarchy;
using HAMS.Frame.Kernel.Core;

namespace HAMS.Frame.Kernel.Services
{
    public class DataBaseLogController : LogControllerBase
    {
        FileAppender dataBaseFileAppender;
        Logger dataBaseLogger;

        IEnvironmentMonitor environmentMonitor;

        public DataBaseLogController(IContainerProvider containerProviderArg) : base(containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();

            dataBaseFileAppender = new FileAppender();
            dataBaseFileAppender.Name = "DataBaseFlatFile";
            dataBaseFileAppender.Layout = base.TextLayout;
            dataBaseFileAppender.AppendToFile = true;
            dataBaseFileAppender.File = environmentMonitor.LogSetting[LogPart.DataBase].Content;

            dataBaseLogger = (Logger)LoggerManager.GetLogger("Base", "DataBase");

            if (base.GlobalLogEnabledFlag == true)
            {
                dataBaseFileAppender.ActivateOptions();

                if (dataBaseLogger.GetAppender("DataBaseFlatFile") != null)
                {
                    dataBaseLogger.RemoveAppender("DataBaseFlatFile");
                }

                dataBaseLogger.AddAppender(dataBaseFileAppender);
            }
        }

        public override void WriteDebug(string messageArg)
        {
            dataBaseLogger.Log(Level.Debug, messageArg, null);
        }
    }
}
