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
    public class ErrorLogController : LogControllerBase
    {
        FileAppender errorFileAppender;
        Logger errorLogger;

        IEnvironmentMonitor environmentMonitor;

        public ErrorLogController(IContainerProvider containerProviderArg) : base(containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();

            errorFileAppender = new FileAppender();
            errorFileAppender.Name = "ErrorFlatFile";
            errorFileAppender.Layout = base.TextLayout;
            errorFileAppender.AppendToFile = true;
            errorFileAppender.File = environmentMonitor.LogSetting[LogPart.Error].Content;

            errorLogger = (Logger)LoggerManager.GetLogger("Base", "Error");

            if (base.GlobalLogEnabledFilter == true)
            {
                errorFileAppender.ActivateOptions();

                if (errorLogger.GetAppender("ErrorFlatFile") != null)
                {
                    errorLogger.RemoveAppender("ErrorFlatFile");
                }

                errorLogger.AddAppender(errorFileAppender);
            }
        }

        public override void Write(string messageArg)
        {
            errorLogger.Log(Level.Error, messageArg, null);
        }
    }
}
