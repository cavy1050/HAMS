using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Extension.Control.Main
{
    /// <summary>
    /// 绩效考核配置器
    /// </summary>
    public interface IConfigurator
    {
        /// <summary>
        /// 医院代码
        /// </summary>
        string HospitalCode { get; set; }

        /// <summary>
        /// 导出文件目录
        /// </summary>
        string ExportFileCatalogue { get; set; }

        /// <summary>
        /// 上传文件目录
        /// </summary>
        string UpLoadFileCatalogue { get; set; }

        /// <summary>
        /// 汇总文件导出目录
        /// </summary>
        string MasterExportFileCatalogue { get; set; }

        /// <summary>
        /// 明细文件导出目录
        /// </summary>
        string DetailExportFileCatalogue { get; set; }
    }
}
