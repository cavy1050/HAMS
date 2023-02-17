using System.Linq;
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
        protected bool GlobalLogEnabledFlag { get; set; }

        public LogControllerBase(IContainerProvider containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
            GlobalLogEnabledFlag = environmentMonitor.LogSetting[LogPart.Global].EnabledFlag;

            if (LoggerManager.GetAllRepositories().FirstOrDefault(x => x.Name == "Base") != null)
                BaseLoggerRepository = LoggerManager.GetRepository("Base");
            else
                BaseLoggerRepository = LoggerManager.CreateRepository("Base");

            BaseLoggerRepository.Configured = GlobalLogEnabledFlag;

            //%date{yyyy-MM-dd HH:mm:ss}
            TextLayout = new PatternLayout("%message%newline%newline");
        }

        public virtual void WriteDebug(string messageArg)
        {

        }

        public virtual void WriteInfo(string messageArg)
        {

        }

        public virtual void WriteWarn(string messageArg)
        {

        }

        public virtual void WriteError(string messageArg)
        {

        }

        public virtual void WriteFatal(string messageArg)
        {

        }
    }
}
