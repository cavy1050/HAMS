using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Prism.Ioc;
using Prism.Mvvm;
using HAMS.Frame.Kernel.Core;

namespace HAMS.Frame.Kernel.Controls
{
    public class ConnectionModel : BindableBase
    {
        IEnvironmentMonitor environmentMonitor;

        string dataBaseName;
        public string DataBaseName
        {
            get => dataBaseName;
            set => SetProperty(ref dataBaseName, value);
        }

        string dataSource;
        public string DataSource
        {
            get => dataSource;
            set => SetProperty(ref dataSource, value);
        }

        string initialCatalog;
        public string InitialCatalog
        {
            get => initialCatalog;
            set => SetProperty(ref initialCatalog, value);
        }

        string userID;
        public string UserID
        {
            get => userID;
            set => SetProperty(ref userID, value);
        }

        string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        SqlConnectionStringBuilder sqlConnectionStringBuilder;
        public SqlConnectionStringBuilder SqlConnectionStringBuilder
        {
            get => sqlConnectionStringBuilder;
            set => sqlConnectionStringBuilder = value;
        }

        public ConnectionModel (IContainerProvider containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
        }

        public void Loaded(string dataBaseIdentifierArg, string ConnectionStringArg)
        {
            DataBasePart dataBase = (DataBasePart)Enum.Parse(typeof(DataBasePart), dataBaseIdentifierArg);
            DataBaseName = environmentMonitor.DataBaseSetting[dataBase].Name;

            sqlConnectionStringBuilder = new SqlConnectionStringBuilder(ConnectionStringArg);
            DataSource = sqlConnectionStringBuilder.DataSource;
            InitialCatalog = sqlConnectionStringBuilder.InitialCatalog;
            UserID= sqlConnectionStringBuilder.UserID;
            Password = sqlConnectionStringBuilder.Password;
        }

        public void Refresh()
        {
            sqlConnectionStringBuilder.DataSource = DataSource;
            sqlConnectionStringBuilder.InitialCatalog = InitialCatalog;
            sqlConnectionStringBuilder.UserID = UserID;
            sqlConnectionStringBuilder.Password = Password;
        }
    }
}
