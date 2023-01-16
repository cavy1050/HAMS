﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HAMS.Frame.Kernel.Core
{
    public enum PathPart
    {
        [Description("所有路径")]
        All,

        [Description("程序运行目录")]
        ApplictionCatalogue,

        [Description("本地数据库文件路径")]
        NativeDataBaseFilePath
    }
}