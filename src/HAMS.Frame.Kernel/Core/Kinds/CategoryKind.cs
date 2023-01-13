using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Frame.Kernel.Core
{
    /// <summary>
    /// 分类型字典设置类型
    /// </summary>
    public class CategoryKind : BaseKind
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

        public CategoryKind(string codeArg,string itemArg,string nameArg, string contentArg, string descriptionArg, string noteArg,
                                string categoryCodeArg, string categoryNameArg, int rankArg, bool flagArg) : base(codeArg, itemArg, nameArg, contentArg, descriptionArg, noteArg, rankArg, flagArg)
        {
            CategoryCode = categoryCodeArg;
            CategoryName = categoryNameArg;
        }
    }
}
