using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using FluentValidation;
using HAMS.Frame.Kernel.Core;

namespace HAMS.Frame.Kernel.Services
{
    public class CostomPathValidator: AbstractValidator<PathManager>
    {
        IEnvironmentMonitor environmentMonitor;

        public CostomPathValidator(IContainerProvider containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();

            RuleFor(path => path.LogFileCatalogue)
                .NotEmpty().WithMessage("日志文件目录自定义设置不能为空!");
                //.Must(filePath => !filePath.Equals(environmentMonitor.PathSetting.GetContent(PathPart.LogFileCatalogue))).WithMessage("日志文件目录与默认设置相同!");
        }
    }
}
