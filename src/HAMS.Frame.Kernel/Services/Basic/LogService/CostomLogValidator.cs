using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace HAMS.Frame.Kernel.Services
{
    public class CostomLogValidator : AbstractValidator<LogManager>
    {
        public CostomLogValidator()
        {
            RuleFor(log => log.GlobalLogEnabledFilter).NotEmpty().WithMessage("全局日志文件启用自定义设置不能为空");

            RuleFor(log => log.GlobalLogLevel).NotEmpty().WithMessage("全局日志文件级别自定义设置不能为空");
        }
    }
}
