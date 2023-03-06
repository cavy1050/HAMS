namespace HAMS.Frame.Kernel.Core
{
    /// <summary>
    /// 分类型字典设置类型
    /// </summary>
    public class CategoryKind : SettingKind
    {
        /// <summary>
        /// 类别代码
        /// </summary>
        public virtual string CategoryCode { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public virtual string CategoryItem { get; set; }
    }
}
