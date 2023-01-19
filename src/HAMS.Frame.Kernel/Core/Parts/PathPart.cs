using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HAMS.Frame.Kernel.Core
{
    public enum PathPart
    {
        /// <summary>
        /// 用于快速加载、保存路径信息
        /// </summary>
        All,

        [Description("程序运行目录")]
        ApplictionCatalogue,

        [Description("本地数据库文件路径")]
        NativeDataBaseFilePath,

        [Description("日志文件目录")]
        LogFileCatalogue
    }
}
