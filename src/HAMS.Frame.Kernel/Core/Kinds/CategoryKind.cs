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
        public string CategoryCode { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public string CategoryName { get; set; }

        public CategoryKind() : base()
        {

        }

        public CategoryKind(string codeArg, string itemArg, string nameArg, string contentArg, string descriptionArg,
                                        string noteArg, string categoryCodeArg, string categoryNameArg, int rankArg, bool defaultFlag, bool enabledFlag) :
                                            base(codeArg, itemArg, nameArg, contentArg, descriptionArg, noteArg, rankArg, defaultFlag, enabledFlag)
        {
            CategoryCode = categoryCodeArg;
            CategoryName = categoryNameArg;
        }
    }
}
