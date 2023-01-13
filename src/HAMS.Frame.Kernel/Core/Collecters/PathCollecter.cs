using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Frame.Kernel.Core
{
    public class PathCollecter : List<BaseKind>, ICollecter<PathPart, string>
    {
        public BaseKind this[PathPart pathIndex]
        {
            set { base[FindIndex(x => x.Item == pathIndex.ToString())] = value; }
            get { return Find(x => x.Item == pathIndex.ToString()); }
        }

        /// <summary>
        /// 获取指定程序路径
        /// </summary>
        public string GetContent(PathPart pathIndex)
        {
            return Find(x => x.Name == pathIndex.ToString()).Content;
        }
    }
}
