using FluentValidation;

namespace HAMS.Frame.Kernel.Services
{
    public class LogValidator : AbstractValidator<LogManager>
    {
        public LogValidator()
        {
            RuleFor(log => log.ApplicationLogFilePath).NotEmpty().WithMessage("程序运行日志文件路径不能为空!");

            RuleFor(log => log.DataBaseLogFilePath).NotEmpty().WithMessage("数据库日志文件路径不能为空!");

            RuleFor(log => log.ServicEventLogFilePath).NotEmpty().WithMessage("服务事件日志文件路径不能为空!");

            RuleFor(log => log.GlobalLogEnabledFlag).NotNull().WithMessage("全局日志启用标志不能为空!");

            RuleFor(log => log.GlobalLogLevel).NotNull().WithMessage("全局日志级别设置不能为空!");
        }
    }
}
