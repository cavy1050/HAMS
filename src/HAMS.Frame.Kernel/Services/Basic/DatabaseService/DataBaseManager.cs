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
        }

        public void DeInit(DataBasePart dataBasePartArg)
        {
            if (dataBasePartArg != DataBasePart.Native)
                throw new ArgumentException("初始化默认参数必须为本地数据库!", nameof(dataBasePartArg));
            else
                NativeConnectString = "Data Source= " + environmentMonitor.PathSetting.GetContent(PathPart.NativeDataBaseFilePath);
        }

        public void Init(DataBasePart dataBasePartArg)
        {

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

                case DataBasePart.All:
                    Load(DataBasePart.Native);
                    break;
            }
        }

        public void Save(DataBasePart dataBasePartArg)
        {

        }
    }
}
