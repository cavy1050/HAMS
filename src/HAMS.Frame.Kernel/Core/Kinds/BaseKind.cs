using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Frame.Kernel.Core
{
    /// <summary>
    /// 基础型字典设置类型
    /// </summary>
    public class BaseKind
    {
        /// <summary>
        /// 代码--标识项
        /// </summary>
        public string Code { get; set; }

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

        /// <summary>
        /// 是否默认设置
        /// </summary>
        //public bool DefaultFlag { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Flag { get; set; }

        public BaseKind()
        {

        }

        public BaseKind(string codeArg, string nameArg, string referNameArg, string contentArg, string descriptionArg,string noteArg, int rankArgs, bool flagArg)
        {
            Code = codeArg;
            Item = nameArg;
            Name = referNameArg;
            Content = contentArg;
            Description = descriptionArg;
            Note = noteArg;
            Rank = rankArgs;
            Flag = flagArg;
        }
    }
}
