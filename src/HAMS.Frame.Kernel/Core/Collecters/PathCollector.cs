using System;
using System.Collections.Generic;

namespace HAMS.Frame.Kernel.Core
{
    public class PathCollector : List<SettingKind>, ILocator<PathPart, string>
    {
        public SettingKind this[PathPart pathIndex]
        {
            set
            {
                if (pathIndex != PathPart.All)
                    base[FindIndex(x => x.Item == pathIndex.ToString())] = value;
                else
                    throw new ArgumentException("索引器只能单个存取!", nameof(pathIndex));
            }

            get
            {
                if (pathIndex != PathPart.All)
                    return Find(x => x.Item == pathIndex.ToString());
                else
                    throw new ArgumentException("索引器只能单个存取!", nameof(pathIndex));
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
                throw new ArgumentException("定位器只能单个存取!", nameof(pathIndex));
        }
    }
}
