namespace HAMS.Frame.Kernel.Core
{
    /// <summary>
    /// 程序验证结果严重级别
    /// </summary>
    public enum SeverityLevelPart
    {
        /// <summary>
        /// 用于快速加载、保存严重级别信息
        /// </summary>
        All,

        /// <summary>
        /// 提示类、不影响程序运行
        /// </summary>
        Info,

        /// <summary>
        /// 错误类、必须将错误处理后程序才能继续运行
        /// </summary>
        Error
    }
}
