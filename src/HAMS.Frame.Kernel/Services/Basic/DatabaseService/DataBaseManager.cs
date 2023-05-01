using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Ioc;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Extensions;

namespace HAMS.Frame.Kernel.Services
{
    public class DataBaseManager : IManager<DataBasePart>
    {
        IContainerProvider containerProvider;
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeBaseController,bagldbBaseController,mzcisdbBaseController, zycisdbBaseController;
        ICipherColltroller cipherColltroller;

        string sqlSentence;
        List<SettingKind> costomDataBaseSettingHub;

        /// <summary>
        /// 本地数据库连接字符串
        /// </summary>
        public string NativeConnectString { get; set; }

        /// <summary>
        /// 病案管理数据库连接字符串
        /// </summary>
        public string BAGLDBConnectString { get; set; }

        /// <summary>
        /// 临床管理系统(门诊)连接字符串
        /// </summary>
        public string MZCISDBConnectString { get; set; }

        /// <summary>
        /// 临床管理系统(住院)连接字符串
        /// </summary>
        public string ZYCISDBConnectString { get; set; }

        public DataBaseManager(IContainerProvider containerProviderArg)
        {
            containerProvider = containerProviderArg;
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
            cipherColltroller = containerProviderArg.Resolve<ICipherColltroller>();
        }

        public void DeInit(DataBasePart dataBasePartArg)
        {
            if (dataBasePartArg != DataBasePart.Native)
                throw new ArgumentException("默认参数必须为本地数据库!", nameof(dataBasePartArg));
            else
                NativeConnectString = "Data Source= " + environmentMonitor.PathSetting.GetContent(PathPart.NativeDataBaseFilePath);
        }

        public void Init(DataBasePart dataBasePartArg)
        {
            if (dataBasePartArg == DataBasePart.Native )
                throw new ArgumentException("自定义参数不能为本地数据库!", nameof(dataBasePartArg));
            {
                nativeBaseController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.Native);
                sqlSentence = "SELECT Code,Item,Name,Content,Description,Note,Rank,DefaultFlag,EnabledFlag FROM System_DataBaseSetting WHERE EnabledFlag = True AND DefaultFlag = False";
                nativeBaseController.QueryNoLog<SettingKind>(sqlSentence, out costomDataBaseSettingHub);

                switch (dataBasePartArg)
                {
                    case DataBasePart.BAGLDB:
                        BAGLDBConnectString = cipherColltroller.DataBaseConnectionStringDecrypt(costomDataBaseSettingHub.FirstOrDefault(x=>x.Code== "01GQ4CXY72MZ7GZAFR9MSE99AW").Content);
                        break;

                    case DataBasePart.MZCISDB:
                        MZCISDBConnectString = cipherColltroller.DataBaseConnectionStringDecrypt(costomDataBaseSettingHub.FirstOrDefault(x => x.Code == "01GYW88JAYT60MZHSZT4G25Q1B").Content);
                        break;

                    case DataBasePart.ZYCISDB:
                        ZYCISDBConnectString = cipherColltroller.DataBaseConnectionStringDecrypt(costomDataBaseSettingHub.FirstOrDefault(x => x.Code == "01GZ8C9VQ9FDQHAV5AB39DGTF7").Content);
                        break;

                    case DataBasePart.All:
                        Init(DataBasePart.BAGLDB);
                        Init(DataBasePart.MZCISDB);
                        Init(DataBasePart.ZYCISDB);
                        break;
                }
            }
        }

        public void Init(DataBasePart dataBasePartArg, params object[] costomConnectStringArgs)
        {
            if (dataBasePartArg == DataBasePart.Native || dataBasePartArg == DataBasePart.All)
                throw new ArgumentException("自定义参数不能为<本地数据库>,<全部数据库>!", nameof(dataBasePartArg));
            {
                string costomConnectString = costomConnectStringArgs.FirstOrDefault().ToString();
                if (!string.IsNullOrEmpty(costomConnectString))
                {
                    switch (dataBasePartArg)
                    {
                        case DataBasePart.BAGLDB:
                            BAGLDBConnectString = costomConnectString;
                            break;

                        case DataBasePart.MZCISDB:
                            MZCISDBConnectString = costomConnectString;
                            break;

                        case DataBasePart.ZYCISDB:
                            ZYCISDBConnectString = costomConnectString;
                            break;
                    }
                }
            }
        }

