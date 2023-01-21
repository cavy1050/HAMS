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
    public class BAGLDBBaseController : DataBaseControllerBase
    {
        string bagldbConnectionString;
        IEnvironmentMonitor environmentMonitor;

        public BAGLDBBaseController(IContainerProvider containerProviderArg) : base(containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();

            bagldbConnectionString = environmentMonitor.DataBaseSetting[DataBasePart.BAGLDB].Content;
            base.DBConnection = new SqlConnection(bagldbConnectionString);
        }
    }
}
