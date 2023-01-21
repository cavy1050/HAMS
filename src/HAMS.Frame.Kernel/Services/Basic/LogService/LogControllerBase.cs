using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using log4net.Core;
using log4net.Layout;
using log4net.Repository;
using HAMS.Frame.Kernel.Core;

namespace HAMS.Frame.Kernel.Services
{
    public abstract class LogControllerBase : ILogController
    {
        IEnvironmentMonitor environmentMonitor;

        protected ILoggerRepository BaseLoggerRepository { get; set; }
        protected virtual ILayout TextLayout { get; set; }
        protected bool GlobalLogEnabledFilter { get; set; }

        public LogControllerBase(IContainerProvider containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
            GlobalLogEnabledFilter = environmentMonitor.LogSetting[LogPart.Global].Flag;

            if (LoggerManager.GetAllRepositories().FirstOrDefault(x => x.Name == "Base") != null)
                BaseLoggerRepository = LoggerManager.GetRepository("Base");
            else
                BaseLoggerRepository = LoggerManager.CreateRepository("Base");

            BaseLoggerRepository.Configured = GlobalLogEnabledFilter;

            TextLayout= new PatternLayout("%date{yyyy-MM-dd HH:mm:ss}  %message %logger %newline");
        }

        public virtual void Write(string messageArg)
        {

        }
    }
}
