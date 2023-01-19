using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Frame.Kernel.Core
{
    /// <summary>
    /// 通过枚举存取Collector中数据
    /// </summary>
    public interface ILocator<Tin,Tout> where Tin : Enum
    {
        Tout GetContent(Tin t);
    }
}
