using System;
using Prism.Ioc;
using System.Data.SQLite;
using HAMS.Frame.Kernel.Core;

namespace HAMS.Frame.Kernel.Services
{
    public class NativeBaseController : DataBaseControllerBase
    {
        string nativeConnectionString;
        IEnvironmentMonitor environmentMonitor;

        public NativeBaseController(IContainerProvider containerProviderArg) : base(containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();

            nativeConnectionString = environmentMonitor.DataBaseSetting[DataBasePart.Native].Content;
            base.DBConnection = new SQLiteConnection(nativeConnectionString);
        }
    }
}
