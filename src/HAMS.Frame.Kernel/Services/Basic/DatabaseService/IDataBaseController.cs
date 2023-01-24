using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAMS.Frame.Kernel.Core;

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
        //bool Query<T>(string queryStingArgs, out List<T> tHub);

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
        //bool ExecNoLog(string sqlSentenceArg);
    }
}
