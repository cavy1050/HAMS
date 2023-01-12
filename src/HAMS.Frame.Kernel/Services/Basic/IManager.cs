using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Frame.Kernel.Services
{
    /// <summary>
    /// 提供对IEnvironmentMonitor加载、持久化的支持.
    /// </summary>
    public interface IManager<T> where T : Enum
    {
        /// <summary>
        /// 对IEnvironmentMonitor加载数据
        /// </summary>
        void Load(T t);

        /// <summary>
        /// 对IEnvironmentMonitor持久化数据
        /// </summary>
        void Save(T t);
    }
}
