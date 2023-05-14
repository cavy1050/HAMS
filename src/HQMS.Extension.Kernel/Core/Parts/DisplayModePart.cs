using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HQMS.Extension.Kernel.Core
{
    /// <summary>
    /// 诊断及手术信息显示模式(默认不显示其他信息，加快界面加载速度)
    /// </summary>
    public enum DisplayModePart
    {
        /// <summary>
        /// 诊断及手术信息仅显示主要信息
        /// </summary>
        [Description("标准")]
        Standard,

        /// <summary>
        /// 诊断及手术信息显示前十位其他信息
        /// </summary>
        [Description("扩展")]
        Extension,

        /// <summary>
        /// 显示全部主要,其他诊断、手术信息
        /// </summary>
        [Description("完全")]
        Full
    }
}
