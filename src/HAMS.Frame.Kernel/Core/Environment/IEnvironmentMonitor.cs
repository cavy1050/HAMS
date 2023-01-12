using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Frame.Kernel.Core
{
    /// <summary>
    /// 全局单例环境存储器,该类型提供程序路径信息、数据库信息、验证信息的外部访问支持.
    /// </summary>
    public interface IEnvironmentMonitor
    {
        PathCollecter PathSetting { get; set; }
    }
}
