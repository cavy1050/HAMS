using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAMS.Frame.Kernel.Services;

namespace HAMS.Frame.Kernel.Core
{
    public class LogCollector : List<LogKind>, ILocator<LogPart, ILogController>
    {
        public LogKind this[LogPart logIndex]
        {
            set
            {
                if (logIndex != LogPart.All)
                    base[FindIndex(x => x.Item == logIndex.ToString())] = value;
                else
                    throw new ArgumentException(nameof(logIndex));
            }

            get
            {
                if (logIndex != LogPart.All)
                    return Find(x => x.Item == logIndex.ToString());
                else
                    throw new ArgumentException(nameof(logIndex));
            }
        }

        /// <summary>
        /// 获取指定日志操作接口
        /// </summary>
        public ILogController GetContent(LogPart logIndex)
        {
            if (logIndex != LogPart.All)
                return Find(x => x.Item == logIndex.ToString()).LogController;
            else
                throw new ArgumentException(nameof(logIndex));
        }
    }
}
