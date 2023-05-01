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
    public class AccountControler : IEventResponseController
    {
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeBaseController;
        IEventController eventController;

        List<SettingKind> userSettingHub;

        JObject requestObj, requestContentObj;
        string account, password, sqlSentence, eventJsonSentence;

        public AccountControler(IContainerProvider containerProviderArgs)
        {
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
            nativeBaseController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.Native);
            eventController = containerProviderArgs.Resolve<IEventController>();
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
                eventJsonSentence = eventController.Response(EventPart.AccountEvent, EventBehaviourPart.Activation, FrameModulePart.ServiceModule,
                    FrameModulePart.LoginModule, new EmptyContentKind(), false, errorMessageArg);
            else
                eventJsonSentence = eventController.Response(EventPart.AccountEvent, EventBehaviourPart.Activation, FrameModulePart.ServiceModule,
                    FrameModulePart.LoginModule, new EmptyContentKind(), true, string.Empty);

            return eventJsonSentence;
        }
    }
}
