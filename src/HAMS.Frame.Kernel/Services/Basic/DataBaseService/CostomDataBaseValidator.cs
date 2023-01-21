using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using FluentValidation;

namespace HAMS.Frame.Kernel.Services
{
    public class CostomDataBaseValidator : AbstractValidator<DataBaseManager>
    {
        IDbConnection dbConnection;

        public CostomDataBaseValidator()
        {
            RuleFor(dataBase => dataBase.BAGLDBConnectString).Must(BAGLDBConnectStringValidate).WithMessage("病案管理数据库连接失败!");
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
