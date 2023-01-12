using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Frame.Kernel.Core
{
    public enum PathPart
    {
        /// <summary>
        /// 快速加载路径
        /// </summary>
        All,

        /// <summary>
        /// 程序运行目录
        /// </summary>
        ApplictionCatalogue,

        /// <summary>
        /// 本地数据库文件路径
        /// </summary>
        NativeDataBaseFilePath
    }
}
