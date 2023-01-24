using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using FluentValidation;

namespace HAMS.Frame.Kernel.Services
{
    public class DataBaseValidator : AbstractValidator<DataBaseManager>
    {
        IDbConnection dbConnection;

        public DataBaseValidator()
        {
            RuleSet("NativeValidateRule",()=>
            {
                RuleFor(dbConnStr=> dbConnStr.NativeConnectString).Must(NativeConnectStringValidate).WithMessage("本地数据库文件连接失败!");
            });

            RuleSet("BAGLDBValidateRule", () =>
            {
                RuleFor(dbConnStr => dbConnStr.BAGLDBConnectString).Must(BAGLDBConnectStringValidate).WithMessage("病案管理数据库连接失败!");
            });
        }

        private bool NativeConnectStringValidate(object nativeConnectStringArg)
        {
            bool ret = false;

            dbConnection = new SQLiteConnection((string)nativeConnectStringArg);
            dbConnection.Open();
            if (dbConnection.State == ConnectionState.Open)
                ret = true;
            dbConnection.Close();
            dbConnection.Dispose();

            return ret;
        }

        private bool BAGLDBConnectStringValidate(object bagldbConnectStringArg)
        {
            bool ret = false;

            dbConnection = new SqlConnection((string)bagldbConnectStringArg);
            dbConnection.Open();
            if (dbConnection.State == ConnectionState.Open)
                ret = true;
            dbConnection.Close();
            dbConnection.Dispose();

            return ret;
        }
    }
}
