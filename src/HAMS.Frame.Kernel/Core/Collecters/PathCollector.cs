using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Frame.Kernel.Core
{
    public class PathCollector : List<BaseKind>, ILocator<PathPart, string>
    {
        public BaseKind this[PathPart pathIndex]
        {
            set
            {
                if (pathIndex != PathPart.All)
                    base[FindIndex(x => x.Item == pathIndex.ToString())] = value;
                else
                    throw new ArgumentException(nameof(pathIndex));
            }

            get
            {
                if (pathIndex != PathPart.All)
                    return Find(x => x.Item == pathIndex.ToString());
                else
                    throw new ArgumentException(nameof(pathIndex));
            }
        }

        /// <summary>
        /// 获取指定程序路径
        /// </summary>
        public string GetContent(PathPart pathIndex)
        {
            if (pathIndex != PathPart.All)
                return Find(x => x.Item == pathIndex.ToString()).Content;
            else
                throw new ArgumentException(nameof(pathIndex));
        }
    }
}
