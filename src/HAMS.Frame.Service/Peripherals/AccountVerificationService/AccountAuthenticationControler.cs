using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Prism.Ioc;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Services;

namespace HAMS.Frame.Service.Peripherals
{
    public class AccountAuthenticationControler : IAccountAuthenticationControler
    {
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeBaseController;

        string sqlSentence;
        List<SettingKind> userSettingHub;

        public AccountAuthenticationControler(IContainerProvider containerProviderArgs)
        {
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
            nativeBaseController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.Native);
        }

        public bool Validate(AccountVerificationServiceRequestContentKind requestContentArg, out string errorMessageArg)
        {
            bool ret = false;
            errorMessageArg = string.Empty;
            string pwdDBText, pwdCipherText;

            sqlSentence = "SELECT Code,Item,Name,Content,Description,Note,Rank,DefaultFlag,EnabledFlag FROM System_UserSetting WHERE Item='" + requestContentArg.Account + "'";
            nativeBaseController.Query<SettingKind>(sqlSentence, out userSettingHub);
            if (userSettingHub.Count==0)
                errorMessageArg = "无此账户记录,请核对后重新输入!";
            else
            {
                pwdDBText = userSettingHub.FirstOrDefault().Content;

                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                pwdCipherText = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(requestContentArg.Password))).Replace("-", "");

                if (pwdDBText != pwdCipherText)
                    errorMessageArg = "密码错误,请核对后重新输入!";
                else
                    ret = true;
            }                

            return ret;
        }
    }
}
