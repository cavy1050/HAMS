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

        [Description("程序运行日志")]
        Application,

        [Description("数据库日志")]
        DataBase,

        [Description("服务事件日志")]
        ServicEvent
    }
}
