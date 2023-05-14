using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Extension.Kernel.Core
{
    public interface ISetting
    {
        /// <summary>
        /// 当前医院代码
        /// </summary>
        string HospitalCode { get; set; }

        /// <summary>
        /// 上传文件目录
        /// </summary>
        string UpLoadFileCatalogue { get; set; }

        /// <summary>
        /// 当前显示模式
        /// </summary>
        DisplayModePart DisplayMode { get; set; }
    }
}
