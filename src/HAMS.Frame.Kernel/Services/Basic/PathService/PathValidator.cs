using System.IO;
using Prism.Ioc;
using FluentValidation;
using HAMS.Frame.Kernel.Core;

namespace HAMS.Frame.Kernel.Services
{
    public class PathValidator : AbstractValidator<PathManager>
    {
        IEnvironmentMonitor environmentMonitor;

        public PathValidator()
        {
            RuleFor(path => path.ApplictionCatalogue).NotEmpty().WithMessage("程序运行目录不能为空,请检查设置并重启程序!");

            RuleFor(path => path.NativeDataBaseFilePath)
                .Cascade(CascadeMode.Stop).NotEmpty().WithMessage("本地数据库文件路径不能为空,请检查设置并重启程序!")
                .Must(filePath => File.Exists(filePath)).WithMessage("当前程序环境缺少本地数据库文件,请检查配置并重启程序!");

            RuleFor(path => path.LogFileCatalogue).NotEmpty().WithMessage("日志文件目录不能为空,请检查设置并重启程序!");
        }

        public PathValidator(IContainerProvider containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();

            Include(new PathValidator());

            RuleFor(path => path.LogFileCatalogue)
                .NotEqual(environmentMonitor.PathSetting.GetContent(PathPart.LogFileCatalogue)).WithMessage("日志文件目录与默认设置相同!");
        }
    }
}
