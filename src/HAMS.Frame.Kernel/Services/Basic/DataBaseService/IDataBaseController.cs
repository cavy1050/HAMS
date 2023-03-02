using System.Collections.Generic;

namespace HAMS.Frame.Kernel.Services
{
    /// <summary>
    /// 提供对数据库操作的基本支持.
    /// </summary>
    public interface IDataBaseController
    {
        /// <summary>
        /// 提供对简单查询操作的基本支持，并记录操作日志
        /// </summary>
        bool Query<T>(string queryStingArgs, out List<T> tHub);

        /// <summary>
        /// 提供对简单数据操作的基本支持，并记录操作日志
        /// </summary>
        //bool Exec(string execStingArgs);

        /// <summary>
        /// 提供对简单数据查询操作的基本支持,不记录操作日志,用于程序初始化设置
        /// </summary>
        bool QueryNoLog<T>(string sqlSentenceArg, out List<T> tHub);

        /// <summary>
        /// 提供对简单数据操作的基本支持,不记录操作日志,用于程序初始化设置保存
        /// </summary>
        bool ExecNoLog(string sqlSentenceArg);

        /// <summary>
        /// 提供对查询语句执行支持,记录操作日志及返回结果
        /// </summary>
        bool ExecuteWithMessage(string sqlSentenceArg, out string retStringArg);
    }
}
