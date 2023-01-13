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
    public class PathManager : IManager<PathPart>
    {
        /// <summary>
        /// 默认程序运行目录
        /// </summary>
        public string DefaultApplictionCatalogue { get; set; }

        /// <summary>
        /// 默认本地数据库文件路径
        /// </summary>
        public string DefaultNativeDataBaseFilePath { get; set; }

        IEnvironmentMonitor environmentMonitor;

        public PathManager(IContainerProvider containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
        }

        public void Initialize(PathPart pathPartArg)
        {
            switch (pathPartArg)
            {
                case PathPart.ApplictionCatalogue:
                    DefaultApplictionCatalogue = AppDomain.CurrentDomain.BaseDirectory;
                    break;

                case PathPart.NativeDataBaseFilePath:
                    DefaultNativeDataBaseFilePath = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName.Replace("exe", "db");
                    break;

                case PathPart.All:
                    Initialize(PathPart.ApplictionCatalogue);
                    Initialize(PathPart.NativeDataBaseFilePath);
                    break;
            }
        }

        public void Load(PathPart pathPartArg)
        {
            switch (pathPartArg)
            {
                case PathPart.ApplictionCatalogue:
                    if (!environmentMonitor.PathSetting.Exists(x => x.Code == "01GPKA6WE841SE31MQWH3Y5WNF"))
                    {
                        environmentMonitor.PathSetting.Add(new BaseKind
                        {
                            Code = "01GPKA6WE841SE31MQWH3Y5WNF",
                            Item = PathPart.ApplictionCatalogue.ToString(),
                            Name = EnumExtension.GetDescription(PathPart.ApplictionCatalogue),
                            Content = DefaultApplictionCatalogue,
                            Rank = Convert.ToInt32(PathPart.ApplictionCatalogue),
                            Flag = true
                        }) ;
                    }
                    break;

                case PathPart.NativeDataBaseFilePath:
                    if (!environmentMonitor.PathSetting.Exists(x => x.Code == "01GPKA6WE85VSFC0S16CF7MCBJ"))
                    {
                        environmentMonitor.PathSetting.Add(new BaseKind
                        {
                            Code = "01GPKA6WE85VSFC0S16CF7MCBJ",
                            Item = PathPart.NativeDataBaseFilePath.ToString(),
                            Name = EnumExtension.GetDescription(PathPart.NativeDataBaseFilePath),
                            Content = DefaultApplictionCatalogue,
                            Rank = Convert.ToInt32(PathPart.NativeDataBaseFilePath),
                            Flag = true
                        });
                    }
                    break;

                case PathPart.All:
                    Initialize(PathPart.ApplictionCatalogue);
                    Initialize(PathPart.NativeDataBaseFilePath);
                    break;
            }
        }

        public void Save(PathPart pathPartArg)
        {
            
        }
    }
}
