using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using FluentValidation;

namespace HAMS.Frame.Kernel.Services
{
    public class DefaultDataBaseValidator : AbstractValidator<DataBaseManager>
    {
        IDbConnection dbConnection;

        public DefaultDataBaseValidator()
        {
            RuleFor(path => path.NativeConnectString).Must(CheckNativeConnectString).WithMessage("本地数据库文件连接失败!");
        }

        private bool CheckNativeConnectString(string nativeConnectStringArg)
        {
            bool ret = false;

            dbConnection = new SQLiteConnection(nativeConnectStringArg);
            dbConnection.Open();
            if (dbConnection.State == ConnectionState.Open)
                ret = true;
            dbConnection.Close();
            dbConnection.Dispose();

            return ret;
        }
    }
}
