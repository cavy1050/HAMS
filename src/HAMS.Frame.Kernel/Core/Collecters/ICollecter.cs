using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Frame.Kernel.Core
{
    public interface ICollecter<Tin,Tout> where Tin : Enum
    {
        Tout GetContent(Tin t);
    }
}
