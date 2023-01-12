using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Frame.Kernel.Core
{
    /// <summary>
    /// 嵌套型字典设置类型
    /// </summary>
    public class NestKind : BaseKind
    {
        /// <summary>
        /// 上级代码
        /// </summary>
        public string SuperCode { get; set; }

        /// <summary>
        /// 上级名称
        /// </summary>
        public string SuperName { get; set; }

        public NestKind() : base()
        {

        }

        public NestKind(string codeArg, string nameArg, string referNameArg, string contentArg, string descriptionArg, string noteArg,
                        string superCodeArg, string superNameArg, int rankArg, bool flagArg) : base(codeArg, nameArg, referNameArg, contentArg, descriptionArg, noteArg, rankArg, flagArg)
        {
            SuperCode = superCodeArg;
            SuperName = superCodeArg;
        }
    }
}
