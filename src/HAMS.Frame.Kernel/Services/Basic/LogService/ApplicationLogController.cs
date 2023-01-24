using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using log4net.Core;
using log4net.Appender;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using HAMS.Frame.Kernel.Core;

namespace HAMS.Frame.Kernel.Services
{
    public class ApplicationLogController : LogControllerBase
    {
        FileAppender errorFileAppender;
        Logger errorLogger;

        IEnvironmentMonitor environmentMonitor;

        public ApplicationLogController(IContainerProvider containerProviderArg) : base(containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();

            errorFileAppender = new FileAppender();
            errorFileAppender.Name = "ApplicationFlatFile";
            errorFileAppender.Layout = base.TextLayout;
            errorFileAppender.AppendToFile = true;
            errorFileAppender.File = environmentMonitor.LogSetting[LogPart.Application].Content;

            errorLogger = (Logger)LoggerManager.GetLogger("Base", "Application");

            if (base.GlobalLogEnabledFlag == true)
            {
                errorFileAppender.ActivateOptions();

                if (errorLogger.GetAppender("ApplicationFlatFile") != null)
                {
                    errorLogger.RemoveAppender("ApplicationFlatFile");
                }

                errorLogger.AddAppender(errorFileAppender);
            }
        }

        public override void WriteInfo(string messageArg)
        {
            errorLogger.Log(Level.Info, messageArg, null);
        }

        public override void WriteError(string messageArg)
        {
            errorLogger.Log(Level.Error, messageArg, null);
        }
    }
}
