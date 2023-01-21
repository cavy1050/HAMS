using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HAMS.Frame.Kernel.Core
{
    public enum DataBasePart
    {
        /// <summary>
        /// 用于快速加载、保存数据库信息
        /// </summary>
        All,

        [Description("本地配置数据库")]
        Native,

        [Description("病案管理数据库")]
        BAGLDB
    }
}
