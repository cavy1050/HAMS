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
    public class ZYCISDBBaseController : DataBaseControllerBase
    {
        string zycisdbConnectionString;
        IEnvironmentMonitor environmentMonitor;

        public ZYCISDBBaseController(IContainerProvider containerProviderArg) : base(containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();

            zycisdbConnectionString = environmentMonitor.DataBaseSetting[DataBasePart.ZYCISDB].Content;
            base.DBConnection = new SqlConnection(zycisdbConnectionString);
        }
    }
}
