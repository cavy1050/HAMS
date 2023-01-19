using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HAMS.Frame.Kernel.Core
{
    public enum LogPart
    {
        /// <summary>
        /// 用于快速加载、保存日志信息
        /// </summary>
        All,

        [Description("全局设置")]
        Global,

        [Description("错误日志")]
        Error,

        [Description("数据库日志")]
        DataBase,

        [Description("服务事件日志")]
        ServicEvent
    }
}
