using System;

namespace HAMS.Frame.Kernel.Services
{
    /// <summary>
    /// 提供对IEnvironmentMonitor加载、持久化的支持.
    /// </summary>
    public interface IManager<TEnum> where TEnum : Enum
    {
        /// <summary>
        /// 初始化默认参数设置
        /// </summary>
        void DeInit(TEnum tenum);

        /// <summary>
        /// 已持久化数据初始化非默认参数设置
        /// </summary>
        void Init(TEnum tenum);

        /// <summary>
        /// 指定参数初始化非默认参数设置
        /// </summary>
        /// 
        /// <remarks>
        /// <paramref name="tenum"/>为PathPart类型时,<paramref name="costomArgs"/>为String类型,传入路径信息
        /// <paramref name="tenum"/>为DataBasePart类型时,<paramref name="costomArgs"/>为String类型,传入数据库连接字符串信息
        /// <paramref name="tenum"/>为LogPart类型时,<paramref name="costomArgs"/>第一参数为bool类型，传入是否启用日志,第二参数为LogLevel类型,传入日志级别
        /// </remarks>
        void Init(TEnum tenum, params object[] costomArgs);

        /// <summary>
        /// 对IEnvironmentMonitor加载数据,根据加载数据初始化IController接口
        /// </summary>
        void Load(TEnum tenum);

        /// <summary>
        /// 对IEnvironmentMonitor持久化数据
        /// </summary>
        void Save(TEnum tenum);
    }
}
