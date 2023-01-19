using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Prism.Ioc;
using Dapper;
using HAMS.Frame.Kernel.Core;

namespace HAMS.Frame.Kernel.Services
{
    public abstract class DataBaseControllerBase: IDataBaseController
    {
        IEnvironmentMonitor environmentMonitor;
        protected IDbConnection DBConnection { get; set; }

        public DataBaseControllerBase(IContainerProvider containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
        }

        public virtual bool QueryNoLog<T>(string queryTextArg, out List<T> tHub)
        {
            bool ret = false;
            tHub = default(List<T>);

            if (environmentMonitor.ValidationSetting.IsValid)
            {
                CommandDefinition commandDefinition = new CommandDefinition(queryTextArg);
                DBConnection.Open();
                tHub = SqlMapper.Query<T>(DBConnection, commandDefinition).AsList();
                ret = true;
                DBConnection.Close();
            }

            return ret;
        }
    }
}
