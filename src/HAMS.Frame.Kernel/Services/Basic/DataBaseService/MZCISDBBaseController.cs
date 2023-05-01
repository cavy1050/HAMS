using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Prism.Ioc;
using HAMS.Frame.Kernel.Core;

namespace HAMS.Frame.Kernel.Services
{
    public class MZCISDBBaseController : DataBaseControllerBase
    {
        string mzcisdbConnectionString;
        IEnvironmentMonitor environmentMonitor;

        public MZCISDBBaseController(IContainerProvider containerProviderArg) : base(containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();

            mzcisdbConnectionString = environmentMonitor.DataBaseSetting[DataBasePart.MZCISDB].Content;
            base.DBConnection = new SqlConnection(mzcisdbConnectionString);
        }
    }
}
