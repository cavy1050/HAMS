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

        public CategoryKind(string codeArgs, string nameArgs, string contentArgs, string descriptionArgs, string noteArg,
                                string categoryCodeArgs, string categoryNameArgs, int rankArgs, bool flagArgs) : base(codeArgs, nameArgs, contentArgs, descriptionArgs, noteArg, rankArgs, flagArgs)
        {
            CategoryCode = categoryCodeArgs;
            CategoryName = categoryNameArgs;
        }
    }
}
