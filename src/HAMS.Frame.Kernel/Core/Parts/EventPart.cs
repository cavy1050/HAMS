namespace HAMS.Frame.Kernel.Core
{
    public enum EventPart
    {
        /// <summary>
        /// 程序事件
        /// </summary>
        ApplicationEvent = 1101,

        /// <summary>
        /// 路径事件
        /// </summary>
        PathEvent = 2101,

        /// <summary>
        /// 数据库事件
        /// </summary>
        DataBaseEvent = 3101,

        /// <summary>
        /// 日志事件
        /// </summary>
        LogEvent = 4101,

        /// <summary>
        /// 主题事件
        /// </summary>
        ThemeEvent = 5101,

        /// <summary>
        /// 用户事件
        /// </summary>
        AccountEvent = 6101,

        /// <summary>
        /// 扩展模块事件
        /// </summary>
        ExtensionModuleEvent = 7101,
    }
}
