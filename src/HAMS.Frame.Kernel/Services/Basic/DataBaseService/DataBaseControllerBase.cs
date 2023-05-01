using System;
using System.Threading.Tasks;
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
        IDataReader reader;

        public DataBaseControllerBase(IContainerProvider containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
        }

        public virtual bool Connection()
        {
            bool ret = false;

            try
            {
                DBConnection.Open();
                if (DBConnection.State == ConnectionState.Open)
                    ret = true;
            }
            catch
            {

            }
            finally
            {
                DBConnection.Close();
            }

            return ret;
        }

        public virtual bool QueryNoLog<T>(string sqlSentenceArg, out List<T> tHub)
        {
            bool ret = false;
            tHub = default(List<T>);

            if (environmentMonitor.ValidationResult.IsValid)
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

            if (environmentMonitor.ValidationResult.IsValid)
            {
                CommandDefinition commandDefinition = new CommandDefinition(sqlSentenceArg);
                DBConnection.Open();
                retVal = SqlMapper.Execute(DBConnection, commandDefinition);
                ret = true;
                DBConnection.Close();
            }

            return ret;
        }

        public virtual bool Query<T>(string sqlSentenceArg, out List<T> tHub)
        {
            bool ret = false;
            tHub = default(List<T>);
            dataBaseLogController= environmentMonitor.LogSetting.GetContent(LogPart.DataBase);

            if (environmentMonitor.ValidationResult.IsValid)
            {
                CommandDefinition commandDefinition = new CommandDefinition(sqlSentenceArg);

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
                    dataBaseLogController.WriteDebug(sqlSentenceArg);
                    DBConnection.Close();
                }
            }

            return ret;
        }

        public virtual bool Execute(string sqlSentenceArg)
        {
            bool ret = false;
            int retVal = 0;
            dataBaseLogController = environmentMonitor.LogSetting.GetContent(LogPart.DataBase);

            if (environmentMonitor.ValidationResult.IsValid)
            {
                CommandDefinition commandDefinition = new CommandDefinition(sqlSentenceArg);

                try
                {
                    DBConnection.Open();
                    retVal = SqlMapper.Execute(DBConnection, commandDefinition);
                }
                catch (Exception ex)
                {
                    dataBaseLogController.WriteDebug(ex.Message);
                }
                finally
                {
                    ret = true;

                    if (retVal == 0)
                        dataBaseLogController.WriteDebug("未影响: " + sqlSentenceArg);
                    else
                        dataBaseLogController.WriteDebug(sqlSentenceArg);

                    DBConnection.Close();
                }
            }

            return ret;
        }

        public virtual bool QueryWithMessage<T>(string sqlSentenceArg, out List<T> tHub , out string retStringArg)
        {
            bool ret = false;
            tHub = default(List<T>);
            retStringArg = string.Empty;
            dataBaseLogController = environmentMonitor.LogSetting.GetContent(LogPart.DataBase);

            if (environmentMonitor.ValidationResult.IsValid)
            {
                CommandDefinition commandDefinition = new CommandDefinition(sqlSentenceArg);

                try
                {
                    DBConnection.Open();
                    reader = SqlMapper.ExecuteReader(DBConnection, commandDefinition);
                    reader.Read();

                    if (reader.GetString(0) != "F")
                    {
                        reader.Close();
                        tHub = SqlMapper.Query<T>(DBConnection, commandDefinition).AsList();
                        ret = true;
                    } 
                    else
                    {
                        retStringArg = reader.GetString(1);
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    dataBaseLogController.WriteDebug(ex.Message);
                }
                finally
                {
                    dataBaseLogController.WriteDebug(sqlSentenceArg);
                    DBConnection.Close();
                }
            }

            return ret;
        }

        public virtual bool ExecuteWithMessage(string sqlSentenceArg, out string retStringArg)
        {
            bool ret = false;
            retStringArg = string.Empty;
            dataBaseLogController = environmentMonitor.LogSetting.GetContent(LogPart.DataBase);

            if (environmentMonitor.ValidationResult.IsValid)
            {
                CommandDefinition commandDefinition = new CommandDefinition(sqlSentenceArg);

                try
                {
                    DBConnection.Open();
                    reader = SqlMapper.ExecuteReader(DBConnection, commandDefinition);
                    reader.Read();
                    
                    if (reader.GetString(0) == "T")
                        ret = true;
                    else
                        retStringArg = reader.GetString(1);
                }
                catch (Exception ex)
                {
                    dataBaseLogController.WriteDebug(ex.Message);
                }
                finally
                {
                    dataBaseLogController.WriteDebug(sqlSentenceArg);
                    DBConnection.Close();
                }
            }

            return ret;
        }
    }
}
