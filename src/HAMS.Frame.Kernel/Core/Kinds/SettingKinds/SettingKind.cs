namespace HAMS.Frame.Kernel.Core
{
    /// <summary>
    /// 程序框架设置基本类型
    /// </summary>
    public class SettingKind : BaseKind
    {
        /// <summary>
        /// 名称(英文)--标识项
        /// </summary>
        public virtual string Item { get; set; }

        /// <summary>
        /// 名称(中文)
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Rank { get; set; }

        // <summary>
        // 默认标志
        // </summary>
        public virtual bool DefaultFlag { get; set; }
    }
}
