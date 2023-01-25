namespace HAMS.Frame.Kernel.Core
{
    /// <summary>
    /// 字典设置类型
    /// </summary>
    public class SettingKind : BaseKind
    {
        /// <summary>
        /// 名称(英文)--标识项
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// 名称(中文)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 设置值
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Rank { get; set; }

        // <summary>
        // 默认标志
        // </summary>
        public bool DefaultFlag { get; set; }

        public SettingKind()
        {

        }

        public SettingKind(string codeArg, string itemarg, string nameArg, string contentArg, string descriptionArg,
                                    string noteArg, int rankArgs, bool defaultFlag, bool enabledFlag) : base(codeArg, enabledFlag)
        {
            Item = itemarg;
            Name = nameArg;
            Content = contentArg;
            Description = descriptionArg;
            Note = noteArg;
            Rank = rankArgs;
            DefaultFlag = defaultFlag;
        }
    }
}
