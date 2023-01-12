using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using FluentValidation;

namespace HAMS.Frame.Kernel.Services
{
    public class PathValidator : AbstractValidator<PathManager>
    {
        public PathValidator()
        {
            RuleFor(path => path.DefaultApplictionCatalogue).NotEmpty().WithMessage("默认程序运行目录不能为空!");

            RuleFor(path => path.DefaultNativeDataBaseFilePath).Must(filePath => File.Exists(filePath)).WithMessage("当前程序环境缺少本地数据库文件,请检查配置并重启程序!");
        }
    }
}
