using System;
using System.Collections.Generic;
using System.Data;
using Prism.Ioc;
using Dapper;
using HAMS.Frame.Kernel.Core;

namespace HAMS.Frame.Kernel.Services
{
    public abstract class DataBaseControllerBase : IDataBaseController
    {
        IEnvironmentMonitor environmentMonitor;
        protected IDbConnection DBConnection { get; set; }
        ILogController dataBaseLogController;

        public DataBaseControllerBase(IContainerProvider containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
        }

        public virtual bool QueryNoLog<T>(string sqlSentenceArg, out List<T> tHub)
        {
            bool ret = false;
            tHub = default(List<T>);

            if (environmentMonitor.SeveritySetting[SeverityLevelPart.Error].Results.IsValid)
            {
                CommandDefinition commandDefinition = new CommandDefinition(sqlSentenceArg);
                DBConnection.Open();
                tHub = SqlMapper.Query<T>(DBConnection, commandDefinition).AsList();
                ret = true;
                DBConnection.Close();
            }

            return ret;
        }

        public virtual bool ExecNoLog(string sqlSentenceArg)
        {
            bool ret = false;
            int retVal;

            if (environmentMonitor.SeveritySetting[SeverityLevelPart.Error].Results.IsValid)
            {
                CommandDefinition commandDefinition = new CommandDefinition(sqlSentenceArg);
                DBConnection.Open();
                retVal = SqlMapper.Execute(DBConnection, commandDefinition);
                ret = true;
                DBConnection.Close();
            }

            return ret;
        }

        public virtual bool Query<T>(string queryStringArg, out List<T> tHub)
        {
            bool ret = false;
            tHub = default(List<T>);
            dataBaseLogController= environmentMonitor.LogSetting.GetContent(LogPart.DataBase);

            if (environmentMonitor.SeveritySetting[SeverityLevelPart.Error].Results.IsValid)
            {
                CommandDefinition commandDefinition = new CommandDefinition(queryStringArg);

                try
                {
                    DBConnection.Open();
                    tHub = SqlMapper.Query<T>(DBConnection, commandDefinition).AsList();
                }
                catch (Exception ex)
                {
                    dataBaseLogController.WriteDebug(ex.Message);
                }
                finally
                {
                    ret = true;
                    dataBaseLogController.WriteDebug(queryStringArg);
                    DBConnection.Close();
                }
            }

            return ret;
        }
    }
}
