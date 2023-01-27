﻿using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Prism.Ioc;
using Prism.Mvvm;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Services;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Windows;

namespace HAMS.Frame.Control.Login.Models
{
    public class LoginConfigModel : BindableBase
    {
        string sqlSentence;
        List<SettingKind> versionSettingHub;

        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeDataBaseController;

        string versionNumber;
        public string VersionNumber
        {
            get => versionNumber;
            set => SetProperty(ref versionNumber, value);
        }

        string versionCode;
        public string VersionCode
        {
            get => versionCode;
            set => SetProperty(ref versionCode, value);
        }

        string validTime;
        public string ValidTime
        {
            get => validTime;
            set => SetProperty(ref validTime, value);
        }

        string openSourceAddress;
        public string OpenSourceAddress
        {
            get => openSourceAddress;
            set => SetProperty(ref openSourceAddress, value);
        }

        string openSourceProtocol;
        public string OpenSourceProtocol
        {
            get => openSourceProtocol;
            set => SetProperty(ref openSourceProtocol, value);
        }

        string email;
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public LoginConfigModel(IContainerProvider containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
        }

        public void LoadVersionData()
        {
            nativeDataBaseController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.Native);

            sqlSentence = "SELECT Code,Item,Name,Content,Description,Note,DefaultFlag,EnabledFlag FROM System_VersionSetting WHERE EnabledFlag=True";
            nativeDataBaseController.Query<SettingKind>(sqlSentence, out versionSettingHub);

            VersionNumber = versionSettingHub.FirstOrDefault(x => x.Code == "01GPGV56ZKV8DHK6AQDPY1B97T").Content;
            VersionCode= versionSettingHub.FirstOrDefault(x => x.Code == "01GPGV56ZK7H2W407TSQY8VM03").Content;
            ValidTime = versionSettingHub.FirstOrDefault(x => x.Code == "01GPGV56ZMTTET8PECQT9X2X8N").Content;
            OpenSourceAddress = versionSettingHub.FirstOrDefault(x => x.Code == "01GPGV56ZMR71N1XAXT0QCNP02").Content;
            OpenSourceProtocol = versionSettingHub.FirstOrDefault(x => x.Code == "01GPGV9FGJBMRJ81MQAE06EPAH").Content;
            Email = versionSettingHub.FirstOrDefault(x => x.Code == "01GPGV9FGJFVY0RYXVEFA9M32C").Content;
        }
    }
}
