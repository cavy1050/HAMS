using System;

namespace HAMS.Frame.Kernel.Services
{
    /// <summary>
    /// 提供对IEnvironmentMonitor加载、持久化的支持.
    /// </summary>
    public interface IManager<TEnum> where TEnum : Enum
    {
        /// <summary>
        /// 初始化默认参数设置,包括默认程序运行目录、默认本地数据库文件路径
        /// </summary>
        void DeInit(TEnum tenum);

        /// <summary>
        /// 初始化非默认参数设置
        /// </summary>
        void Init(TEnum tenum);

        /// <summary>
        /// 初始化IController接口,对IEnvironmentMonitor加载数据
        /// </summary>
        void Load(TEnum tenum);

        /// <summary>
        /// 对IEnvironmentMonitor持久化数据
        /// </summary>
        void Save(TEnum tenum);
    }
}