        public void Load(DataBasePart dataBasePartArg)
        {
            switch (dataBasePartArg)
            {
                case DataBasePart.Native:
                    if (!environmentMonitor.DataBaseSetting.Exists(x => x.Code == "01GQ4CXY72MR4SKSJG7664B1HS"))
                        environmentMonitor.DataBaseSetting.Add(new DataBaseKind
                        {
                            Code = "01GQ4CXY72MR4SKSJG7664B1HS",
                            Item = DataBasePart.Native.ToString(),
                            Name = EnumExtension.GetDescription(DataBasePart.Native),
                            Content = NativeConnectString,
                            Rank = Convert.ToInt32(DataBasePart.Native),
                            EnabledFlag = true
                        });

                    nativeBaseController = containerProvider.Resolve<IDataBaseController>(DataBasePart.Native.ToString());
                    environmentMonitor.DataBaseSetting[DataBasePart.Native].DataBaseController = nativeBaseController;
                    break;

                case DataBasePart.BAGLDB:
                    if (!environmentMonitor.DataBaseSetting.Exists(x => x.Code == "01GQ4CXY72MZ7GZAFR9MSE99AW"))
                        environmentMonitor.DataBaseSetting.Add(new DataBaseKind
                        {
                            Code = "01GQ4CXY72MZ7GZAFR9MSE99AW",
                            Item = DataBasePart.BAGLDB.ToString(),
                            Name = EnumExtension.GetDescription(DataBasePart.BAGLDB),
                            Content = BAGLDBConnectString,
                            Rank = Convert.ToInt32(DataBasePart.BAGLDB),
                            EnabledFlag = true
                        });

                    environmentMonitor.DataBaseSetting[DataBasePart.BAGLDB].Content = BAGLDBConnectString;
                    bagldbBaseController = containerProvider.Resolve<IDataBaseController>(DataBasePart.BAGLDB.ToString());
                    environmentMonitor.DataBaseSetting[DataBasePart.BAGLDB].DataBaseController = bagldbBaseController;
                    break;

                case DataBasePart.MZCISDB:
                    if (!environmentMonitor.DataBaseSetting.Exists(x => x.Code == "01GYW88JAYT60MZHSZT4G25Q1B"))
                        environmentMonitor.DataBaseSetting.Add(new DataBaseKind
                        {
                            Code = "01GYW88JAYT60MZHSZT4G25Q1B",
                            Item = DataBasePart.MZCISDB.ToString(),
                            Name = EnumExtension.GetDescription(DataBasePart.MZCISDB),
                            Content = MZCISDBConnectString,
                            Rank = Convert.ToInt32(DataBasePart.MZCISDB),
                            EnabledFlag = true
                        });

                    environmentMonitor.DataBaseSetting[DataBasePart.MZCISDB].Content = MZCISDBConnectString;
                    mzcisdbBaseController = containerProvider.Resolve<IDataBaseController>(DataBasePart.MZCISDB.ToString());
                    environmentMonitor.DataBaseSetting[DataBasePart.MZCISDB].DataBaseController = mzcisdbBaseController;
                    break;

                case DataBasePart.ZYCISDB:
                    if (!environmentMonitor.DataBaseSetting.Exists(x => x.Code == "01GZ8C9VQ9FDQHAV5AB39DGTF7"))
                        environmentMonitor.DataBaseSetting.Add(new DataBaseKind
                        {
                            Code = "01GZ8C9VQ9FDQHAV5AB39DGTF7",
                            Item = DataBasePart.ZYCISDB.ToString(),
                            Name = EnumExtension.GetDescription(DataBasePart.ZYCISDB),
                            Content = ZYCISDBConnectString,
                            Rank = Convert.ToInt32(DataBasePart.ZYCISDB),
                            EnabledFlag = true
                        });

                    environmentMonitor.DataBaseSetting[DataBasePart.ZYCISDB].Content = ZYCISDBConnectString;
                    zycisdbBaseController = containerProvider.Resolve<IDataBaseController>(DataBasePart.ZYCISDB.ToString());
                    environmentMonitor.DataBaseSetting[DataBasePart.ZYCISDB].DataBaseController = zycisdbBaseController;
                    break;

                case DataBasePart.All:
                    Load(DataBasePart.Native);
                    Load(DataBasePart.BAGLDB);
                    Load(DataBasePart.MZCISDB);
                    Load(DataBasePart.ZYCISDB);
                    break;
            }
        }

        public void Save(DataBasePart dataBasePartArg)
        {
            switch (dataBasePartArg)
            {
                case DataBasePart.Native:
                    sqlSentence = "UPDATE System_DataBaseSetting SET Content='" + cipherColltroller.DataBaseConnectionStringEncrypt(NativeConnectString) + "' WHERE Code='01GQ4CXY72MR4SKSJG7664B1HS'";
                    nativeBaseController.ExecNoLog(sqlSentence);
                    break;

                case DataBasePart.BAGLDB:
                    sqlSentence = "UPDATE System_DataBaseSetting SET Content='" + cipherColltroller.DataBaseConnectionStringEncrypt(BAGLDBConnectString) + "' WHERE Code='01GQ4CXY72MZ7GZAFR9MSE99AW'";
                    nativeBaseController.ExecNoLog(sqlSentence);
                    break;

                case DataBasePart.MZCISDB:
                    sqlSentence = "UPDATE System_DataBaseSetting SET Content='" + cipherColltroller.DataBaseConnectionStringEncrypt(MZCISDBConnectString) + "' WHERE Code='01GYW88JAYT60MZHSZT4G25Q1B'";
                    nativeBaseController.ExecNoLog(sqlSentence);
                    break;

                case DataBasePart.ZYCISDB:
                    sqlSentence = "UPDATE System_DataBaseSetting SET Content='" + cipherColltroller.DataBaseConnectionStringEncrypt(ZYCISDBConnectString) + "' WHERE Code='01GZ8C9VQ9FDQHAV5AB39DGTF7'";
                    nativeBaseController.ExecNoLog(sqlSentence);
                    break;

                case DataBasePart.All:
                    Save(DataBasePart.Native);
                    Save(DataBasePart.BAGLDB);
                    Save(DataBasePart.MZCISDB);
                    Save(DataBasePart.ZYCISDB);
                    break;
            }
        }
    }
}
