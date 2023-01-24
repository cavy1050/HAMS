using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Extensions;

namespace HAMS.Frame.Kernel.Services
{
    public class DataBaseManager : IManager<DataBasePart>
    {
        IContainerProvider containerProvider;
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeBaseController;
        IDataBaseController bagldbBaseController;
        ISecurityController securityController;

        string sqlSentence;
        List<BaseKind> costomDataBaseSettingHub;

        /// <summary>
        /// 本地数据库连接字符串
        /// </summary>
        public string NativeConnectString { get; set; }

        /// <summary>
        /// 病案管理数据库连接字符串
        /// </summary>
        public string BAGLDBConnectString { get; set; }

        public DataBaseManager(IContainerProvider containerProviderArg)
        {
            containerProvider = containerProviderArg;
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
            securityController= containerProviderArg.Resolve<ISecurityController>();
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
            else
            {         
                nativeBaseController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.Native);
                sqlSentence = "SELECT Code,Item,Name,Content,Description,Note,Rank,Flag FROM System_DataBaseSetting WHERE Flag = False";
                nativeBaseController.QueryNoLog<BaseKind>(sqlSentence, out costomDataBaseSettingHub);

                switch (dataBasePartArg)
                {
                    case DataBasePart.BAGLDB:
                        BAGLDBConnectString = securityController.DataBaseConnectionStringDecrypt(costomDataBaseSettingHub.FirstOrDefault(x=>x.Code== "01GQ4CXY72MZ7GZAFR9MSE99AW").Content);
                        break;

                    case DataBasePart.All:
                        Init(DataBasePart.BAGLDB);
                        break;
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
                            Flag = true
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
                            Flag = false
                        });

                    bagldbBaseController = containerProvider.Resolve<IDataBaseController>(DataBasePart.BAGLDB.ToString());
                    environmentMonitor.DataBaseSetting[DataBasePart.BAGLDB].DataBaseController = bagldbBaseController;
                    break;

                case DataBasePart.All:
                    Load(DataBasePart.Native);
                    Load(DataBasePart.BAGLDB);
                    break;
            }
        }

        public void Save(DataBasePart dataBasePartArg)
        {

        }
    }
}
