using System.Collections.Generic;

namespace HAMS.Frame.Kernel.Services
{
    /// <summary>
    /// 提供对数据库操作的基本支持.
    /// </summary>
    public interface IDataBaseController
    {
        /// <summary>
        /// 测试连接
        /// </summary>
        bool Connection();

        /// <summary>
        /// 提供对本地数据库数据查询操作的基本支持,不记录操作日志,仅用于程序初始化设置
        /// </summary>
        bool QueryNoLog<T>(string sqlSentenceArg, out List<T> tHub);

        /// <summary>
        /// 提供对本地数据库数据操作的基本支持,不记录操作日志,仅用于程序初始化设置保存
        /// </summary>
        bool ExecNoLog(string sqlSentenceArg);

        /// <summary>
        /// 提供对简单查询操作、表值函数查询的基本支持，并记录操作日志
        /// </summary>
        bool Query<T>(string sqlSentenceArg, out List<T> tHub);

        /// <summary>
        /// 提供简单数据操作支持，并记录操作日志
        /// </summary>
        bool Execute(string sqlSentenceArg);

        /// <summary>
        /// 提供对查询操作执行的支持，记录操作日志及执行结果
        /// </summary>
        bool QueryWithMessage<T>(string sqlSentenceArg, out List<T> tHub,out string retStringArg);

        /// <summary>
        /// 提供对数据操作的执行支持,记录操作日志及执行结果
        /// </summary>
        bool ExecuteWithMessage(string sqlSentenceArg, out string retStringArg);
    }
}
