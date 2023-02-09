using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Prism.Ioc;
using Newtonsoft.Json.Linq;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Events;
using HAMS.Frame.Kernel.Services;

namespace HAMS.Frame.Service.Peripherals
{
    public class AccountVerificationControler : IAccountVerificationControler
    {
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeBaseController;
        IEventServiceController eventServiceController;

        List<SettingKind> userSettingHub;

        JObject requestObj, requestContentObj;
        string account, password, sqlSentence, eventJsonSentence;

        public AccountVerificationControler(IContainerProvider containerProviderArgs)
        {
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
            nativeBaseController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.Native);
            eventServiceController = containerProviderArgs.Resolve<IEventServiceController>();
        }

        private bool Validate(string requestServiceTextArg, out string errorMessageArg)
        {
            bool ret = false;
            errorMessageArg = string.Empty;
            string pwdDBText, pwdCipherText;

            requestObj = JObject.Parse(requestServiceTextArg);
            requestContentObj = requestObj.Value<JObject>("svc_cont");

            account = requestContentObj.Value<string>("acct_code");
            password = requestContentObj.Value<string>("acct_pwd");

            sqlSentence = "SELECT Code,Item,Name,Content,Description,Note,Rank,DefaultFlag,EnabledFlag FROM System_UserSetting WHERE Item='" + account + "'";
            nativeBaseController.Query<SettingKind>(sqlSentence, out userSettingHub);
            if (userSettingHub.Count == 0)
                errorMessageArg = "无此账户记录,请核对后重新输入!";
            else
            {
                pwdDBText = userSettingHub.FirstOrDefault().Content;

                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                pwdCipherText = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(password))).Replace("-", "");

                if (pwdDBText != pwdCipherText)
                    errorMessageArg = "密码错误,请核对后重新输入!";
                else
                {
                    ret = true;
                    environmentMonitor.UserSetting = userSettingHub.FirstOrDefault();
                }

            }

            return ret;
        }

        public string Response(string requestServiceTextArg)
        {
            string errorMessageArg;

            if (!Validate(requestServiceTextArg, out errorMessageArg))
                eventJsonSentence = eventServiceController.Response(EventServicePart.AccountVerificationService, FrameModulePart.ServiceModule,
                    new List<FrameModulePart> { FrameModulePart.LoginModule },
                    false, errorMessageArg, new EmptyContentKind());
            else
                eventJsonSentence = eventServiceController.Response(EventServicePart.AccountVerificationService, FrameModulePart.ServiceModule,
                    new List<FrameModulePart> { FrameModulePart.LoginModule },
                    true, string.Empty, new EmptyContentKind());

            return eventJsonSentence;
        }
    }
}
