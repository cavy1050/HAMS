using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Extensions;
using System.Windows;

namespace HAMS.Frame.Kernel.Services
{
    public class PathManager : IManager<PathPart>
    {
        IContainerProvider containerProvider;
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeBaseController;

        string sqlSentence;

        /// <summary>
        /// 程序运行目录
        /// </summary>
        public string ApplictionCatalogue { get; set; }

        /// <summary>
        /// 本地数据库文件路径
        /// </summary>
        public string NativeDataBaseFilePath { get; set; }

        /// <summary>
        /// 日志文件目录
        /// </summary>
        public string LogFileCatalogue { get; set; }

        public PathManager(IContainerProvider containerProviderArg)
        {
            containerProvider = containerProviderArg;
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
        }

        public void DeInit(PathPart pathPartArg)
        {
            switch (pathPartArg)
            {
                case PathPart.ApplictionCatalogue:
                    ApplictionCatalogue = AppDomain.CurrentDomain.BaseDirectory;
                    break;

                case PathPart.NativeDataBaseFilePath:
                    NativeDataBaseFilePath = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName.Replace("exe", "db");
                    break;

                case PathPart.LogFileCatalogue:
                    LogFileCatalogue = AppDomain.CurrentDomain.BaseDirectory + "Log\\";
                    break;

                case PathPart.All:
                    DeInit(PathPart.ApplictionCatalogue);
                    DeInit(PathPart.NativeDataBaseFilePath);
                    DeInit(PathPart.LogFileCatalogue);
                    break;
            }
        }

        public void Init(PathPart pathPartArg)
        {
            List<BaseKind> costomSettingKind;
            nativeBaseController = containerProvider.Resolve<IDataBaseController>(DataBasePart.Native.ToString());
            sqlSentence = "SELECT Code,Item,Name,Content,Description,Note,Rank,Flag FROM System_PathSetting WHERE Code NOT IN ('01GPKA6WE841SE31MQWH3Y5WNF','01GPKA6WE85VSFC0S16CF7MCBJ')";
            nativeBaseController.QueryNoLog<BaseKind>(sqlSentence, out costomSettingKind);

            switch (pathPartArg)
            {
                case PathPart.LogFileCatalogue:
                    LogFileCatalogue = costomSettingKind.FirstOrDefault(x => x.Code == "01GPSK8EY3VD74Y0508D7KP2Z4").Content;
                    break;

                case PathPart.All:
                    Init(PathPart.LogFileCatalogue);
                    break;
            }
        }

        public void Load(PathPart pathPartArg)
        {
            switch (pathPartArg)
            {
                case PathPart.ApplictionCatalogue:
                    if (!environmentMonitor.PathSetting.Exists(x => x.Code == "01GPKA6WE841SE31MQWH3Y5WNF"))
                        environmentMonitor.PathSetting.Add(new BaseKind
                        {
                            Code = "01GPKA6WE841SE31MQWH3Y5WNF",
                            Item = PathPart.ApplictionCatalogue.ToString(),
                            Name = EnumExtension.GetDescription(PathPart.ApplictionCatalogue),
                            Content = ApplictionCatalogue,
                            Rank = Convert.ToInt32(PathPart.ApplictionCatalogue),
                            Flag = true
                        });
                    break;

                case PathPart.NativeDataBaseFilePath:
                    if (!environmentMonitor.PathSetting.Exists(x => x.Code == "01GPKA6WE85VSFC0S16CF7MCBJ"))
                        environmentMonitor.PathSetting.Add(new BaseKind
                        {
                            Code = "01GPKA6WE85VSFC0S16CF7MCBJ",
                            Item = PathPart.NativeDataBaseFilePath.ToString(),
                            Name = EnumExtension.GetDescription(PathPart.NativeDataBaseFilePath),
                            Content = NativeDataBaseFilePath,
                            Rank = Convert.ToInt32(PathPart.NativeDataBaseFilePath),
                            Flag = true
                        });
                    break;

                case PathPart.LogFileCatalogue:
                    if (!environmentMonitor.PathSetting.Exists(x => x.Code == "01GPSK8EY3VD74Y0508D7KP2Z4"))
                        environmentMonitor.PathSetting.Add(new BaseKind
                        {
                            Code = "01GPSK8EY3VD74Y0508D7KP2Z4",
                            Item = PathPart.LogFileCatalogue.ToString(),
                            Name = EnumExtension.GetDescription(PathPart.LogFileCatalogue),
                            Content = LogFileCatalogue,
                            Rank = Convert.ToInt32(PathPart.LogFileCatalogue),
                            Flag = true
                        });
                    else
                        environmentMonitor.PathSetting[PathPart.LogFileCatalogue].Content = LogFileCatalogue;
                    break;

                case PathPart.All:
                    Load(PathPart.ApplictionCatalogue);
                    Load(PathPart.NativeDataBaseFilePath);
                    Load(PathPart.LogFileCatalogue);
                    break;
            }
        }

        public void Save(PathPart pathPartArg)
        {
            
        }
    }
}
