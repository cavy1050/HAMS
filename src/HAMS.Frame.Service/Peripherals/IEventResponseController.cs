using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Frame.Service.Peripherals
{
    public interface IEventResponseController
    {
        string Response(string requestServiceTextArg);
    }
}
