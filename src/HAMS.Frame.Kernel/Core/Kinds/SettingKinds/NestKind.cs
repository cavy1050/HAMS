using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string SuperCode { get; set; }

        /// <summary>
        /// 上级项
        /// </summary>
        public string SupeItem { get; set; }

        public NestKind() : base()
        {

        }

        public NestKind(string codeArg, string itemArg, string nameArg, string contentArg, string descriptionArg,
                                        string noteArg, string categoryCodeArg, string categoryNameArg, int rankArg, string superCodeArg, string supeItemArg, bool defaultFlag, bool enabledFlag) :
                                            base(codeArg, itemArg, nameArg, contentArg, descriptionArg, noteArg, rankArg, defaultFlag, enabledFlag)
        {
            SuperCode = superCodeArg;
            SupeItem = supeItemArg;
        }
    }
}
