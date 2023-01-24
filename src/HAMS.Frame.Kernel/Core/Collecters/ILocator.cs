using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Frame.Kernel.Core
{
    /// <summary>
    /// 定位器接口，通过枚举定位Collector集合
    /// </summary>
    public interface ILocator<Tin,Tout> where Tin : Enum
    {
        Tout GetContent(Tin t);
    }
}
