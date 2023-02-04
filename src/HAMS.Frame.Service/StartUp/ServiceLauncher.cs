﻿using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Events;
using HAMS.Frame.Kernel.Services;
using HAMS.Frame.Service.Peripherals;
using System.Windows;

namespace HAMS.Frame.Service
{
    public class ServiceLauncher
    {
        IContainerProvider containerProvider;
        IModuleManager moduleManager;
        IEventAggregator eventAggregator;
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeBaseController;
        IEventServiceController eventServiceController;

        IAccountAuthenticationControler accountAuthenticationControler;

        string eventServiceJsonText, sqlSentence;
        List<SettingKind> userSettingHub;

        public ServiceLauncher(IContainerProvider containerProviderArg)
        {
            containerProvider = containerProviderArg;
            moduleManager = containerProviderArg.Resolve<IModuleManager>();
            eventAggregator = containerProviderArg.Resolve<IEventAggregator>();
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
            nativeBaseController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.Native);
            eventServiceController = containerProviderArg.Resolve<IEventServiceController>();
        }

        public void Initialize()
        {
            moduleManager.LoadModuleCompleted += ModuleManager_LoadModuleCompleted;

            eventAggregator.GetEvent<RequestServiceEvent>().Subscribe(OnRequestApplicationAlterationService, ThreadOption.PublisherThread, false, x => x.Contains("ApplicationAlterationService"));
            eventAggregator.GetEvent<RequestServiceEvent>().Subscribe(OnRequestAccountVerificationService, ThreadOption.PublisherThread, false, x => x.Contains("AccountVerificationService"));
        }

        private void ModuleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs args)
        {
            if (args.ModuleInfo.ModuleName == "LoginModule")
            {
                eventServiceJsonText = eventServiceController.Request(EventServicePart.EventInitializationService, FrameModulePart.ServiceModule, FrameModulePart.All, new EmptyContentKind());
                eventAggregator.GetEvent<RequestServiceEvent>().Publish(eventServiceJsonText);
            }
        }

        private void OnRequestApplicationAlterationService(string requestServiceTextArg)
        {
            JObject requestObj = JObject.Parse(requestServiceTextArg);
            JObject requestContentObj = requestObj["svc_cont"].Value<JObject>();

            ControlTypePart requestControlTypeObj = (ControlTypePart)Enum.Parse(typeof(ControlTypePart), requestContentObj["app_ctl_type"].Value<string>());
            ActiveFlagPart requestActiveFlagObj = (ActiveFlagPart)Enum.Parse(typeof(ActiveFlagPart), requestContentObj["app_act_flag"].Value<string>());

            switch (requestControlTypeObj)
            {
                case ControlTypePart.LoginWindow:
                    if (!environmentMonitor.ApplicationControlSetting.ContainsKey(ControlTypePart.LoginWindow))
                        environmentMonitor.ApplicationControlSetting.Add(requestControlTypeObj, requestActiveFlagObj);
                    else
                        environmentMonitor.ApplicationControlSetting[ControlTypePart.LoginWindow] = requestActiveFlagObj;
                    break;
                case ControlTypePart.MainWindow:
                    if (!environmentMonitor.ApplicationControlSetting.ContainsKey(ControlTypePart.MainWindow))
                        environmentMonitor.ApplicationControlSetting.Add(requestControlTypeObj, requestActiveFlagObj);
                    else
                        environmentMonitor.ApplicationControlSetting[ControlTypePart.MainWindow] = requestActiveFlagObj;
                    break;
                case ControlTypePart.MainLeftDrawer:
                    if (!environmentMonitor.ApplicationControlSetting.ContainsKey(ControlTypePart.MainLeftDrawer))
                        environmentMonitor.ApplicationControlSetting.Add(requestControlTypeObj, requestActiveFlagObj);
                    else
                        environmentMonitor.ApplicationControlSetting[ControlTypePart.MainLeftDrawer] = requestActiveFlagObj;
                    break;
            }
        }

        private void OnRequestAccountVerificationService(string requestServiceTextArg)
        {
            string errorMessage=string.Empty;
            List<FrameModulePart> targetFrameModules = new List<FrameModulePart>() { FrameModulePart.ApplictionModule, FrameModulePart.LoginModule, FrameModulePart.KernelModule };

            JObject requestObj = JObject.Parse(requestServiceTextArg);
            AccountVerificationServiceRequestContentKind accountVerificationServiceRequestContent = JsonConvert.DeserializeObject<AccountVerificationServiceRequestContentKind>(requestObj["svc_cont"].ToString());

            accountAuthenticationControler = containerProvider.Resolve<IAccountAuthenticationControler>();

            if (accountAuthenticationControler.Validate(accountVerificationServiceRequestContent, out errorMessage))
            {
                sqlSentence = "SELECT Code,Item,Name,Content,Description,Note,Rank,DefaultFlag,EnabledFlag FROM System_UserSetting WHERE Item='" + accountVerificationServiceRequestContent.Account + "'";
                nativeBaseController.Query<SettingKind>(sqlSentence, out userSettingHub);

                AccountVerificationServiceResponseContentKind accountVerificationServiceResponseContent = new AccountVerificationServiceResponseContentKind();
                accountVerificationServiceResponseContent.Account = accountVerificationServiceRequestContent.Account;
                accountVerificationServiceResponseContent.Password = accountVerificationServiceRequestContent.Password;
                accountVerificationServiceResponseContent.Name = userSettingHub.FirstOrDefault().Name;

                eventServiceJsonText = eventServiceController.Response(EventServicePart.AccountVerificationService, FrameModulePart.ServiceModule, targetFrameModules, true, errorMessage, accountVerificationServiceResponseContent);
            }
            else
                eventServiceJsonText = eventServiceController.Response(EventServicePart.AccountVerificationService, FrameModulePart.ServiceModule, targetFrameModules, false, errorMessage, new EmptyContentKind());

            eventAggregator.GetEvent<ResponseServiceEvent>().Publish(eventServiceJsonText);
        }
    }
}
