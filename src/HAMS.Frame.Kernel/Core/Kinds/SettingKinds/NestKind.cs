namespace HAMS.Frame.Kernel.Core
{
    /// <summary>
    /// 嵌套字典设置类型
    /// </summary>
    public class NestKind : SettingKind
    {
        /// <summary>
        /// 上级代码
        /// </summary>
        public virtual string SuperCode { get; set; }

        /// <summary>
        /// 上级项
        /// </summary>
        public virtual string SuperItem { get; set; }
    }
}
